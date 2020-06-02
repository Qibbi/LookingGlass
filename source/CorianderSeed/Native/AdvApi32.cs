using System;
using System.Runtime.InteropServices;

namespace Coriander.Native
{
    internal static partial class AdvApi32
    {

        private const string _moduleName = "advapi32.dll";

        public static readonly IntPtr HModule;

        static AdvApi32()
        {
            HModule = NativeLibrary.Load(_moduleName);
            // WinReg
            RegCreateKeyExA = Marshal.GetDelegateForFunctionPointer<RegCreateKeyExADelegate>(NativeLibrary.GetExport(HModule, nameof(RegCreateKeyExA)));
            RegOpenKeyExA = Marshal.GetDelegateForFunctionPointer<RegOpenKeyExADelegate>(NativeLibrary.GetExport(HModule, nameof(RegOpenKeyExA)));
            RegOpenKeyExW = Marshal.GetDelegateForFunctionPointer<RegOpenKeyExWDelegate>(NativeLibrary.GetExport(HModule, nameof(RegOpenKeyExW)));
            RegQueryValueExA = Marshal.GetDelegateForFunctionPointer<RegQueryValueExADelegate>(NativeLibrary.GetExport(HModule, nameof(RegQueryValueExA)));
            RegQueryValueExW = Marshal.GetDelegateForFunctionPointer<RegQueryValueExWDelegate>(NativeLibrary.GetExport(HModule, nameof(RegQueryValueExW)));
            RegSetValueExA = Marshal.GetDelegateForFunctionPointer<RegSetValueExADelegate>(NativeLibrary.GetExport(HModule, nameof(RegSetValueExA)));
            RegCloseKey = Marshal.GetDelegateForFunctionPointer<RegCloseKeyDelegate>(NativeLibrary.GetExport(HModule, nameof(RegCloseKey)));
        }
    }
}
