using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBot.Command
{
   public abstract class Command
    {
        public abstract string[] Names { get; set; }
        public abstract TelegramBotClient client { get; set; }
        public abstract void Execute(Message message, TelegramBotClient Bot);
        public  bool Contains(string message)
        {
            foreach (var mess in Names)
            {
                if (message.Contains(mess))
                {
                    return true;
                }
            }
                            return false;

        }

    }
}
