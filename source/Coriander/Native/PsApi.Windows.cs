using System;

namespace Coriander.Native
{
    internal static partial class PsApi
    {
        public delegate bool EnumProcessModulesDelegate(IntPtr hProcess, IntPtr lphModule, int cb, ref int lpcbNeeded);
        public delegate bool EnumProcessModulesExDelegate(IntPtr hProcess, IntPtr lphModule, int cb, ref int lpcbNeeded, int dwFilterFlag);

        public static readonly EnumProcessModulesDelegate EnumProcessModules;
        public static readonly EnumProcessModulesExDelegate EnumProcessModulesEx;
    }
}
