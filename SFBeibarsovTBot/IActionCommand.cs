﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

internal interface IActionCommand
{
    string Action(Conversation chat);

}
