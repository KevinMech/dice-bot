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
            DiceBot dicebot = new DiceBot(metadata =>
            {
                metadata.AppName = "DiscordDiceBot";
                metadata.AppVersion = "0.1.0";
                metadata.AppUrl = "https://github.com/KevinMech/dice-bot";
                metadata.LogLevel = LogSeverity.Error;
                metadata.LogHandler += DiceBot.ErrorLogHandling;
            });
        }
    }
}
