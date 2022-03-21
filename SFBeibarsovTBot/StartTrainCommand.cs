using Telegram.Bot;

class StartTrainCommand : AbstractCommand, IActionCommand
{
    ITelegramBotClient botClient;
    Dictionary<long, Word> selectWord;
    public StartTrainCommand(ITelegramBotClient telegramBot){

        CommandText = "/starttrain";
        botClient = telegramBot;
        selectWord = new Dictionary<long, Word>();
        
    }

    public bool Action(Conversation chat)
    {
        if (chat.isAddingWordProcess) return false;
        if (chat.isTraningProcess) return false;
        chat.isTraningProcess = true;
        return true;
    }

    public string ReturnText(Conversation chat)
    {
        Action(chat);
        return "Тренировка начата";
    }

    public async void DoForStageAsync(Conversation chat, string message){

        string text = "Верно!";

        Console.WriteLine("Следующая стадия тренировок");
        Random rnd = new Random();
        var dice = rnd.Next(0, chat.dictionary.Count);

        var selectWord = chat.getRndWord();
        
        var check = chat.CheckWord(selectWord.Russian, message);
        if (check) text = "Верно!";
        else text = "Не верно!";
        SendCommandText(text, chat.GetId());

    }

    public async Task SendCommandText(string message, long id)
    {

        await botClient.SendTextMessageAsync(chatId: id, text: message);

    }
}