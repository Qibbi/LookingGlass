using System;
using System.Runtime.InteropServices;

namespace Coriander.Native
{
    internal static partial class User32
    {
        private const string _moduleName = "user32.dll";

        public static readonly IntPtr HModule;

        static User32()
        {
            HModule = NativeLibrary.Load(_moduleName);
            // Windows
            IsWindowUnicode = Marshal.GetDelegateForFunctionPointer<IsWindowUnicodeDelegate>(NativeLibrary.GetExport(HModule, nameof(IsWindowUnicode)));
            DefWindowProcA = Marshal.GetDelegateForFunctionPointer<DefWindowProcADelegate>(NativeLibrary.GetExport(HModule, nameof(DefWindowProcA)));
            DefWindowProcW = Marshal.GetDelegateForFunctionPointer<DefWindowProcWDelegate>(NativeLibrary.GetExport(HModule, nameof(DefWindowProcW)));
            ShowWindow = Marshal.GetDelegateForFunctionPointer<ShowWindowDelegate>(NativeLibrary.GetExport(HModule, nameof(ShowWindow)));
            SetLayeredWindowAttributes = Marshal.GetDelegateForFunctionPointer<SetLayeredWindowAttributesDelegate>(NativeLibrary.GetExport(HModule, nameof(SetLayeredWindowAttributes)));
            RegisterWindowMessageA = Marshal.GetDelegateForFunctionPointer<RegisterWindowMessageADelegate>(NativeLibrary.GetExport(HModule, nameof(RegisterWindowMessageA)));
            GetWindowRect = Marshal.GetDelegateForFunctionPointer<GetWindowRectDelegate>(NativeLibrary.GetExport(HModule, nameof(GetWindowRect)));
            PostMessageA = Marshal.GetDelegateForFunctionPointer<PostMessageADelegate>(NativeLibrary.GetExport(HModule, nameof(PostMessageA)));
            PostQuitMessage = Marshal.GetDelegateForFunctionPointer<PostQuitMessageDelegate>(NativeLibrary.GetExport(HModule, nameof(PostQuitMessage)));
            ClipCursor = Marshal.GetDelegateForFunctionPointer<ClipCursorDelegate>(NativeLibrary.GetExport(HModule, nameof(ClipCursor)));
        }
    }
}
