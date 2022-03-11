using Telegram.Bot.Types;
using Telegram.Bot;

class BotMessageLogic
{

    private Messenger messenger;

    public Dictionary<long, Conversation> chatList;

    private TelegramBotClient botClient;

    public BotMessageLogic(TelegramBotClient botClient)
    {
        this.botClient = botClient;
        messenger = new Messenger(botClient);
        chatList = new Dictionary<long, Conversation>();
    }
    public async Task Response(Message message)
    {
        var id = message.Chat.Id;

        if (!chatList.ContainsKey(id))
        {
            var newchat = new Conversation(message.Chat);

            chatList.Add(id, newchat);
        }

        var chat = chatList[id];
        chat.AddMessage(message);

        await SendTextMessage(chat);

    }

    private async Task SendTextMessage(Conversation chat)
    {
        var text = messenger.CreateTextMessage(chat);

        await botClient.SendTextMessageAsync(chatId: chat.GetId(), text: text);
    }
}