using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TgBot.Client;
using TgBot.Models;

namespace TgBot.Command.Commands
{
    class GetBeerByRandom : Command
    {

        public override string[] Names { get; set; } = new string[] { "/beerbyrnd", "beer_by_rnd" };

        private readonly BeerApiClient _beerApiClient = new BeerApiClient();
        public override TelegramBotClient client { get; set; }
        



        public override async void Execute(Message message, TelegramBotClient Bot)
        {
            this.client = Bot;
            await Bot.SendTextMessageAsync(message.Chat.Id, $"Your random beer: ");
            BeerApiClient bc = new BeerApiClient();
            var result = await bc.GetBeerByRandom();
            if (result == null)
            {
                await client.SendTextMessageAsync(message.From.Id, $"nema ");
            }
            string output = string.Empty;

            foreach (BeerInfo beer in result)
            {
                output += $"Name: {beer.Name}\n\n" +
                $"Tagline: {beer.TagLine}\n\n" +
                $"Description: {beer.Description}\n\n" +
                $"Abv: {beer.Abv}\n\n" +
                $"Srm: {beer.Srm}\n\n" +
                $"Ebc: {beer.Ebc}\n\n" +
                $"Ibu: {beer.Ibu}\n\n";

                if (beer.FoodPairing != null)
                {
                    foreach (var food in beer.FoodPairing)
                    {
                        output += $"{food}, ";
                    }
                }


                await client.SendTextMessageAsync(message.From.Id, output);
                output = string.Empty;
            }

        }





        
    }
}
    

//namespace TgBot.Command.Commands
//{
//    class JpgToPdf : Command
//    {
//        public override string Name { get; set; } = "/convertToPdf";
//        private readonly ImageClient _imageClient = new ImageClient();
//        public override TelegramBotClient client { get; set; }
//        private string Url { get; set; }
//        public override async void Execute(Message message, TelegramBotClient _client)
//        {
//            this.client = _client;
//            await _client.SendTextMessageAsync(message.From.Id, "Send link for image");
//            this.client.OnMessage += GetString;
//        }
//        private async void GetString(object sender, MessageEventArgs e)
//        {
//            Url = e.Message.Text;
//            ImageClient im = new ImageClient();
//            var result = await im.ConvertToPdf(Url);
//            await client.SendDocumentAsync(e.Message.From.Id, new InputOnlineFile(result, "image.pdf"));
//        }
//    }
//}