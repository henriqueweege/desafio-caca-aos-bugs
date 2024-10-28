
using System.Runtime.InteropServices;

namespace Dima.E2ETests.Infrastructure.Applications.Strategies;

internal interface IApplicationRunner
{
    Task Run(string applicationPath);
    bool AppliesToSO();
}
