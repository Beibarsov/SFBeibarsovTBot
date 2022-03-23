using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

class StartTrainCommand : AbstractCommand, IActionCommand
{
    ITelegramBotClient botClient;
    Dictionary<long, Word> bufferSelectWord;
    public StartTrainCommand(ITelegramBotClient telegramBot)
    {

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

        botClient.SendTextMessageAsync(chatId: chat.GetId(), text: "Бла-бла-бла", replyMarkup: CreateKeyboard());
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
        var text = "Неудачно";
        if (chat.TrainingType == TrainingType.EngToRus)
        {
            text = ($"Выбранное слово - {word.English}");
        }
        if (chat.TrainingType == TrainingType.RusToEng)
        {
            text = ($"Выбранное слово - {word.Russian}");
        }

        await botClient.SendTextMessageAsync(chatId: chat.GetId(), text: text);
    }
    public async void DoForStageAsync(Conversation chat, string message)
    {

        string text = "Не назначено";
        var selectWord = bufferSelectWord[chat.GetId()];
        bool check = false;
        if (chat.TrainingType == TrainingType.EngToRus)
             check = chat.CheckWord(selectWord.Russian, message);
        if (chat.TrainingType == TrainingType.RusToEng)
            check = chat.CheckWord(selectWord.English, message);
        if (check) text = "Верно!";
        else text = "Не верно!";

        AddTrainWord(chat);
        SendCommandText(text, chat.GetId());

    }

    public async Task SendCommandText(string message, long id)
    {

        await botClient.SendTextMessageAsync(chatId: id, text: message);

    }

    public InlineKeyboardMarkup CreateKeyboard()
    {
        var buttonList = new List<InlineKeyboardButton>();
        buttonList.Add(new InlineKeyboardButton("С Русского на Английский") { CallbackData = "RusToEng" });
        buttonList.Add(new InlineKeyboardButton("С Английского на Русский") { CallbackData = "EngToRus" });
        return new InlineKeyboardMarkup(buttonList);
    }
}