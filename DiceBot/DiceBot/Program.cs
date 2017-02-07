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
            DiscordClient dicebot = new DiscordClient();
            dicebot.Connect(Token.getToken, TokenType.Bot);
            dicebot.MessageReceived += DebugEchoTest;
            while (true)
            {
                Console.ReadLine();
            }
        }

        private static void DebugEchoTest(object sender, MessageEventArgs e)
        {
            e.Channel.SendMessage("Message Received!");
        }
    }
}
