using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class ShowAllDictionary : AbstractCommand, IActionCommand
{
    public ShowAllDictionary()
    {
        CommandText = "/dictionary";
    }

    public string Action(Conversation chat)
    {
        return chat.GetDictionary().ToString();
    }

   
}


