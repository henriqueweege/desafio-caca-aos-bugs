
using System.Runtime.InteropServices;

namespace Dima.E2ETests.Infrastructure.Applications.Strategies;

internal interface IRunnerStrategy
{
    Task Run(string applicationPath);
    bool AppliesToSO();
}
