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
        char SPECIALCHARACTER = '>';

        public DiceBot(Action<DiscordConfigBuilder> configFunc) : base(configFunc)
        {
            Connect(3, 4000);
            MessageReceived += DiceBot_MessageReceived;
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
        /// <summary>
        /// If bot recognizes a user in chat using roll command, will roll dice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DiceBot_MessageReceived(object sender, MessageEventArgs e)
        {
            string[] message = e.Message.Text.Split(' ');
            if(message[0] == SPECIALCHARACTER + "roll")
            {
                int diceSize = 0;
                bool isNum = Int32.TryParse(message[1], out diceSize);
                if (isNum)
                {
                    Random rand = new Random();
                    int roll = rand.Next(1,diceSize);
                    e.Channel.SendMessage("**" + e.User.Name + "** has rolled a **" + roll + "**");
                }
                else e.Channel.SendMessage(message[1] + " is not recognized! Please use a number and try again.");
            }


        }
    }
}
