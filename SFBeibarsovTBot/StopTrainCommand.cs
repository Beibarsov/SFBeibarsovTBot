using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class StopTrainCommand : AbstractCommand, IActionCommand
{
    public StopTrainCommand()
    {
        CommandText = "/stoptrain";
    }

    public bool Action(Conversation chat)
    {
        if (chat.isTraningProcess == true)
        {
            chat.isTraningProcess = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public string ReturnText(Conversation chat)
    {
        if (Action(chat)) return "Тренировка завершена";
        else return "Что-то пошло не так";
    }
}
