using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;


internal class AddWordCommand : AbstractCommand
{
    ITelegramBotClient botClient;

    private Dictionary<long, Word> buffer;

    public AddWordCommand(ITelegramBotClient botClient)
    {
        CommandText = "/addword";
        this.botClient = botClient;
        buffer = new Dictionary<long, Word>();
    }
    public async void StartProcessAsync(Conversation chat)
    {
        buffer.Add(chat.GetId(), new Word());
        var text = "Введите русский вариант:";
        await SendCommandText(text, chat.GetId());

    }

    public async void DoForStageAsync(AddingState addingState, Conversation chat, string message){
        var word = buffer[chat.GetId()];
        var text = "";

        switch(addingState){
            case AddingState.Russian:
                word.Russian = message;

                text = "Введите английский перевод, пожалуйста";
                break;
            case AddingState.English:
                word.English = message;

                text = "Введите тему";
                break;
            case AddingState.Theme:
                word.Theme = message;
                text = "Слово успешно добавлено";

                chat.dictionary.Add(word.English, word);
                

                buffer.Remove(chat.GetId());
                chat.isAddingWordProcess = false;
                break;
        }

        await SendCommandText(text, chat.GetId());
    }


    public async Task SendCommandText(string message, long id)
    {

        await botClient.SendTextMessageAsync(chatId: id, text: message);

    }


}

