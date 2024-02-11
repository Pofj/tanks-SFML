using game.GameElements;
using game.MenuElements;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.WindowElements
{
    class GameWindow
    {
        private RenderWindow window;
        private Game game;
       // private WindowHandlerMethods Handler;
        private Menu menu;
        public GameWindow(uint width, uint height)
        {
           // Handler = new WindowHandlerMethods();
            KeyIsPress.OldWidth = width;
            KeyIsPress.OldHeight = height;
            KeyIsPress.CurrentHeigh = KeyIsPress.OldHeight;
            KeyIsPress.CurrentWidth = KeyIsPress.OldWidth;
            game = new Game();
            menu = new game.MenuElements.Menu();
            window = new RenderWindow(new VideoMode(width, height), "Tank");
            window.Closed += this.OnClose;
            window.KeyPressed += game.KeyPressed;
            window.KeyReleased += game.KeyReleased;
            window.MouseButtonPressed += game.LeftClick;
            window.MouseMoved += game.MouseMoved;
            window.Resized += this.OnResize;
            GlobalTimer.ClockGL.Restart();
        }
        public void OnResize(object sender, EventArgs e)
        {
            KeyIsPress.CurrentWidth = window.Size.X; // window.Size.X / (float)OldWidth;
            KeyIsPress.CurrentHeigh = window.Size.Y;
        }
        public void OnClose(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }
        public void OpenWindow()
        {
            SFML.System.Clock clock = new Clock();
            float deltaTime = 0.0f;
            float frameTime;
            game.LoadLevel();
            while (window.IsOpen)
            {
                window.DispatchEvents();

                frameTime = clock.Restart().AsSeconds();
                deltaTime += frameTime;
                while (deltaTime > 0.016f)
                {
                    window.DispatchEvents();
                    window.Clear(Color.Black);
                    if (game.GetGameStatus()) game.show(window);
                    if (menu.GetMenuStatus()) menu.show(window);
                    deltaTime -= 0.016f;
                    window.Display();
                }
            }
        }
    }
}
