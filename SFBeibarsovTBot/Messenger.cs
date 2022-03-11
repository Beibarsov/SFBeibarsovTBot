using Telegram.Bot;

class Messenger
{
    private ITelegramBotClient botClient;

    public Messenger(ITelegramBotClient botClient)
    {
        this.botClient = botClient;

    }

    public string CreateTextMessage(Conversation chat)
    {
        var text = "default";
        switch (chat.GetLastMessage())
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
        }
        return text;
    }


    public async Task MakeAnswer(Conversation chat)
    {
        var text = CreateTextMessage(chat);
        await SendText(chat, text);
    }

    private async Task SendText(Conversation chat, string text)
    {
        await botClient.SendTextMessageAsync(
              chatId: chat.GetId(),
              text: text
            );
    }
}