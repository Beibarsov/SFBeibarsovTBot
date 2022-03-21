using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class GetRandomWordCommand : AbstractCommand, IShowInfoCommand
{
    public GetRandomWordCommand()
    {
        CommandText = "/getrandomword";
    }

    public string ReturnText(Conversation chat)
    {
        return chat.getRndWord().ToString();
    }
}

