using CompileLab.Repository.Entities;
using CompileLab.Service.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Service.Services
{
    internal static class Tools
    {
        private static SemaphoreSlim _semaphore = new SemaphoreSlim(Math.Max(1, Environment.ProcessorCount / 2));

        public static void CleanupOldTests(string basePath)
        {
            try
            {
                if (!Directory.Exists(basePath)) return;
                DateTime threshold = DateTime.Now.AddDays(-1);
                var directories = Directory.GetDirectories(basePath);

                foreach (var dir in directories)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dir);

                    if (dirInfo.LastWriteTime < threshold)
                    {
                        try
                        {
                            Directory.Delete(dir, true);
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException("No permission to delete folders", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("General cleanup error", ex);
            }
        }

        public static string SaveCodeToFile(StudentAnswer studentAnswer)
        {
            string uniqueRunName = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + "_" + Guid.NewGuid().ToString().Substring(0, 4);
            string studentDir = Path.Combine(Path.GetTempPath(), "CompileLab", uniqueRunName);

            if (!Directory.Exists(studentDir)) Directory.CreateDirectory(studentDir);

            string projectName = studentAnswer.Exercise.Language switch
            {
                ProgrammingLanguage.csharp => "Program.cs",
                ProgrammingLanguage.python => "main.py",
                _ => throw new NotSupportedException("Unsupported programming language")
            };

            string fullPath = Path.Combine(studentDir, projectName);
            if (studentAnswer.AnswerCode.Length > 50000)
                throw new Exception("Code too long");
            File.WriteAllText(fullPath, studentAnswer.AnswerCode);

            return studentDir;
        }
        public static async Task<(bool Success, string Details)> CompileCodeAsync(string folderPath)
        {
            string csprojContent = @"<Project Sdk=""Microsoft.NET.Sdk"">
                <PropertyGroup>
                <OutputType>Exe</OutputType>
                <TargetFramework>net9.0</TargetFramework>
                <ImplicitUsings>enable</ImplicitUsings>
                <Nullable>enable</Nullable>
                <RuntimeIdentifier>linux-x64</RuntimeIdentifier>
              </PropertyGroup>
            </Project>";

            await File.WriteAllTextAsync(Path.Combine(folderPath, "Project.csproj"), csprojContent);

            var psi = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = $"publish \"{Path.Combine(folderPath, "Project.csproj")}\" -c Release -r linux-x64 --self-contained false -o \"{Path.Combine(folderPath, "publish")}\" /nologo",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            if (File.Exists(Path.Combine(folderPath, "publish", "Project.dll")))
            {
                return (true, "Success");
            }

            return (false, output + error);
        }

        public static async Task<(bool Success, string Output)> ExecuteTestAsync(string folderPath, StudentAnswer studentAnswer, params string[] inputs)
        {
            ProgrammingLanguage pl = studentAnswer.Exercise.Language;
            await _semaphore.WaitAsync();
            string testerImage = pl switch
            {
                ProgrammingLanguage.csharp => "tester-csharp:1.0",
                ProgrammingLanguage.python => "tester-python:1.0",
                _ => throw new NotSupportedException("Unsupported programming language")
            };

            string mountSource = pl == ProgrammingLanguage.csharp
                ? Path.Combine(folderPath, "publish")
                : folderPath;

            string runCommand = pl == ProgrammingLanguage.csharp
                ? "dotnet /app/Project.dll"
                : "python /app/main.py";

            try
            {
                string ramLimit = "--memory=128m";
                string cpuLimit = "--cpus=0.3";
                string network = "--network none";
                string readOnly = "--read-only";
                string tempFs = "--tmpfs /tmp:exec,mode=1777";
                string user = "--user 1000:1000";
                string runId = Guid.NewGuid().ToString().Substring(0, 8);

                var psi = new ProcessStartInfo
                {
                    FileName = "docker",

                    Arguments = $"run --rm --init --name test_{runId} -i " +
                    $"{ramLimit} {cpuLimit} {network} {readOnly} {tempFs} {user} " +
                    $"-v \"{mountSource}:/app:ro\" " +
                    $"{testerImage} " +
                    $"{runCommand}",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = new Process { StartInfo = psi };
                process.Start();

                // הזרקת קלטים (Inputs)
                using (var sw = process.StandardInput)
                {
                    foreach (var line in inputs)
                        await sw.WriteLineAsync(line);
                }

                var outputTask = process.StandardOutput.ReadToEndAsync();
                var errorTask = process.StandardError.ReadToEndAsync();

                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10)); // טיימר חיצוני של 10 שניות לכל התהליך
                try
                {
                    await process.WaitForExitAsync(cts.Token);
                }
                catch (OperationCanceledException)
                {
                    if (!process.HasExited) process.Kill(true);
                    return (false, "Time Limit Exceeded");
                }
                string rawOutput = await outputTask;
                string rawError = await errorTask;
                string fullLog = rawOutput + rawError;

                if (process.ExitCode == 0)
                {
                    if(pl == ProgrammingLanguage.python)
                    {
                        return (true, CleanPythonRuntimeError(rawOutput));
                    }
                    return (true, CleanOutput(rawOutput));
                }
                if(pl == ProgrammingLanguage.python)
                {
                    return (false, CleanPythonRuntimeError(rawError));
                }
                return (false, CleanOutput(rawError));
            }
            finally
            {
                _semaphore.Release();
            }

        }
        public static async Task<AnswerMarkDto> Test(ICollection<TestCase> cases, string folderPath, StudentAnswer studentAnswer)
        {
            try
            {
                AnswerMarkDto answerMark = new AnswerMarkDto { IsSuccess = false, Mark = 0, Remark = null };
                if (studentAnswer.Exercise.Language == ProgrammingLanguage.csharp)
                {
                    var (Success, Details) = await CompileCodeAsync(folderPath);

                    if (!Success)
                    {
                        answerMark.TypeError = "CompilationError";
                        answerMark.ErrorMessage = CleanCompilationError(Details);
                        return answerMark;
                    }
                }

                List<string> errores = new List<string>();
                int numOfSucces = 0;

                foreach (TestCase testCase in cases)
                {

                    var (Success, Output) = await Tools.ExecuteTestAsync(folderPath, studentAnswer, testCase.Input.Split(','));

                    if (Success && Tools.CheckingSucces(Output, testCase.Output))
                    {
                        numOfSucces++;
                    }

                    else if (Success)
                    {
                        errores.Add($"the expected output was: {testCase.Output} the real output was: {Output}");
                    }

                    else
                    {
                        errores.Add($"{Output}");
                    }
                }
                double mark = 0;

                if (cases.Count != 0)
                    mark = (double)numOfSucces / cases.Count * 100;
                
                answerMark.Mark = mark;
                answerMark.ErrorMessage = string.Join("\n", errores);
                answerMark.IsSuccess = numOfSucces == cases.Count;
                return answerMark;
            }
            catch (Exception ex)
            {
                throw new Exception("Testing error", ex);
            }
            finally
            {
                try
                {
                    if (Directory.Exists(folderPath))
                    {
                        await Task.Delay(500);
                        Directory.Delete(folderPath, true);
                    }
                }
                catch (IOException)
                {   
                }
            }
        }

        public static bool CheckingSucces(string result, string expectedOutput)
        {
            return result.Trim() == expectedOutput.Trim();
        }
        public static string CleanOutput(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return "";

            var forbiddenKeywords = new[] { "Restore complete", "Build succeeded", "warning CS", ".csproj", "Determine projects" };

            var lines = raw.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var cleanLines = lines
                .Select(l => l.Trim())
                .Where(l => !forbiddenKeywords.Any(k => l.Contains(k)));

            return string.Join(Environment.NewLine, cleanLines).Trim();
        }
        public static string CleanPythonRuntimeError(string rawError)
        {
            if (string.IsNullOrWhiteSpace(rawError)) return "Unknown Error";

            // פיצול לשורות וניקוי רווחים
            var lines = rawError.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(l => l.Trim())
                                .ToList();

            if (lines.Count == 0) return "Unknown Error";

            // 1. חילוץ השורה האחרונה (סוג השגיאה והתיאור)
            // דוגמה: "IndexError: list index out of range"
            string finalError = lines.Last();

            // 2. חיפוש מספר השורה בתוך ה-Traceback באמצעות Regex
            // מחפש את התבנית "line" ואחריה מספר
            string lineInfo = "unknown line";
            var lineRegex = new System.Text.RegularExpressions.Regex(@"line (\d+)");

            // עוברים מהסוף להתחלה כדי למצוא את המיקום המדויק ביותר בקוד הסטודנט
            for (int i = lines.Count - 1; i >= 0; i--)
            {
                var match = lineRegex.Match(lines[i]);
                if (match.Success)
                {
                    lineInfo = $"Line {match.Groups[1].Value}";
                    break;
                }
            }

            // החזרת פורמט ידידותי: "סוג השגיאה: תיאור (Line X)"
            return $"{finalError} ({lineInfo})";
        }
        public static string CleanCompilationError(string rawDetails)
        {
            if (string.IsNullOrWhiteSpace(rawDetails)) return "";

            var lines = rawDetails.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var errorLines = lines
                .Where(l => l.Contains(": error "))
                .Select(l => {
                    int bracketIndex = l.LastIndexOf(" [");
                    string cleanLine = bracketIndex != -1 ? l.Substring(0, bracketIndex) : l;

                    int errorLabelIndex = cleanLine.IndexOf("error ");
                    if (errorLabelIndex != -1)
                    {
                        return cleanLine.Substring(errorLabelIndex).Trim();
                    }

                    return cleanLine.Trim();
                })
                .Distinct();

            return string.Join(Environment.NewLine, errorLines);
        }

        public async static Task<AnswerMarkDto> GetMark(StudentAnswer studentAnswer)
        {
            string folderPath = SaveCodeToFile(studentAnswer);
            ICollection<TestCase> cases = studentAnswer.Exercise.EdgeCases;
            return await Test(cases, folderPath, studentAnswer);
        }
    }
}
