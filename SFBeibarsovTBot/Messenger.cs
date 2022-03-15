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
        

    }

    public string CreateTextMessage(Conversation chat)
    {
        var text = "default";
       /* switch (chat.GetLastMessage())
        {

            case "/sayhi":
                {
                    text = "Hello!";
                    break;
                }
            case "/askme":
                {
                    text = "How a you?";
                    break;
                }
            default:
                {
                    var delimiter = ", ";
                    text = $"Вот что вы писали нам ранее: {string.Join(delimiter, chat.GetTextMessages().ToArray())}";
                    break;
                }
        }*/
        return text;
    }

    public InlineKeyboardMarkup CreateKeyboard()
    {
        var buttonList = new List<InlineKeyboardButton>();
        buttonList.Add(new InlineKeyboardButton("Perviy"){ CallbackData = "perviy" });
        return new InlineKeyboardMarkup(buttonList);
    }

    public async Task MakeAnswer(Conversation chat)
    {

        var lastmessage = chat.GetLastMessage();
        if (cmdParser.isCommand(lastmessage))
        {
            Console.WriteLine("ЭТО КОМАНДА!!!!");
        }
        var text = CreateTextMessage(chat);
        await SendText(chat, text);
        await SendKeyboard(chat, "Выберите вариант", CreateKeyboard());
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