﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinRun.UI.Hotkeys
{
    public interface IHotkeyHandler
    {
        Hotkey Hotkey { get; }

        void Handle(Hotkey hotkey);
    }
}
