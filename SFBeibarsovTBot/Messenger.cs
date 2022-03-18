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


    }

    public string CreateTextMessage()
    {
        var text = "default";
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
        if (cmdParser.isCommand(lastmessage))
        {
            //Console.WriteLine("ЭТО КОМАНДА!!!!");
            await ExecComand(chat, lastmessage);
        }
        else
        {
            await SendText(chat, CreateTextMessage());
        }

        //await SendKeyboard(chat, "Выберите вариант", CreateKeyboard());
    }

    private async Task ExecComand(Conversation chat, string message)
    {

        if (cmdParser.isTextCommand(message))
        {
            string text = cmdParser.GetMessageText(message, chat);
            await SendText(chat, text);
        }
        if (cmdParser.isAddingCommand(message))
        {
           cmdParser.AddWord(chat, message);
        }
        if (cmdParser.isTextWithAction(message))
        {
            string text = cmdParser.GetMessageText(message, chat);
            await SendText(chat, text);
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