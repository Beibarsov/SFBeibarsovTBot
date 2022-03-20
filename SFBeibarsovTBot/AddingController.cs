public class AddingController
{

    private Dictionary<long, AddingState> ChatAdding;

    public AddingController()
    {
        ChatAdding = new Dictionary<long, AddingState>();
    }

    public void AddFirsState(Conversation chat)
    {
        ChatAdding.Add(chat.GetId(), AddingState.Russian);
    }

    public void NextStage(string message, Conversation chat){
        var currentstate = ChatAdding[chat.GetId()];
        ChatAdding[chat.GetId()] = currentstate + 1;

        if (ChatAdding[chat.GetId()] == AddingState.Finish){
            chat.isAddingWordProcess = false;
            ChatAdding.Remove(chat.GetId());
        }
    }

    public AddingState GetStage(Conversation chat){
        return ChatAdding[chat.GetId()];
    }



}