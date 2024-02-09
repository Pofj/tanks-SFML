using game.WindowElements;
using game.GameElements;
using game.SpaceConstant;
using game.GameObject;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.ComponentModel.Design;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;



namespace MainProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            GameWindow window = new GameWindow(1200,800);
            window.OpenWindow();





            //RenderWindow window = new RenderWindow(new VideoMode(1920, 1050), "Tank");
            //window.Closed += (sender, e) => { window.Close(); };
            //window.KeyPressed += KeyClick;
            //SFML.System.Clock clock = new Clock();
            //float deltaTime = 0.0f;
            //float frameTime;
            //Game game = new Game();
            //game.LoadLevel();
            //while(window.IsOpen)
            //{
            //    window.DispatchEvents();




            //    frameTime = clock.Restart().AsSeconds();
            //    deltaTime += frameTime;
            //    while (deltaTime > 0.016f)
            //    {
            //        window.Clear(Color.Black);
            //        if (game.GetGameStatus()) game.show(window);
            //        //if (menu.MenuActive()) menu.action_show(window);
            //        deltaTime -= 0.016f;
            //        window.Display();
            //    }
            //}
            
        }
        //void EventHandling(in RenderWindow window)
        //{
        //    Event e;
        //    while(window.PollEvent(e))

        //}
    }
}
