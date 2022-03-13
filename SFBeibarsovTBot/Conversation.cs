using Telegram.Bot.Types;

class Conversation
{
    private Chat telegramChat;
    private List<Message> telegramMessages;

    public Conversation(Chat chat)
    {
        telegramChat = chat;
        telegramMessages = new List<Message>();
    }

    public void AddMessage(Message message)
    {
        telegramMessages.Add(message);
    }

    public long GetId() => telegramChat.Id;

    public List<string> GetTextMessages()
    {
        var textMessages = new List<string>();
        foreach (var message in telegramMessages)
        {
            if (message.Text != null)
            {
                textMessages.Add(message.Text);
            }
        }
        return textMessages;

    }

    public string GetLastMessage() => telegramMessages[telegramMessages.Count -1].Text;

    

}