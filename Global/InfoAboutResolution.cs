using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.Global
{
    static class InfoAboutResolution
    {
        static public int OriginalWidth = 1024, OriginalHeight = 768;
        static public float OriginalRatio = (float)1024 / 768;
        static public float CurrentRatio;
        static public uint OldHeight, OldWidth; //start params
        static public uint PreviousHeight, PreviousWidth;
        static public uint CurrentHeight, CurrentWidth;
        static public float RatioWidthResolution;
        static public float RatioHeightResolution;
    }
}
