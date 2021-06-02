using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Client;

namespace TgBot.Command.Commands
{
    class Start : Command
    {
        public override string[] Names { get; set; } = { "/start" };
        private readonly BeerApiClient _beerApiClient = new BeerApiClient();
        public override TelegramBotClient client { get; set; }
        public override async void Execute(Message message, TelegramBotClient Bot)
        {
            string startText =
                                $"Hi {message.From.FirstName} {message.From.LastName}! I can help you to find a beer." +
                                $"\nHere is what I can do:" +
                                $"\n/beerbyrnd - get random beer" +
                                $"\n/beerbyname - find beer by it’s name"+
                                $"\n/beerbyabv - find beer by it’s ABV(alc) level " +
                                $"\n/beerbyebc - find beer by it’s EBC(colour) level  " +
                                $"\n/beerbyibu - find beer by it’s IBU(bitterness) level  "  ;

            await Bot.SendTextMessageAsync(message.From.Id, startText);
        }
    }
}
