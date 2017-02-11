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
            Logging.consoleLog("Connecting to server...", Logging.logType.System);
            for (int tries = 0; tries < maxTries; tries++)
            {
                try
                {
                    Console.WriteLine();
                    Logging.consoleLog("attempting connection...[" + (tries + 1) + "/3]", Logging.logType.System);
                    Task task = Task.Run(async () => await Connect(Token.getToken, TokenType.Bot));
                    task.Wait();
                    Logging.consoleLog("Connected to server!", Logging.logType.System);
                    break; 
                }
                catch (AggregateException e)
                {
                    Logging.consoleLog("Failed!", Logging.logType.Error);
                    foreach (Exception exception in e.InnerExceptions) Logging.consoleLog(exception.Message, Logging.logType.Error);
                    //If bot fails to connect to server, print error to screen and exit program
                    if(tries < (maxTries - 1)) System.Threading.Thread.Sleep(timeout);
                    else
                    {
                        Console.WriteLine();
                        Logging.consoleLog("Could not connect to server!", Logging.logType.Error);
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
            }
        }
    }
}
