using System;

namespace Dima.E2ETests.ExecutionOrderer;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class OrderOfExecutionAttribute : Attribute
{
    public int Priority { get; private set; }

    public OrderOfExecutionAttribute(int priority) 
        => Priority = priority;
}
