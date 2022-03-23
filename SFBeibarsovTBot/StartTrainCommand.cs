using Telegram.Bot;

class StartTrainCommand : AbstractCommand, IActionCommand
{
    ITelegramBotClient botClient;
    Dictionary<long, Word> bufferSelectWord;
    public StartTrainCommand(ITelegramBotClient telegramBot){

        CommandText = "/starttrain";
        botClient = telegramBot;
        bufferSelectWord = new Dictionary<long, Word>();
        
    }

    public bool Action(Conversation chat)
    {

        if (chat.isAddingWordProcess) return false;
        if (chat.isTraningProcess) return false;
        chat.isTraningProcess = true;
        AddTrainWord(chat);
        return true;
    }

    public string ReturnText(Conversation chat)
    {
        Action(chat);
        return "Тренировка начата";
    }

    public async void AddTrainWord(Conversation chat)
    {
        bufferSelectWord.Remove(chat.GetId());
        bufferSelectWord.Add(chat.GetId(), chat.getRndWord());
        var word = bufferSelectWord[chat.GetId()];
        var text = ($"Выбранное слово - {word.English}");
        await botClient.SendTextMessageAsync(chatId: chat.GetId(), text: text );
    }
    public async void DoForStageAsync(Conversation chat, string message){

        string text = "Не назначено";
        var selectWord = bufferSelectWord[chat.GetId()];

        Console.WriteLine($"Выбрано слово {selectWord.Russian}") ;
        
        var check = chat.CheckWord(selectWord.Russian, message);
        if (check) text = "Верно!";
        else text = "Не верно!";
        
        AddTrainWord(chat);
        SendCommandText(text, chat.GetId());

    }

    public async Task SendCommandText(string message, long id)
    {

        await botClient.SendTextMessageAsync(chatId: id, text: message);

    }
}