using System;

namespace Coriander.Native
{
    internal static partial class Kernel32
    {
        public delegate bool DuplicateHandleDelegate(IntPtr hSourceProcessHandle,
                                                     IntPtr hSourceHandle,
                                                     IntPtr hTargetProcessHandle,
                                                     ref IntPtr lpTargetHandle,
                                                     int dwDesiredAccess,
                                                     bool bInheritHandle,
                                                     int dwOptions);
        public delegate bool CloseHandleDelegate(IntPtr hObject);

        public static readonly DuplicateHandleDelegate DuplicateHandle;
        public static readonly CloseHandleDelegate CloseHandle;
    }
}
