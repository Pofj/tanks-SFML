using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.GameElements
{
    static class KeyIsPress
    {
        static public bool isWPressed = false;
        static public bool isSPressed = false;
        static public bool isAPressed = false;
        static public bool isDPressed = false;
        static public bool leftClick = false;
        static public double CursorPositionX, CursorPositionY;
        static public uint OldHeight, OldWidth;
        static public uint CurrentHeigh, CurrentWidth;
    }
}
