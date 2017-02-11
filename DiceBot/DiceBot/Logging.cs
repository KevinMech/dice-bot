using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace DiceBot
{
    class Logging
    {
        public enum logType
        {
            System,
            Info,
            Warning,
            Error
        }

        /// <summary>
        /// Logs any system messages to console depending on severity
        /// </summary>
        public static void consoleLog(string message, logType logtype)
        {
            string TimeStamp = "[" + DateTime.Now.ToLongTimeString() + "]";
            switch (logtype)
            {
                case logType.System:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case logType.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case logType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case logType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
            Console.WriteLine(TimeStamp + " " + message);
        }

        /// <summary>
        /// Logs any errors encountered by the discord API and prints to console screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ErrorLogHandling(object sender, LogMessageEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.Message);
        }
    }
}
