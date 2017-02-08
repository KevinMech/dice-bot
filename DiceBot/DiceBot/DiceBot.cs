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
        /// Attempts to connect to Discords Server
        /// </summary>
        /// <param name="maxTries">The amount of tries the bot will attempt to connect to the server</param>
        /// <param name="timeout">The amount of time in milliseconds the bot will make in between each attempt</param>
        private void Connect(int maxTries, int timeout)
        {
            bool success = false;
            Console.WriteLine("Connecting to server...");
            for (int tries = 0; tries < maxTries; tries++)
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine("attempting connection...[" + (tries + 1) + "/3]");
                    Task task = Task.Run(async () => await Connect(Token.getToken, TokenType.Bot));
                    task.Wait();
                    success = true;
                    Console.WriteLine("Connected to server!");
                    break;
                }
                catch (AggregateException e)
                {
                    Console.WriteLine("Failed!");
                    foreach (Exception exception in e.InnerExceptions) Console.WriteLine(exception.Message);
                    if(tries < (maxTries - 1)) System.Threading.Thread.Sleep(timeout);
                }
            }
            if (!success)
            {
                Console.WriteLine();
                Console.WriteLine("Could not connect to server!");
                Console.ReadLine();
                Environment.Exit(0);
            }
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
