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
        }
    }
}
