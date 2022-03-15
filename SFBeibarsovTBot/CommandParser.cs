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
}

