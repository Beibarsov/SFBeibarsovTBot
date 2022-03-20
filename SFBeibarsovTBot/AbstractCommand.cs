using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class AbstractCommand : IChatCommand
{
    public string CommandText;

    public bool CheckMessage(string message)
    {
        return message.StartsWith(CommandText);
    }


}
