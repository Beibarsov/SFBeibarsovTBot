using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class ShowAllDictionary : AbstractCommand, IShowInfoCommand
{

    public string text;
    public ShowAllDictionary()
    {
        CommandText = "/dictionary";
        text = "";
    }

    public string ReturnText(Conversation chat)
    {
        Console.WriteLine("Пытается показать словарь");
        string text = "";
        foreach (var dict in chat.GetDictionary())
        {
            text += dict.Value.ToString() + " \n";
        }
        return text;
    }
}


