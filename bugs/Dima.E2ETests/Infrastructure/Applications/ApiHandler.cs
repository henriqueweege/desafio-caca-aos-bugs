﻿using Dima.Core;

namespace Dima.E2ETests.Infrastructure.Applications;

internal sealed class ApiHandler
{
    private static ApplicationRunner applicationRunner = new();
    public static void RunApi()
    {
        Environment.SetEnvironmentVariable(Configuration.E2ETestEnv, "true");
        // applicationRunner.Run("dotnet run --project ../../../../Dima.Api/Dima.Api.csproj");
        applicationRunner.Run("../../../../Dima.Api/Dima.Api.csproj");
    }
}