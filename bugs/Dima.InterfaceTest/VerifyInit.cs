﻿using System.Runtime.CompilerServices;
using AngleSharp.Diffing;

namespace Dima.InterfaceTest;

public static class VerifyInit
{
    [ModuleInitializer]
    public static void InitPlaywright()
    {
        ClipboardAccept.Enable();
        VerifyAngleSharpDiffing.Initialize(options =>
        {
            options.AddDefaultOptions();
        });
        VerifyPlaywright.Initialize();
        VerifyBunit.Initialize(excludeComponent: true);
    }
}

