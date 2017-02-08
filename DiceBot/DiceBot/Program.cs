using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace DiceBot
{
    class Program
    {
        static void Main(string[] args)
        {
            DiscordClient dicebot = new DiscordClient(metadata =>
            {
                metadata.AppName = "DiscordDiceBot";
                metadata.AppVersion = "0.1.0";
                metadata.AppUrl = "https://github.com/KevinMech/dice-bot";
                metadata.LogLevel = LogSeverity.Error;
                metadata.LogHandler += ErrorLogHandling;
            });
            dicebot.ExecuteAndWait(async () =>
            {
                await dicebot.Connect(Token.getToken, TokenType.Bot);
            });
            while (true)
            {
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Logs any errors encountered by the discord API and prints to console screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void ErrorLogHandling(object sender, LogMessageEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.Message);
        }

        //async static void DebugEchoTest(object sender, MessageEventArgs e)
        //{
        //    Console.WriteLine("message sent");
        //    await e.Channel.SendMessage("Message Received!");
        //}
    }
}
