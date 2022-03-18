using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class CommandParser
{
    private List<IChatCommand> Commands;

    public CommandParser()
    {
        Commands = new List<IChatCommand>();
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
    public bool isTextWithAction(string message)
    {
        var command = Commands.Find(x => x.CheckMessage(message));

        return command is IActionCommand;
    }


    public bool isAddingCommand(string message)
    {
        var command = Commands.Find(x => x.CheckMessage(message));

        return command is IAddingCommand;
    }
    public string GetMessageText(string message, Conversation chat)
    {


        var command = Commands.Find(x => x.CheckMessage(message)) as IChatTextCommand;
        return command.ReturnText();

    }
    public string GetMessageTextandAction(string message, Conversation chat)
    {


        var command = Commands.Find(x => x.CheckMessage(message)) as IActionCommand;
        return command.Action(chat);

    }

    public void AddWord(Conversation chat, string message)
    {
        var command = Commands.Find(x => x.CheckMessage(message)) as IAddingCommand;
        command.Action( chat);

    }
}

