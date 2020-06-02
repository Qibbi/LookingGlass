using System;
using System.Runtime.InteropServices;

namespace Coriander.Native
{
    internal static partial class Kernel32
    {
        [Flags]
        public enum SecurityDescriptorControlType : ushort
        {
            /// <summary>
            /// Indicated that the SID of the owner of the security descriptor was provided by a default mechanism.
            /// This flag can be used by a resource manager to identify objects whose owner was set by a default mechanism.
            /// To set this flag, use the SetSecurityDescriptorOwner function.
            /// </summary>
            OwnerDefaulted = 1 << 0,
            /// <summary>
            /// Indicated that the SID of the security descriptor group was provided by a default mechanism.
            /// This flag can be used by a resource manager to identify objects whose security descriptor group was set by a default mechanism.
            /// To set this flag, use the SetSecurityDescriptorOwner function.
            /// </summary>
            GroupDefaulted = 1 << 1,
            DACLPresent = 1 << 2,
            DACLDefaulted = 1 << 3,
            SACLPresent = 1 << 4,
            SACLDefaulted = 1 << 5,
            DACLInheritReq = 1 << 8,
            SACLInheritReq = 1 << 9,
            DACLAutoInherited = 1 << 10,
            SACLAutoInherited = 1 << 11,
            DACLProtected = 1 << 12,
            SACLProtected = 1 << 13,
            RMControlValid = 1 << 14,
            SelfRelative = 1 << 15
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SecurityDescriptor
        {
            public byte Revision;
            public byte Sbz1;
            public SecurityDescriptorControlType Control;
            public IntPtr Owner;
            public IntPtr Group;
            public IntPtr Sacl;
            public IntPtr Dacl;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SecurityAttributes
        {
            public int NLength;
            public IntPtr LpSecurityDescriptor;
            [MarshalAs(UnmanagedType.Bool)] public bool BInheritHandle;
        }


        private const string _moduleName = "kernel32.dll";

        public static readonly IntPtr HModule;

        static Kernel32()
        {
            HModule = NativeLibrary.Load(_moduleName);
            // ProcessEnv
            GetCommandLineA = Marshal.GetDelegateForFunctionPointer<GetCommandLineADelegate>(NativeLibrary.GetExport(HModule, nameof(GetCommandLineA)));
            GetCommandLineW = Marshal.GetDelegateForFunctionPointer<GetCommandLineWDelegate>(NativeLibrary.GetExport(HModule, nameof(GetCommandLineW)));
            // LibLoaderApi
            LoadLibraryA = Marshal.GetDelegateForFunctionPointer<LoadLibraryADelegate>(NativeLibrary.GetExport(HModule, nameof(LoadLibraryA)));
            GetProcAddress = Marshal.GetDelegateForFunctionPointer<GetProcAddressDelegate>(NativeLibrary.GetExport(HModule, nameof(GetProcAddress)));
            FreeLibrary = Marshal.GetDelegateForFunctionPointer<FreeLibraryDelegate>(NativeLibrary.GetExport(HModule, nameof(FreeLibrary)));
            GetModuleFileNameW = Marshal.GetDelegateForFunctionPointer<GetModuleFileNameWDelegate>(NativeLibrary.GetExport(HModule, nameof(GetModuleFileNameW)));
            // ProcessThreadsApi
            GetCurrentProcess = Marshal.GetDelegateForFunctionPointer<GetCurrentProcessDelegate>(NativeLibrary.GetExport(HModule, nameof(GetCurrentProcess)));
            CreateProcessA = Marshal.GetDelegateForFunctionPointer<CreateProcessADelegate>(NativeLibrary.GetExport(HModule, nameof(CreateProcessA)));
            CreateProcessW = Marshal.GetDelegateForFunctionPointer<CreateProcessWDelegate>(NativeLibrary.GetExport(HModule, nameof(CreateProcessW)));
            TerminateProcess = Marshal.GetDelegateForFunctionPointer<TerminateProcessDelegate>(NativeLibrary.GetExport(HModule, nameof(TerminateProcess)));
            GetCurrentThreadId = Marshal.GetDelegateForFunctionPointer<GetCurrentThreadIdDelegate>(NativeLibrary.GetExport(HModule, nameof(GetCurrentThreadId)));
            CreateThread = Marshal.GetDelegateForFunctionPointer<CreateThreadDelegate>(NativeLibrary.GetExport(HModule, nameof(CreateThread)));
            OpenThread = Marshal.GetDelegateForFunctionPointer<OpenThreadDelegate>(NativeLibrary.GetExport(HModule, nameof(OpenThread)));
            SuspendThread = Marshal.GetDelegateForFunctionPointer<SuspendThreadDelegate>(NativeLibrary.GetExport(HModule, nameof(SuspendThread)));
            ResumeThread = Marshal.GetDelegateForFunctionPointer<ResumeThreadDelegate>(NativeLibrary.GetExport(HModule, nameof(ResumeThread)));
            // SynchApi
            CreateEventA = Marshal.GetDelegateForFunctionPointer<CreateEventADelegate>(NativeLibrary.GetExport(HModule, nameof(CreateEventA)));
            OpenEventA = Marshal.GetDelegateForFunctionPointer<OpenEventADelegate>(NativeLibrary.GetExport(HModule, nameof(OpenEventA)));
            SetEvent = Marshal.GetDelegateForFunctionPointer<SetEventDelegate>(NativeLibrary.GetExport(HModule, nameof(SetEvent)));
            ResetEvent = Marshal.GetDelegateForFunctionPointer<ResetEventDelegate>(NativeLibrary.GetExport(HModule, nameof(ResetEvent)));
            CreateMutexA = Marshal.GetDelegateForFunctionPointer<CreateMutexADelegate>(NativeLibrary.GetExport(HModule, nameof(CreateMutexA)));
            CreateMutexW = Marshal.GetDelegateForFunctionPointer<CreateMutexWDelegate>(NativeLibrary.GetExport(HModule, nameof(CreateMutexW)));
            WaitForSingleObject = Marshal.GetDelegateForFunctionPointer<WaitForSingleObjectDelegate>(NativeLibrary.GetExport(HModule, nameof(WaitForSingleObject)));
            // MemoryApi
            VirtualProtect = Marshal.GetDelegateForFunctionPointer<VirtualProtectDelegate>(NativeLibrary.GetExport(HModule, nameof(VirtualProtect)));
            // HandleApi
            DuplicateHandle = Marshal.GetDelegateForFunctionPointer<DuplicateHandleDelegate>(NativeLibrary.GetExport(HModule, nameof(DuplicateHandle)));
            CloseHandle = Marshal.GetDelegateForFunctionPointer<CloseHandleDelegate>(NativeLibrary.GetExport(HModule, nameof(CloseHandle)));
            // NamedPipeApi
            CreatePipe = Marshal.GetDelegateForFunctionPointer<CreatePipeDelegate>(NativeLibrary.GetExport(HModule, nameof(CreatePipe)));
            // FileApi
            CreateFileA = Marshal.GetDelegateForFunctionPointer<CreateFileADelegate>(NativeLibrary.GetExport(HModule, nameof(CreateFileA)));
            CreateFileW = Marshal.GetDelegateForFunctionPointer<CreateFileWDelegate>(NativeLibrary.GetExport(HModule, nameof(CreateFileW)));
            FindFirstFileA = Marshal.GetDelegateForFunctionPointer<FindFirstFileADelegate>(NativeLibrary.GetExport(HModule, nameof(FindFirstFileA)));
            FindFirstFileW = Marshal.GetDelegateForFunctionPointer<FindFirstFileWDelegate>(NativeLibrary.GetExport(HModule, nameof(FindFirstFileW)));
            FindClose = Marshal.GetDelegateForFunctionPointer<FindCloseDelegate>(NativeLibrary.GetExport(HModule, nameof(FindClose)));
            // ErrHandlingApi
            SetErrorMode = Marshal.GetDelegateForFunctionPointer<SetErrorModeDelegate>(NativeLibrary.GetExport(HModule, nameof(SetErrorMode)));
            GetLastError = Marshal.GetDelegateForFunctionPointer<GetLastErrorDelegate>(NativeLibrary.GetExport(HModule, nameof(GetLastError)));
            // WinBase
            LStrLenA = Marshal.GetDelegateForFunctionPointer<LStrLenADelegate>(NativeLibrary.GetExport(HModule, "lstrlenA"));
            LStrLen2A = Marshal.GetDelegateForFunctionPointer<LStrLen2ADelegate>(NativeLibrary.GetExport(HModule, "lstrlenA"));
            SetCurrentDirectoryW = Marshal.GetDelegateForFunctionPointer<SetCurrentDirectoryWDelegate>(NativeLibrary.GetExport(HModule, nameof(SetCurrentDirectoryW)));
            GetCurrentDirectoryW = Marshal.GetDelegateForFunctionPointer<GetCurrentDirectoryWDelegate>(NativeLibrary.GetExport(HModule, nameof(GetCurrentDirectoryW)));
            // Windows
            AllocConsole = Marshal.GetDelegateForFunctionPointer<AllocConsoleDelegate>(NativeLibrary.GetExport(HModule, nameof(AllocConsole)));
            GetConsoleWindow = Marshal.GetDelegateForFunctionPointer<GetConsoleWindowDelegate>(NativeLibrary.GetExport(HModule, nameof(GetConsoleWindow)));
        }

        [DllImport(_moduleName, EntryPoint = "LoadLibraryA")] internal static extern IntPtr Load(string lpLibFileName);
        [DllImport(_moduleName, EntryPoint = "GetProcAddress")] internal static extern IntPtr GetExport(IntPtr hModule, string lpProcName);
        [DllImport(_moduleName, EntryPoint = "FreeLibrary")] internal static extern bool Close(IntPtr hLibModule);
    }
}
