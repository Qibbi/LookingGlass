using System;
using System.Runtime.InteropServices;

namespace Coriander.Native
{
    internal static class MsVcRt
    {
        public delegate IntPtr ClearMemoryDelegate(IntPtr ptr, byte value, ulong count);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int MemICmpDelegate([In] IntPtr buf1, [In] IntPtr buf2, SizeT size);
        [UnmanagedFunctionPointer(CallingConvention.FastCall)]
        public delegate SizeT StrLenDelegate([In] IntPtr str);

        private const string _moduleName = "msvcrt.dll";

        private static readonly IntPtr _hModule;

        private static readonly ClearMemoryDelegate ClearMemoryInt;

        public static readonly MemICmpDelegate MemICmp;

        static MsVcRt()
        {
            _hModule = NativeLibrary.Load(_moduleName);
            ClearMemoryInt = Marshal.GetDelegateForFunctionPointer<ClearMemoryDelegate>(NativeLibrary.GetExport(_hModule, "memset"));
            MemICmp = Marshal.GetDelegateForFunctionPointer<MemICmpDelegate>(NativeLibrary.GetExport(_hModule, "_memicmp"));
        }

        // [DllImport(_moduleName, EntryPoint = "memset", CallingConvention = CallingConvention.Cdecl)]
        // private static extern IntPtr ClearMemory(IntPtr ptr, byte value, ulong count);

        public static IntPtr ClearMemory(IntPtr ptr, byte value, int count)
        {
            return ClearMemoryInt(ptr, value, (ulong)count);
        }
    }
}
