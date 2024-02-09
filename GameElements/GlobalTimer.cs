using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.GameElements
{
    static class GlobalTimer
    {
        static public SFML.System.Clock ClockGL;
        static GlobalTimer()
        {
            ClockGL = new Clock();
        }
    }
}
