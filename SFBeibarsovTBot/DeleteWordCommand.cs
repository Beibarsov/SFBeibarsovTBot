internal class DeleteWordCommand : AbstractCommand, IActionCommand
{
    public DeleteWordCommand(){
        CommandText = "/deleteword";
    }


    public string ReturnText(Conversation chat)
    {
        if (Action(chat)) return "Слово удалено!";
        else return "Не удалено";

    }

    public bool Action(Conversation chat)
    {
        Console.WriteLine("Попытка удаления");
        var messgage = chat.GetLastMessage();
        messgage = messgage.Substring(CommandText.Length + 1);

        if (chat.dictionary.ContainsKey(messgage)){
            chat.dictionary.Remove(messgage);
            return true;
        }
        return false;
    }


}