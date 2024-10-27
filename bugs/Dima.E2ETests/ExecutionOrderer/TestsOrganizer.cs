using Xunit.Abstractions;
using Xunit.Sdk;

namespace Dima.E2ETests.ExecutionOrderer;

public class TestsOrganizer : ITestCaseOrderer
{
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(
        IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
    {
        string assemblyName = typeof(OrderOfExecutionAttribute).AssemblyQualifiedName!;
        var sortedMethods = new Dictionary<int, TTestCase>();
        foreach (TTestCase testCase in testCases)
         {
            int priority = testCase.TestMethod.Method
                .GetCustomAttributes(assemblyName)
                .FirstOrDefault()
                ?.GetNamedArgument<int>(nameof(OrderOfExecutionAttribute.Priority)) ?? 0;

            sortedMethods.Add(priority, testCase);
        }

        foreach (TTestCase testCase in sortedMethods.OrderBy(x => x.Key).Select(priority => priority.Value))
        {
            yield return testCase;
        }
    }
}
