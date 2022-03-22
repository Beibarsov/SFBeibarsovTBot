using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

class Messenger
{
    private ITelegramBotClient botClient;

    private CommandParser cmdParser;

    public Messenger(ITelegramBotClient botClient)
    {
        this.botClient = botClient;
        cmdParser = new CommandParser();
        cmdParser.AddComand(new SayHiCommand());
        cmdParser.AddComand(new AddWordCommand(botClient));
        cmdParser.AddComand(new ShowAllDictionary());
        cmdParser.AddComand(new DeleteWordCommand());
        cmdParser.AddComand(new StartTrainCommand(botClient));
        cmdParser.AddComand(new GetRandomWordCommand());
        cmdParser.AddComand(new StopTrainCommand());


    }

    public string CreateTextMessage()
    {
        var text = "Ожидаем команды";
        return text;
    }

    public InlineKeyboardMarkup CreateKeyboard()
    {
        var buttonList = new List<InlineKeyboardButton>();
        buttonList.Add(new InlineKeyboardButton("Perviy") { CallbackData = "perviy" });
        return new InlineKeyboardMarkup(buttonList);
    }

    public async Task MakeAnswer(Conversation chat)
    {
        var lastmessage = chat.GetLastMessage();
        if (chat.isAddingWordProcess)
        {

            Console.WriteLine("Продолжается заполнение");
            cmdParser.NextStage(lastmessage, chat);
        }
        if (!chat.isAddingWordProcess)
        {
            if (cmdParser.isCommand(lastmessage))
            {
                Console.WriteLine("ЭТО КОМАНДА!!!!");
                await ExecComand(chat, lastmessage);
                return;
            }
            else if (!chat.isAddingWordProcess && !chat.isTraningProcess)
            {
                await SendText(chat, CreateTextMessage());
            }
        }
        if (chat.isTraningProcess){
            Console.WriteLine("Пошел процесс тренировки");
            cmdParser.ContunueTraining(chat, lastmessage);
        }
        //await SendKeyboard(chat, "Выберите вариант", CreateKeyboard());
    }

    private async Task ExecComand(Conversation chat, string message)
    {
        if (cmdParser.isTextWithAction(message))
        {
            Console.WriteLine("Правильно определено");
            string text = cmdParser.GetMessageTextandAction(message, chat);
            await SendText(chat, text);
        }
        if (cmdParser.isInfoCommand(message))
        {
            Console.WriteLine("Правильно определено");
            string text = cmdParser.GetInfoMessageText(message, chat);
            await SendText(chat, text);
        }

        if (cmdParser.isTextCommand(message))
        {
            string text = cmdParser.GetSimpleMessageText(message, chat);
            await SendText(chat, text);
        }
        if (cmdParser.isAddingCommand(message))
        {
            chat.isAddingWordProcess = true;
            cmdParser.StartAddingWord(message, chat);
        }
    }

    private async Task SendText(Conversation chat, string text)
    {
        await botClient.SendTextMessageAsync(
              chatId: chat.GetId(),
              text: text
            );
    }
    public async Task SendKeyboard(Conversation chat, string text, InlineKeyboardMarkup keyboard)
    {
        await botClient.SendTextMessageAsync(
        chatId: chat.GetId(), text: text, replyMarkup: keyboard);
        Console.WriteLine("Гльлвл!");
    }
}