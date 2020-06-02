using System;
using System.Runtime.InteropServices;

namespace Coriander.Native
{
    internal static partial class PsApi
    {
        private const string _moduleName = "psapi.dll";

        public static readonly IntPtr HModule;

        static PsApi()
        {
            HModule = NativeLibrary.Load(_moduleName);
            // Windows
            EnumProcessModules = Marshal.GetDelegateForFunctionPointer<EnumProcessModulesDelegate>(NativeLibrary.GetExport(HModule, nameof(EnumProcessModules)));
            EnumProcessModulesEx = Marshal.GetDelegateForFunctionPointer<EnumProcessModulesExDelegate>(NativeLibrary.GetExport(HModule, nameof(EnumProcessModulesEx)));
        }
    }
}
