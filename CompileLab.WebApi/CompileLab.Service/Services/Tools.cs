//using CompileLab.Repository.Entities;
//using CompileLab.Service.Dto;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CompileLab.Service.Services
//{
//    internal static class Tools
//    {
//        private static SemaphoreSlim _semaphore = new SemaphoreSlim(4);

//        public static void CleanupOldTests(string basePath)
//        {
//            try
//            {
//                if (!Directory.Exists(basePath)) return;
//                DateTime threshold = DateTime.Now.AddDays(-1);
//                var directories = Directory.GetDirectories(basePath);

//                foreach (var dir in directories)
//                {
//                    DirectoryInfo dirInfo = new DirectoryInfo(dir);

//                    if (dirInfo.LastWriteTime < threshold)
//                    {
//                        try
//                        {
//                            Directory.Delete(dir, true);
//                        }
//                        catch (Exception)
//                        {
                            
//                        }
//                    }
//                }
//            }
//            catch (UnauthorizedAccessException ex)
//            {
//                throw new UnauthorizedAccessException("No permission to delete folders", ex);
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("General cleanup error", ex);
//            }
//        }

//        public static string SaveCodeToFile(StudentAnswer studentAnswer)
//        {
//            string uniqueRunName = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + "_" + Guid.NewGuid().ToString().Substring(0, 4);
//            string studentDir = Path.Combine(Directory.GetCurrentDirectory(), "TempTests", uniqueRunName);

//            if (!Directory.Exists(studentDir)) Directory.CreateDirectory(studentDir);

//            string projectName = studentAnswer.Exercise.Language switch
//            {
//                ProgrammingLanguage.csharp => "Program.cs",
//                ProgrammingLanguage.python => "main.py",
//                _ => throw new NotSupportedException("Unsupported programming language")
//            };

//            string fullPath = Path.Combine(studentDir, projectName);
//            File.WriteAllText(fullPath, studentAnswer.AnswerCode);

//            return studentDir;
//        }
//        public static async Task<(bool Success, string Details)> CompileCodeAsync(string folderPath)
//        {
//            string csprojContent = @"<Project Sdk=""Microsoft.NET.Sdk"">
//                <PropertyGroup>
//                <OutputType>Exe</OutputType>
//                <TargetFramework>net9.0</TargetFramework>
//                <ImplicitUsings>enable</ImplicitUsings>
//                <Nullable>enable</Nullable>
//                <RuntimeIdentifier>linux-x64</RuntimeIdentifier>
//              </PropertyGroup>
//            </Project>";

//            await File.WriteAllTextAsync(Path.Combine(folderPath, "Project.csproj"), csprojContent);

//            var psi = new ProcessStartInfo
//            {
//                FileName = "dotnet",
//                Arguments = $"publish \"{Path.Combine(folderPath, "Project.csproj")}\" -c Release -r linux-x64 --self-contained false -o \"{Path.Combine(folderPath, "publish")}\" /nologo",
//                RedirectStandardOutput = true,
//                RedirectStandardError = true,
//                UseShellExecute = false,
//                CreateNoWindow = true
//            };

//            using var process = Process.Start(psi);
//            string output = await process.StandardOutput.ReadToEndAsync();
//            string error = await process.StandardError.ReadToEndAsync();
//            process.WaitForExit();

//            if (File.Exists(Path.Combine(folderPath, "publish", "Project.dll")))
//            {
//                return (true, "Success");
//            }

//            return (false, output + error);
//        }

//        public static async Task<(bool Success, string Output)> ExecuteTestAsync(string folderPath, StudentAnswer studentAnswer, params string[] inputs)
//        {
//            ProgrammingLanguage pl = studentAnswer.Exercise.Language;
//            await _semaphore.WaitAsync();
//            string testerImage = pl switch
//            {
//                ProgrammingLanguage.csharp => "tester-csharp:1.0",
//                ProgrammingLanguage.python => "tester-python:1.0",
//                _ => throw new NotSupportedException("Unsupported programming language")
//            };

//            string mountSource = pl == ProgrammingLanguage.csharp
//                ? Path.Combine(folderPath, "publish")
//                : folderPath;

//            string runCommand = pl == ProgrammingLanguage.csharp
//                ? "dotnet /app/Project.dll"
//                : "python /app/main.py";

//            try
//            {
//                string ramLimit = "--memory=128m";
//                string cpuLimit = "--cpus=0.3";
//                string network = "--network none";
//                string readOnly = "--read-only";
//                string tempFs = "--tmpfs /tmp:exec,mode=1777";
//                string user = "--user 1000:1000";
//                string runId = Guid.NewGuid().ToString().Substring(0, 8);

//                var psi = new ProcessStartInfo
//                {
//                    FileName = "docker",

//                    Arguments = $"run --rm --init --name test_{runId} -i " +
//                    $"{ramLimit} {cpuLimit} {network} {readOnly} {tempFs} {user} " +
//                    $"-v \"{mountSource}:/app:ro\" " +
//                    $"{testerImage} " +
//                    $"{runCommand}",
//                    RedirectStandardInput = true,
//                    RedirectStandardOutput = true,
//                    RedirectStandardError = true,
//                    UseShellExecute = false,
//                    CreateNoWindow = true
//                };

//                using var process = new Process { StartInfo = psi };
//                process.Start();

//                // הזרקת קלטים (Inputs)
//                using (var sw = process.StandardInput)
//                {
//                    foreach (var line in inputs)
//                        await sw.WriteLineAsync(line);
//                }

//                var outputTask = process.StandardOutput.ReadToEndAsync();
//                var errorTask = process.StandardError.ReadToEndAsync();

//                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10)); // טיימר חיצוני של 10 שניות לכל התהליך
//                try
//                {
//                    await process.WaitForExitAsync(cts.Token);
//                }
//                catch (OperationCanceledException)
//                {
//                    if (!process.HasExited) process.Kill(true);
//                    return new TestResult { Success = false, Error = "Time Limit Exceeded" };
//                }
//                string rawOutput = await outputTask;
//                string rawError = await errorTask;
//                string fullLog = rawOutput + rawError;

//                if (process.ExitCode == 0)
//                {
//                    return new TestResult
//                    {
//                        Success = true,
//                        Output = CleanOutput(rawOutput),
//                        RawDetails = fullLog
//                    };
//                }

//                return new TestResult
//                {
//                    Success = false,
//                    Error = "Runtime Error",
//                    Output = CleanOutput(rawOutput),
//                    RawDetails = fullLog
//                };
//            }
//            finally
//            {
//                _semaphore.Release(); // משחרר את המשבצת לסטודנט הבא
//            }

//        }
//        public static async Task<AnswerMarkDto> Test(List<TestCase> cases, string folderPath, StudentAnswer studentAnswer)
//        {
//            try
//            {
//                AnswerMarkDto answerMark = new AnswerMarkDto { IsSuccess = false, Mark = 0, Remark = null };
//                if (studentAnswer.Exercise.Language == ProgrammingLanguage.csharp)
//                {
//                    var (Success, Details) = await CompileCodeAsync(folderPath);

//                    if (!Success)
//                    {

//                        answerMark.TypeError = "CompilationError";
//                        answerMark.ErrorMessage = CleanCompilationError(Details);
//                        return answerMark;
//                    }
//                }

//                List<string> errores = new List<string>();
//                int numOfSucces = 0;

//                foreach (TestCase testCase in cases)
//                {
//                    var (Success, Output)= await Tools.ExecuteTestAsync(folderPath, pl, [.. edge.Inputs]);

//                    if (Tools.CheckingSucces(result, edge.Output))
//                    {
//                        numOfSucces++;
//                    }
//                    else if (result.Success)
//                    {
//                        errores.Add($"the expected output was: {edge.Output} the real output was: {result.Output}");
//                    }
//                    else
//                    {
//                        errores.Add($"{result.Error}: {result.RawDetails}");
//                    }
//                }

//                Console.WriteLine($"Mark: {numOfSucces}/{edges.Count}");
//                if (errores.Count > 0)
//                {
//                    Console.WriteLine("Your Errors:");
//                    foreach (string str in errores) Console.WriteLine(str);
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
//            }
//            finally
//            {
//                try
//                {
//                    if (Directory.Exists(folderPath))
//                    {
//                        await Task.Delay(500);
//                        Directory.Delete(folderPath, true);
//                        Console.WriteLine($"--- Cleanup: Folder {folderPath} deleted ---");
//                    }
//                }
//                catch (IOException ex)
//                {
//                    Console.WriteLine($"Cleanup warning: Could not delete folder immediately. {ex.Message}");
//                }
//            }
//        }
//    }
//}
