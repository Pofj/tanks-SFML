using game.GameObject;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.GameElements
{
    class Game
    {
        public delegate void GameHandler(SFML.Window.MouseMoveEventArgs e);
        public event GameHandler? MouseMove;
        private float SizeBackground = 1.1f;
        private bool GameIsOn = true;
        private int AliveBotCount;
        private Texture BackgroundTexture;
        private Sprite BackgroundSprite;
        HUD PlayerHUD;
        List<game.GameObject.Box> AllObjects;
        public Game()
        {
            AllObjects = new List<game.GameObject.Box>();
            BackgroundTexture = new Texture("D:\\projects\\c# tank\\game\\Resources\\Backgrounds\\background.png");
            BackgroundSprite = new Sprite(BackgroundTexture);
            CheckCorrectCreation();
            BackgroundSprite.Scale = new SFML.System.Vector2f(SizeBackground, SizeBackground);
            BackgroundSprite.Origin = new Vector2f(0, 0);
        }
        public void KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            if (!GameIsOn) return;

            if (e.Code == Keyboard.Key.W)
            {
                KeyIsPress.isWPressed = true;
            }
            if (e.Code == Keyboard.Key.S)
            {
                KeyIsPress.isSPressed = true;
            }
            if (e.Code == Keyboard.Key.A)
            {
                KeyIsPress.isAPressed = true;
            }
            if (e.Code == Keyboard.Key.D)
            {
                KeyIsPress.isDPressed = true;
            }

        }
        public void LeftClick(object sender, SFML.Window.MouseButtonEventArgs e)
        {
            if (!GameIsOn || e.Button != Mouse.Button.Left) return;
            
            KeyIsPress.leftClick = true;
        }
        public void KeyReleased(object sender, SFML.Window.KeyEventArgs e)
        {
            if (!GameIsOn) return;

            if(e.Code == Keyboard.Key.W)
            {
                KeyIsPress.isWPressed = false;
            }
            if (e.Code == Keyboard.Key.S)
            {
                KeyIsPress.isSPressed = false;
            }
            if (e.Code == Keyboard.Key.A)
            {
                KeyIsPress.isAPressed = false;
            }
            if (e.Code == Keyboard.Key.D)
            {
                KeyIsPress.isDPressed = false;
            }
        
        }
        public void MouseMoved(object sender, SFML.Window.MouseMoveEventArgs e)
        {
            MouseMove.Invoke(e);
        }
        private void CheckCorrectCreation()
        {
            if (BackgroundTexture == null) Console.WriteLine("Error to load texture on game class}");
            if (BackgroundSprite == null) Console.WriteLine("Error to create sprite or on class game class");
        }
        public void ChangeSize(float NewSize)
        {
            BackgroundSprite.Scale = new SFML.System.Vector2f(NewSize, NewSize);
            SizeBackground = NewSize;
        }
        public bool GetGameStatus()
        {
            return GameIsOn;
        }
        public void StartGame()
        {
            GameIsOn = true;
        }
        public void EndGame()
        {
            GameIsOn = false;
        }
        public void LoadLevel()
        {
            Box temp = new Box(800, 400);
            Tank temp2 = new Tank(400, 300, 0);
            MouseMove += temp2.UpdateCursorPos;
            AllObjects.Add(temp);
            AllObjects.Add(temp2);
        }
        private void Update(in RenderWindow window)
        {
            for (int i = 0; i < AllObjects.Count; i++)
            {
                AllObjects[i].Action(AllObjects);
            }
        }
        public void show(in RenderWindow window)
        {
            window.Draw(BackgroundSprite);
            Update(window);
            for (int i = 0; i < AllObjects.Count; i++)
            {
                AllObjects[i].Show(window);
            }
            //player_HUD.show(window);
        }
    }
}
