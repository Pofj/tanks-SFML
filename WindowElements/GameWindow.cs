using game.GameElements;
using game.Global;
using game.MenuElements;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.WindowElements
{
    class GameWindow
    {
        private RenderWindow window;
        private RenderWindow window2;
        private Game game;
       // private WindowHandlerMethods Handler;
        
        private Menu menu;
        public GameWindow(uint width, uint height)
        {
            
            // Handler = new WindowHandlerMethods();
            InfoAboutResolution.OldWidth = width;
            InfoAboutResolution.OldHeight = height;
            InfoAboutResolution.CurrentHeight = InfoAboutResolution.OldHeight;
            InfoAboutResolution.CurrentWidth = InfoAboutResolution.OldWidth;
            InfoAboutResolution.CurrentRatio = (float)InfoAboutResolution.CurrentWidth / InfoAboutResolution.CurrentHeight;
            //InfoAboutResolution.RatioWidthResolution = (float)InfoAboutResolution.CurrentWidth / (float)InfoAboutResolution.OriginalWidth / (InfoAboutResolution.CurrentRatio > InfoAboutResolution.OriginalRatio ? (InfoAboutResolution.CurrentRatio / InfoAboutResolution.OriginalRatio) : 1);
            //InfoAboutResolution.RatioHeightResolution = (float)InfoAboutResolution.CurrentHeight / (float)InfoAboutResolution.OriginalHeight / (InfoAboutResolution.CurrentRatio < InfoAboutResolution.OriginalRatio ? (InfoAboutResolution.CurrentRatio / InfoAboutResolution.OriginalRatio) : 1);

            InfoAboutResolution.RatioWidthResolution = (float)InfoAboutResolution.CurrentWidth / (float)InfoAboutResolution.OriginalWidth/* / (InfoAboutResolution.CurrentRatio > InfoAboutResolution.OriginalRatio ? (InfoAboutResolution.CurrentRatio / InfoAboutResolution.OriginalRatio) : 1)*/;
            //InfoAboutResolution.RatioHeightResolution = (float)InfoAboutResolution.CurrentHeight / (float)InfoAboutResolution.OriginalHeight / (InfoAboutResolution.CurrentRatio < InfoAboutResolution.OriginalRatio ? (InfoAboutResolution.CurrentRatio / InfoAboutResolution.OriginalRatio) : 1);
            InfoAboutResolution.PreviousWidth = InfoAboutResolution.OldWidth;
            InfoAboutResolution.PreviousHeight = InfoAboutResolution.OldHeight;

            game = new Game();
            menu = new game.MenuElements.Menu();
            //window = new RenderWindow(new VideoMode(1024, 780), "Tank");
            //window.Size = new Vector2u(width, height);
            window = new RenderWindow(new VideoMode(width, height), "Tank");
            window2 = new RenderWindow(new VideoMode(1920, 1080), "TEMP");
            KeyIsPress.temp2 = window2;

            window.Closed += this.OnClose;
            window.KeyPressed += game.KeyPressed;
            window.KeyReleased += game.KeyReleased;
            window.MouseButtonPressed += game.LeftClick;
            window.MouseMoved += this.MouseMoved;
            window.Resized += this.OnResize;
            window.Resized += game.ChangeSize;

            GlobalTimer.ClockGL.Restart();
            KeyIsPress.temp = window;
        }
        public void MouseMoved(object sender, SFML.Window.MouseMoveEventArgs e)
        {
            float k = InfoAboutResolution.OldWidth / InfoAboutResolution.CurrentWidth;
            k = InfoAboutResolution.OldHeight / InfoAboutResolution.CurrentHeight;
            InfoAboutMouse.CursorPositionX = e.X /** ((float)InfoAboutResolution.OldWidth / InfoAboutResolution.CurrentWidth)*/;
            InfoAboutMouse.CursorPositionY = e.Y /** ((float)InfoAboutResolution.OldHeight / InfoAboutResolution.CurrentHeight)*/;


        }
        private void OnResize(object sender, EventArgs e)
        {
            if ((window.Size.X != InfoAboutResolution.CurrentWidth && window.Size.Y != InfoAboutResolution.CurrentHeight))
            {
                 window.Size = new Vector2u(InfoAboutResolution.CurrentWidth, InfoAboutResolution.CurrentHeight);
                //goto Point22;
            }
            if (window.Size.X != InfoAboutResolution.CurrentWidth)
            {
                float k = (float)window.Size.X / (float)InfoAboutResolution.CurrentWidth;
                window.Size = new Vector2u(window.Size.X, (uint)(window.Size.Y * k)); ;
            }
            else if (window.Size.Y != InfoAboutResolution.CurrentHeight)
            {
                float k = (float)window.Size.Y / (float)InfoAboutResolution.CurrentHeight;
                window.Size = new Vector2u((uint)(window.Size.X * k), window.Size.Y);
            }
            //if (window.Size.Y > SFML.Window.VideoMode.DesktopMode.Height - 30 || window.Size.X > SFML.Window.VideoMode.DesktopMode.Width)
            //{
            //    window.Size = new Vector2u(SFML.Window.VideoMode.DesktopMode.Width, SFML.Window.VideoMode.DesktopMode.Height);
            //}
            if (window.Size.Y == 0 || window.Size.X == 0)
            {
                window.Size = new Vector2u((uint)(InfoAboutResolution.OriginalWidth / 8),(uint)(InfoAboutResolution.OriginalHeight / 8));
            }
            // Point22:
            InfoAboutResolution.PreviousWidth = InfoAboutResolution.CurrentWidth;
            InfoAboutResolution.PreviousHeight = InfoAboutResolution.CurrentHeight;
            InfoAboutResolution.CurrentWidth = window.Size.X;
            InfoAboutResolution.CurrentHeight = window.Size.Y;
            InfoAboutResolution.RatioWidthResolution =  (float)InfoAboutResolution.CurrentWidth / (float)InfoAboutResolution.OriginalWidth /*/ (InfoAboutResolution.CurrentRatio > InfoAboutResolution.OriginalRatio ? (InfoAboutResolution.CurrentRatio / InfoAboutResolution.OriginalRatio) : 1)*/;
            InfoAboutResolution.RatioHeightResolution = (float)InfoAboutResolution.CurrentHeight / (float)InfoAboutResolution.OriginalHeight /** (InfoAboutResolution.CurrentRatio < InfoAboutResolution.OriginalRatio ? (InfoAboutResolution.CurrentRatio / InfoAboutResolution.OriginalRatio) : 1)*/;
        }
        private void OnClose(object sender, EventArgs e)
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
                    window.Clear(SFML.Graphics.Color.Black);
                    window.Clear(SFML.Graphics.Color.Black);
                    if (game.GetGameStatus()) game.Show(window);
                    if (menu.GetMenuStatus()) menu.show(window);
                    deltaTime -= 0.016f;
                    window.Display();
                    window2.Display();
                }
            }
        }
    }
}
