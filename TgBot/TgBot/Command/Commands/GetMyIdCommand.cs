using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Client;

namespace TgBot.Command.Commands
{
    public class GetMyIdCommand : Command
    {
        private readonly BeerApiClient _beerApiClient = new BeerApiClient();
        public override TelegramBotClient client { get; set; }
        public override string[] Names { get; set; } = new string[] { "getmyid", "get_my_id" };

        public override async void Execute(Message message, TelegramBotClient Bot)
        {
            await Bot.SendTextMessageAsync(message.Chat.Id, $"Your Telegram ID: {message.From.Id}");
        }
    }
}
