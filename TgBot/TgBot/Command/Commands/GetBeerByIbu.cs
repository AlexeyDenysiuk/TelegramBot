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
    class GetBeerByIbu : Command
    {

        public override string[] Names { get; set; } = new string[] { "/beerbyibu", "beer_by_ibu" };

        private readonly BeerApiClient _beerApiClient = new BeerApiClient();
        public override TelegramBotClient client { get; set; }
        private string ibu { get; set; }



        public override async void Execute(Message message, TelegramBotClient Bot)
        {
            this.client = Bot;
            await Bot.SendTextMessageAsync(message.Chat.Id, $"Send wanted IBU(bitterness)(will be shown results in (-5,5) range)");
            this.client.OnMessage += GetString;

        }
        private async void GetString(object sender, MessageEventArgs e)
        {
            ibu = e.Message.Text;
            
            BeerApiClient bc = new BeerApiClient();
            var result = await bc.GetBeerByIbu(ibu);


            SendInf(result, e.Message);
            this.client.OnMessage -= GetString;

        }
        protected async void SendInf(IEnumerable<BeerInfo> beerInfo, Message message)
        {
            if (beerInfo == null)
            {
                await client.SendTextMessageAsync(message.From.Id, $"Nothing ");
            }
            string output=string.Empty;

            foreach (BeerInfo beer in beerInfo)
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
    

