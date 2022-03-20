using Telegram.Bot;

class StartTrain : AbstractCommand, IActionCommand
{
    ITelegramBotClient botClient;
    Dictionary<long, Word> selectWord;
    public StartTrain(ITelegramBotClient telegramBot){

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
        return "Тренировка начата";
    }

    public async void DoForStageAsync(Conversation chat, string message){
        Random rnd = new Random();
        var dice = rnd.Next(0, chat.dictionary.Count);

        var selectWord = chat.getRndWord();
        
       // var check = chat.CheckWord(selectWord, message);
    }
}