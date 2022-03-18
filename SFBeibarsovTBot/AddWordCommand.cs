using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;


internal class AddWordCommand : AbstractCommand, IChatTextCommand, IAddingCommand
{
    ITelegramBotClient botClient;

    public AddWordCommand(ITelegramBotClient botClient)
    {
        CommandText = "/addword";
        this.botClient = botClient;
    }
    public string ReturnText()
    {
        return "Будет добавлено слово";
    }

    public async void Action(Conversation chat)
    {
        chat.AddWordDictionary("text", new Word("eng", "rus", "testtheme"));
    }


}

