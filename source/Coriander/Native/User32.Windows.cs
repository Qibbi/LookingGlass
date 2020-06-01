using System;
using System.Runtime.InteropServices;

namespace Coriander.Native
{
    internal static partial class User32
    {
        public delegate bool IsWindowUnicodeDelegate(IntPtr hWnd);
        public delegate IntPtr DefWindowProcADelegate(IntPtr hWnd, uint msg, UIntPtr wParam, IntPtr lParam);
        public delegate IntPtr DefWindowProcWDelegate(IntPtr hWnd, uint msg, UIntPtr wParam, IntPtr lParam);
        public delegate bool ShowWindowDelegate(IntPtr hWnd, int nCmdShow);
        public delegate bool SetLayeredWindowAttributesDelegate(IntPtr hWnd, uint crKey, byte bAlpha, int dwFlags);
        public delegate uint RegisterWindowMessageADelegate([In, MarshalAs(UnmanagedType.LPStr)] string lpString);
        public delegate bool GetWindowRectDelegate(IntPtr hWnd, out Rect lpRect);
        public delegate bool PostMessageADelegate(IntPtr hWnd, uint msg, UIntPtr wParam, IntPtr lParam);
        public delegate void PostQuitMessageDelegate(int nExitCode);
        public delegate bool ClipCursorDelegate([In] IntPtr lpRect);

        public static readonly IsWindowUnicodeDelegate IsWindowUnicode;
        public static readonly DefWindowProcADelegate DefWindowProcA;
        public static readonly DefWindowProcWDelegate DefWindowProcW;
        public static readonly ShowWindowDelegate ShowWindow;
        public static readonly SetLayeredWindowAttributesDelegate SetLayeredWindowAttributes;
        public static readonly RegisterWindowMessageADelegate RegisterWindowMessageA;
        public static readonly GetWindowRectDelegate GetWindowRect;
        public static readonly PostMessageADelegate PostMessageA;
        public static readonly PostQuitMessageDelegate PostQuitMessage;
        public static readonly ClipCursorDelegate ClipCursor;
    }
}
