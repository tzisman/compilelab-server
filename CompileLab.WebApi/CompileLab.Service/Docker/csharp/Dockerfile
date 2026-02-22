FROM mcr.microsoft.com/dotnet/runtime:9.0

WORKDIR /app

ENV DOTNET_CLI_HOME=/tmp \
    NUGET_PACKAGES=/tmp/.nuget \
    DOTNET_SKIP_WORKLOAD_INTEGRITY_CHECK=1 \
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true \
    DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    DOTNET_NOLOGO=true

RUN chown -R 1000:1000 /app

USER 1000

CMD ["dotnet", "StudentSolution.dll"]