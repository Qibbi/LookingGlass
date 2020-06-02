using System;

namespace Coriander.Native
{
    internal static partial class Kernel32
    {
        [Flags]
        internal enum SetErrorModeTypes : uint
        {
            Default,
            /// <summary>
            /// The system does not display the critical-error-handler message box. Instead, the system sends the error to the calling process.
            /// Best practice is that all applications call the process-wide <see cref="Kernel32.SetErrorMode(uint)" /> function with a parameter of <see cref="FailCriticalErrors" /> at startup.
            /// This is to prevent error mode dialogs from hanging the application.
            /// </summary>
            FailCriticalErrors = 1 << 0,
            /// <summary>
            /// The system does not display the Windows Error Reporting dialog.
            /// </summary>
            NoGPFaultErrorBox = 1 << 1,
            /// <summary>
            /// The system automatically fixes memory alignment faults and makes them invisible to the application. It does this for the calling process and any descendant processes.
            /// This feature is only supported by certain processor architectures.
            /// After this value is set for a process, subsequent attempts to clear the value are ignored.
            /// </summary>
            NoAlignmentFaultException = 1 << 2,
            /// <summary>
            /// The OpenFile function does not display a message box when it fails to find a file.
            /// Instead, the error is returned to the caller. This error mode overrides the OF_PROMPT flag.
            /// </summary>
            NoOpenFileErrorBox = 1 << 15
        }

        public delegate uint SetErrorModeDelegate(SetErrorModeTypes uMode);
        public delegate int GetLastErrorDelegate();

        public static readonly SetErrorModeDelegate SetErrorMode;
        public static readonly GetLastErrorDelegate GetLastError;
    }
}
