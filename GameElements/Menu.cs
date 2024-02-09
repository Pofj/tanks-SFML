using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.GameElements
{
    class Menu
    {
        private bool MenuIsOn;
        public Menu()
        {
            MenuIsOn = false;
        }
        public bool GetMenuStatus()
        {
            return MenuIsOn;
        }
        public void show(in RenderWindow window)
        {
            //update
            //show

        }
    }
}
