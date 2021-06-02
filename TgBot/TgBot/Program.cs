using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
//using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.Command.Commands;

namespace TgBot
{
    class Program
    {
        static TelegramBotClient Bot;
        private static List<Command.Command> commands;
        static void Main(string[] args)
        {
            Bot = new TelegramBotClient(Config.Token);


            commands = new List<Command.Command>();
            commands.Add(new GetMyIdCommand());
            commands.Add(new GetChatIdCommand());
            commands.Add(new GetBeerByName());
            commands.Add(new GetBeerByAbv());
            commands.Add(new GetBeerByIbu());
            commands.Add(new GetBeerByEbc());
            commands.Add(new GetBeerByRandom());
            commands.Add(new Start());




            Bot.StartReceiving();
            Bot.OnMessage += OnMessageHandler;
            Bot.OnCallbackQuery += Bot_OnCallbackQueryReceived;
            var me = Bot.GetMeAsync().Result;
            Console.WriteLine(me.FirstName);
            Bot.StartReceiving();
            Console.ReadLine();
            Bot.StopReceiving();
        }
        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if(message.Text != null)
            {
                Console.WriteLine($"{message.From.FirstName} {message.From.LastName} sent: {message.Text}");
                foreach(var comm in commands)
                {
                    if (comm.Contains(message.Text))
                    {
                        comm.Execute(message, Bot);
                    }
                }
            }
        }
        private static async void Bot_OnCallbackQueryReceived(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            string buttonText = e.CallbackQuery.Data;
            string name = $"{e.CallbackQuery.From.FirstName} {e.CallbackQuery.From.LastName}";
            Console.WriteLine($"{name} pressed button {buttonText}");
            await Bot.AnswerCallbackQueryAsync(e.CallbackQuery.Id, $"Pressed {buttonText}");
        }

        private static async void Bot_OnMessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            
            var message = e.Message;

            if(message == null||message.Type != MessageType.Text)
            {
                return;
            }

            string name = $"{message.From.FirstName} {message.From.LastName}";
            Console.WriteLine($"{name} sent: {message.Text}");
            await Bot.SendTextMessageAsync(message.Chat.Id, message.Text , replyToMessageId: message.MessageId);
            switch (message.Text)
            {
                case "/start" :
                    string text =
  @"Teams list:
/start = bot launching
/inline = menu
/keyboard = keyboard";
                   await Bot.SendTextMessageAsync(message.From.Id, text);
                    break;
                case "/inline":
                    var inlineKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("YT", "https://www.youtube.com/watch?v=vG84k5WOiOs&t=2593s"),
                            InlineKeyboardButton.WithUrl("CH", "https://www.youtube.com/watch?v=vG84k5WOiOs&t=2593s")

                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Punkt1"),
                                                        InlineKeyboardButton.WithCallbackData("Punkt2")

                        }
                    });
                    await Bot.SendTextMessageAsync(message.From.Id, "Choose menu", replyMarkup: inlineKeyboard);
                    break;
                case "/keyboard":
                    var replyKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                        new[]
                        {
                            new KeyboardButton("Hi"),
                            new KeyboardButton("Wassup"),
                        },
                        new[]
                        {
                            new KeyboardButton("Contact"){RequestContact=true },
                            new KeyboardButton("Geo"){RequestLocation=true}
                        }
                    });
                    await Bot.SendTextMessageAsync(message.Chat.Id,"Message",replyMarkup: replyKeyboard );
                    break;
                default: break;
            }
        }
    }
}
