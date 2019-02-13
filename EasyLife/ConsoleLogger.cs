using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyLife
{
    public static class ConsoleLoggerExtensions
    {
        public static void ConsoleLog(this object obj)
        {
            ConsoleLogger.Log(obj);
        }

        public static void ConsoleLogLine(this object obj)
        {
            ConsoleLogger.LogLine(obj);
        }

        public static string EFormat(this object obj)
        {
            return ConsoleLogger.EFormat(obj);
        }

        public static string EFormat(this string format, params object[] args)
        {
            return ConsoleLogger.EFormat(format, args);
        }

    }

    public class ConsoleLogger
    {
#if DEBUG
        public static bool Enabled = true;
#else
        public static bool Enabled = false;
#endif

        public static void LogLine(string format, params object[] args)
        {
            if (Enabled)
            {
                args = ConvertArgs(args);
                Console.WriteLine(string.Format(format, args));
            }
        }

        public static void LogLine(object arg)
        {
            if (Enabled) Console.WriteLine(ConvertArg(arg));
        }

        public static void LogLine()
        {
            if (Enabled) Console.WriteLine();
        }

        public static void Log(string format, params object[] args)
        {
            if (Enabled) Console.Write(EFormat(format, args));
        }

        public static void Log(object arg)
        {
            if (Enabled) Console.Write(ConvertArg(arg).ToString());
        }

        public static string EFormat(object arg)
        {
            return ConvertArg(arg).ToString();
        }

        public static string EFormat(string format, params object[] args)
        {
            args = ConvertArgs(args);
            return string.Format(format, args);
        }

        public static object[] ConvertArgs(object[] args)
        {
            if (!args.Any(a => (a is Func<string>) 
                                || (a is System.Collections.IDictionary)
                                || (a is System.Collections.IEnumerable && !(a is string))
                         )) return args;
            var newArgs = new List<object>(args.Length);
            foreach(var a in args)
            {
                object newArg = ConvertArg(a);
                newArgs.Add(newArg);
            }
            return newArgs.ToArray();
        }

        internal static object ConvertArg(object a)
        {
            if (a is Func<string>) a = ConvertLambdaArgToString(a);
            if (a is System.Collections.IDictionary) a = ConvertDictionaryArgToString(a);
            if (a is System.Collections.IEnumerable && !(a is string)) a = ConvertEnumerableArgToString(a);
            return a;
        }

        private static object ConvertLambdaArgToString(object a)
        {
            string strValue = ((Func<string>)a)();
            return strValue;
        }

        private static object ConvertDictionaryArgToString(object a)
        {
            var dict = (System.Collections.IDictionary)a;
            if (dict ==null || dict.Count == 0) return "{}";
            var strValues = new List<string>(dict.Count);
            foreach (var o in dict.Keys)
            {
                strValues.Add(string.Format("{{{0}:{1}}}",ConvertArg(o).ToString(),ConvertArg(dict[o]).ToString()));
            }
            object newArg = string.Format("{{{0}}}", string.Join(",", strValues));
            return newArg;
        }

        private static object ConvertEnumerableArgToString(object a)
        {
            var enumerable = a as System.Collections.IEnumerable;
            if (enumerable == null) return "[]";
            var strValues = new List<string>();

            foreach (var o in enumerable)
            {
                strValues.Add(ConvertArg(o).ToString());
            }
            if (!strValues.Any()) return "[]";
            object newArg = string.Format("[{0}]", string.Join(",", strValues));
            return newArg;
        }
    }

}
