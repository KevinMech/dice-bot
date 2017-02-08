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
            Console.WriteLine("Connecting...");
            ExecuteAndWait(async () => await Connect(Token.getToken, TokenType.Bot));
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
