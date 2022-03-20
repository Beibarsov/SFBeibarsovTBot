using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class CommandParser
{
    private List<IChatCommand> Commands;

    private AddingController addingController;

    public CommandParser()
    {
        Commands = new List<IChatCommand>();
        addingController = new AddingController();
    }

    public void AddComand(IChatCommand cmd)
    {
        Commands.Add(cmd);
    }

    public bool isCommand(string message)
    {
        if (Commands.Exists(x => x.CheckMessage(message)))
        {
            return true;
        }
        return false;
    }

    public bool isTextCommand(string message)
    {
        var command = Commands.Find(x => x.CheckMessage(message));

        return command is IChatTextCommand;
    }
    public bool isInfoCommand(string message)
    {
        var command = Commands.Find(x => x.CheckMessage(message));

        return command is IShowInfoCommand;
    }
    public bool isTextWithAction(string message)
    {
        var command = Commands.Find(x => x.CheckMessage(message));

        return command is IActionCommand;
    }


    public bool isAddingCommand(string message)
    {
        var command = Commands.Find(x => x.CheckMessage(message));

        return command is AddWordCommand;
    }

    public void StartAddingWord(string message, Conversation chat)
    {
        var command = Commands.Find(x => x.CheckMessage(message)) as AddWordCommand;

        addingController.AddFirsState(chat);
        command.StartProcessAsync(chat);
    }

    public void NextStage(string message, Conversation chat)
    {
        var command = Commands.Find(x => x is AddWordCommand) as AddWordCommand;
        command.DoForStageAsync(addingController.GetStage(chat), chat, message);
        addingController.NextStage(message, chat);
    }
    public string GetSimpleMessageText(string message, Conversation chat)
    {


        var command = Commands.Find(x => x.CheckMessage(message)) as IChatTextCommand;
        return command.ReturnText();

    }
    public string GetInfoMessageText(string message, Conversation chat)
    {


        var command = Commands.Find(x => x.CheckMessage(message)) as IShowInfoCommand;
        return command.ReturnText(chat);

    }
    public string GetMessageTextandAction(string message, Conversation chat)
    {


        var command = Commands.Find(x => x.CheckMessage(message)) as IActionCommand;
        return command.ReturnText(chat);


    }

    public void AddWord(Conversation chat, string message)
    {
        var command = Commands.Find(x => x.CheckMessage(message)) as IAddingCommand;
        command.Action(chat);

    }
}

