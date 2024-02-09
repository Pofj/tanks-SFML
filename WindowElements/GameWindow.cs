using game.GameElements;
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
        private uint width; //ширина
        private uint height;
        private RenderWindow window;
        private Game game;
        private WindowHandlerMethods Handler;
        private game.GameElements.Menu menu;
        public GameWindow(uint width, uint height)
        {
            Handler = new WindowHandlerMethods();
            this.width = width;
            this.height = height;
            game = new Game();
            menu = new game.GameElements.Menu();
            window = new RenderWindow(new VideoMode(width, height), "Tank");
            window.Closed += Handler.OnClose;
            window.KeyPressed += game.KeyPressed;
            window.KeyReleased += game.KeyReleased;
            window.MouseButtonPressed += game.LeftClick;
            window.MouseMoved += game.MouseMoved;
            GlobalTimer.ClockGL.Restart();
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
