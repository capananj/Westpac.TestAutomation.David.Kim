using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomation.Framework.Common
{
    public static class Logger
    {
        public static void Log(LogType logType, string message)
        {
            var method = new StackTrace().GetFrame(1).GetMethod();
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd hh:mm:ss:ff}][{logType}][{method.DeclaringType.Name}][{method.Name}] {message}");
        }
    }


}
