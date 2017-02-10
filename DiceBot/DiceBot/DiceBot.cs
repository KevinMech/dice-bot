using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace DiceBot
{
    class DiceBot : DiscordClient
    {
        //statistics recorded for the bot
        public int TimesRun { get; }
        public int DiceRolled { get; set; }
        public DiceBot(Action<DiscordConfigBuilder> configFunc) : base(configFunc)
        {
            Connect(3, 4000);
            while (true)
            {
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Attempts to connect the bot to Discords Server.
        /// </summary>
        /// <param name="maxTries">The amount of tries the bot will attempt to connect to the server</param>
        /// <param name="timeout">The amount of time in milliseconds the bot will make in between each attempt</param>
        private void Connect(int maxTries, int timeout)
        {
            consoleLog("Connecting to server...", logType.System);
            for (int tries = 0; tries < maxTries; tries++)
            {
                try
                {
                    Console.WriteLine();
                    consoleLog("attempting connection...[" + (tries + 1) + "/3]", logType.System);
                    Task task = Task.Run(async () => await Connect(Token.getToken, TokenType.Bot));
                    task.Wait();
                    consoleLog("Connected to server!", logType.System);
                    break;
                }
                catch (AggregateException e)
                {
                    consoleLog("Failed!", logType.Error);
                    foreach (Exception exception in e.InnerExceptions) consoleLog(exception.Message, logType.Error);
                    //If bot fails to connect to server, print error to screen and exit program
                    if(tries < (maxTries - 1)) System.Threading.Thread.Sleep(timeout);
                    else
                    {
                        Console.WriteLine();
                        consoleLog("Could not connect to server!", logType.Error);
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
            }
        }

        private enum logType
        {
            System,
            Info,
            Warning,
            Error
        }

        /// <summary>
        /// Logs any system messages to console depending on severity
        /// </summary>
        private void consoleLog(string message, logType logtype)
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
