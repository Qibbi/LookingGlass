using Coriander.Core.Diagnostics;
using Coriander.Native;
using System;

public static class CnC3EntryPoint
{
    public static void Run()
    {
        #region DEBUGCONSOLE
#if DEBUG
        IntPtr consoleWindow = IntPtr.Zero;
        if (Kernel32.AllocConsole())
        {
            consoleWindow = Kernel32.GetConsoleWindow();
            User32.SetLayeredWindowAttributes(consoleWindow, 0u, 225, 2);
        }
        User32.ShowWindow(consoleWindow, 5);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Title = "Coriander Debug Console";
        Tracer.TraceWrite += Log;
        Tracer.SetTraceLevel(6);
#endif
        #endregion
        Console.WriteLine(".Net Core active.");
        Console.ReadLine();
    }

    #region DEBUGCONSOLE
#if DEBUG
    private static void Log(string source, TraceEventType eventType, string message)
    {
        Console.Write(DateTime.Now.ToString("hh:mm:ss.fff "));
        ConsoleColor backupForeground;
        ConsoleColor backupBackground;
        switch (eventType)
        {
            case TraceEventType.CRITICAL:
            case TraceEventType.ERROR:
                backupForeground = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Black;
                backupBackground = Console.BackgroundColor;
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Write($"[{source}:{eventType}]");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = backupBackground;
                Console.WriteLine(" " + message);
                Console.ForegroundColor = backupForeground;
                break;
            case TraceEventType.WARNING:
                backupForeground = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Black;
                backupBackground = Console.BackgroundColor;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.Write($"[{source}:{eventType}]");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.BackgroundColor = backupBackground;
                Console.WriteLine(" " + message);
                Console.ForegroundColor = backupForeground;
                break;
            default:
                Console.WriteLine($"[{source}:{eventType}] {message}");
                break;
        }
    }
#endif
    #endregion
}
