using Telegram.Bot.Types;

public class Conversation
{
    private Chat telegramChat;
    private List<Message> telegramMessages;
    public Dictionary<string, Word> dictionary;
    public bool isAddingWordProcess = false;

    public bool isTraningProcess = false;

    public TrainingType TrainingType;

    public Conversation(Chat chat)
    {
        telegramChat = chat;
        telegramMessages = new List<Message>();
        dictionary = new Dictionary<string, Word>();
        dictionary.Add("1", new Word("123", "123", "123"));
    }

    public void AddMessage(Message message)
    {
        telegramMessages.Add(message);
    }

    public void AddWordDictionary(string key, Word word)
    {
        dictionary.Add(key, word);
    }

    public Dictionary<string, Word> GetDictionary()
    {
        return dictionary;
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

    public bool CheckWord(string word, string answer){
        Word control;
        var result = false;

        control = dictionary.Values.FirstOrDefault(x=>x.Russian == word);

        result = control?.Russian == answer;

        return result;
        
    }

    public Word getRndWord(){
        Random rnd = new Random();
        var dice = rnd.Next(0, dictionary.Count);
        var word = dictionary.Values.AsEnumerable().ElementAt(dice);
        return word; 
    }

    public string GetLastMessage() => telegramMessages[telegramMessages.Count -1].Text;

    

}