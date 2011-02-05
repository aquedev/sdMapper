using System;
using System.Diagnostics;

namespace sdMapper.Extensions
{
    public static class ArgumentExtensions
    {
        [DebuggerHidden]
        public static void NotNull<T>(this T arg, string argumentName) where T : class
        {
            if (arg == null)
                throw new ArgumentException(argumentName);
        }

        [DebuggerHidden]
        public static void NotNull<T>(this Nullable<T> arg, string argumentName) where T : struct
        {
            if (arg.HasValue == false)
                throw new ArgumentException(argumentName);
        }

        [DebuggerHidden]
        public static void NotNullOrEmpty(this string arg, string argumentName) 
        {
            if (string.IsNullOrEmpty(arg))
                throw new ArgumentException(argumentName);
        }
    }
}
