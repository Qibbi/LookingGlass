using System;
using System.Collections.Generic;
using System.Reflection;

namespace Coriander.Core.Diagnostics
{
    public enum TraceEventType
    {
        CRITICAL,
        ERROR,
        WARNING,
        INFORMATION,
        VERBOSE,
        START,
        STOP,
        SUSPEND,
        RESUME,
        TRANSFER
    }

    public enum TraceKindBitFlagsType
    {
        None = -1,
        Exception,
        Assert,
        Error,
        Warning,
        Message,
        Info,
        Note,
        Method,
        Scope,
        Constructor,
        Property,
        Data,
        ALL
    }

    public class TraceKindBitFlags : BitFlags<TraceKindBitFlagsType>
    {
    }

    public delegate void TraceWriteDelegate(string source, TraceEventType eventType, string message);

    public class Tracer
    {
        private class InternalScopedTrace : IDisposable
        {
            private readonly string _text;

            private Tracer _tracer;

            public InternalScopedTrace(Tracer tracer, string text)
            {
                _tracer = tracer;
                _text = text;
                _tracer.Output(TraceEventType.VERBOSE, $"Entering {_text}");
                ++_indentLevel;
            }

            public void Dispose()
            {
                --_indentLevel;
                _tracer.Output(TraceEventType.VERBOSE, $"Leaving {_text}");
            }
        }

        public static readonly TraceKindBitFlags[] VerbosityLevels;

        private static Dictionary<string, Tracer> _tracers;
        [ThreadStatic] private static int _indentLevel;
        private static TraceKindBitFlags _traceMask;

        public static TraceWriteDelegate TraceWrite;

        private readonly string _name;
        private readonly string _description;

        public bool IsEnabled { get; set; }

        static Tracer()
        {
            _tracers = new Dictionary<string, Tracer>(StringComparer.OrdinalIgnoreCase);
            VerbosityLevels = new TraceKindBitFlags[]
            {
                new TraceKindBitFlags { TraceKindBitFlagsType.None },
                new TraceKindBitFlags { TraceKindBitFlagsType.Exception },
                new TraceKindBitFlags { TraceKindBitFlagsType.Exception, TraceKindBitFlagsType.Assert },
                new TraceKindBitFlags { TraceKindBitFlagsType.Exception, TraceKindBitFlagsType.Assert, TraceKindBitFlagsType.Error },
                new TraceKindBitFlags { TraceKindBitFlagsType.Exception, TraceKindBitFlagsType.Assert, TraceKindBitFlagsType.Error, TraceKindBitFlagsType.Warning },
                new TraceKindBitFlags { TraceKindBitFlagsType.Exception, TraceKindBitFlagsType.Assert, TraceKindBitFlagsType.Error, TraceKindBitFlagsType.Warning, TraceKindBitFlagsType.Message },
                new TraceKindBitFlags { TraceKindBitFlagsType.Exception, TraceKindBitFlagsType.Assert, TraceKindBitFlagsType.Error, TraceKindBitFlagsType.Warning, TraceKindBitFlagsType.Message, TraceKindBitFlagsType.Info },
                new TraceKindBitFlags { TraceKindBitFlagsType.Exception, TraceKindBitFlagsType.Assert, TraceKindBitFlagsType.Error, TraceKindBitFlagsType.Warning, TraceKindBitFlagsType.Message, TraceKindBitFlagsType.Info, TraceKindBitFlagsType.Note },
                new TraceKindBitFlags { TraceKindBitFlagsType.Exception, TraceKindBitFlagsType.Assert, TraceKindBitFlagsType.Error, TraceKindBitFlagsType.Warning, TraceKindBitFlagsType.Message, TraceKindBitFlagsType.Info, TraceKindBitFlagsType.Note, TraceKindBitFlagsType.Method },
                new TraceKindBitFlags { TraceKindBitFlagsType.Exception, TraceKindBitFlagsType.Assert, TraceKindBitFlagsType.Error, TraceKindBitFlagsType.Warning, TraceKindBitFlagsType.Message, TraceKindBitFlagsType.Info, TraceKindBitFlagsType.Note, TraceKindBitFlagsType.Method, TraceKindBitFlagsType.Scope },
                new TraceKindBitFlags { TraceKindBitFlagsType.Exception, TraceKindBitFlagsType.Assert, TraceKindBitFlagsType.Error, TraceKindBitFlagsType.Warning, TraceKindBitFlagsType.Message, TraceKindBitFlagsType.Info, TraceKindBitFlagsType.Note, TraceKindBitFlagsType.Method, TraceKindBitFlagsType.Scope, TraceKindBitFlagsType.Constructor },
                new TraceKindBitFlags { TraceKindBitFlagsType.ALL }
            };
            _traceMask = VerbosityLevels[3];
        }

        private Tracer(string name, string description)
        {
            _name = name;
            _description = description;
            IsEnabled = true;
        }

        private static void OnWrite(string source, TraceEventType eventType, string message)
        {
            TraceWrite?.Invoke(source, eventType, message);
        }

        private static string GetCallingMethodInfo(string additionalInfo)
        {
            MethodBase method = new System.Diagnostics.StackFrame(2).GetMethod();
            string result = string.Empty;
            if (method is MethodInfo methodInfo)
            {
                result = $"{methodInfo.ReturnType.FullName} ";
            }
            string parameters = string.Empty;
            ParameterInfo[] parameterInfos = method.GetParameters();
            if (parameterInfos.Length > 0)
            {
                parameters = string.Join<ParameterInfo>(", ", parameterInfos);
            }
            result += $"{method.DeclaringType.FullName}.{method.Name}({parameters})";
            if (!string.IsNullOrEmpty(additionalInfo))
            {
                result += $" [{additionalInfo}]";
            }
            return result;
        }

        public static Tracer GetTracer(string name, string description)
        {
            if (!_tracers.TryGetValue(name, out Tracer result))
            {
                result = new Tracer(name, description);
                _tracers.Add(name, result);
            }
            return result;
        }

        public static void SetTraceLevel(int level)
        {
            _traceMask = VerbosityLevels[level];
        }

        private void Output(TraceEventType eventType, string message)
        {
            if (IsEnabled)
            {
                OnWrite(_name, eventType, message.PadLeft(_indentLevel << 1));
            }
        }

        public void TraceData(string message)
        {
            if (_traceMask.HasFlag(TraceKindBitFlagsType.Data))
            {
                Output(TraceEventType.VERBOSE, message);
            }
        }

        public IDisposable TraceMethod()
        {
            if (_traceMask.HasFlag(TraceKindBitFlagsType.Method))
            {
                return new InternalScopedTrace(this, GetCallingMethodInfo(string.Empty));
            }
            return null;
        }

        public IDisposable TraceMethod(string additionalInfo)
        {
            if (_traceMask.HasFlag(TraceKindBitFlagsType.Method))
            {
                return new InternalScopedTrace(this, GetCallingMethodInfo(additionalInfo));
            }
            return null;
        }

        public void TraceNote(string message)
        {
            if (_traceMask.HasFlag(TraceKindBitFlagsType.Note))
            {
                Output(TraceEventType.VERBOSE, message);
            }
        }

        public void TraceInfo(string message)
        {
            if (_traceMask.HasFlag(TraceKindBitFlagsType.Info))
            {
                Output(TraceEventType.INFORMATION, message);
            }
        }

        public void TraceMessage(string message)
        {
            if (_traceMask.HasFlag(TraceKindBitFlagsType.Message))
            {
                Output(TraceEventType.INFORMATION, message);
            }
        }

        public void TraceWarning(string message)
        {
            if (_traceMask.HasFlag(TraceKindBitFlagsType.Warning))
            {
                Output(TraceEventType.WARNING, message);
            }
        }

        public void TraceError(string message)
        {
            if (_traceMask.HasFlag(TraceKindBitFlagsType.Error))
            {
                Output(TraceEventType.ERROR, message);
            }
        }

        public void TraceAssert(string message)
        {
            if (_traceMask.HasFlag(TraceKindBitFlagsType.Assert))
            {
                Output(TraceEventType.CRITICAL, message);
            }
        }

        public void TraceException(string message)
        {
            if (_traceMask.HasFlag(TraceKindBitFlagsType.Exception))
            {
                Output(TraceEventType.CRITICAL, message);
            }
        }

        public void TraceException(Exception ex)
        {
            TraceException(ex.Message);
        }
    }
}
