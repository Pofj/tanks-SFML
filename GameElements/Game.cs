using game.GameObject;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace game.GameElements
{
    class Game
    {

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
            float k = KeyIsPress.OldWidth / KeyIsPress.CurrentWidth;
            k = KeyIsPress.OldHeight / KeyIsPress.CurrentHeigh;
            KeyIsPress.CursorPositionX = e.X * ((float)KeyIsPress.OldWidth / KeyIsPress.CurrentWidth); KeyIsPress.CursorPositionY = e.Y *((float)KeyIsPress.OldHeight/KeyIsPress.CurrentHeigh);
            
        }
        private void CheckCorrectCreation()
        {
            if (BackgroundTexture == null) Console.WriteLine("Error to load texture on game class}");
            if (BackgroundSprite == null) Console.WriteLine("Error to create sprite or on class game class");
        }
        public void ChangeResizeKoef(float k, float k2)
        {
            for(int i = 0; i < AllObjects.Count; i++)
            {
                AllObjects[i].SetResizeKoef(k,k2);
            }
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
        public void DeleteDeadObject()
        {
            for (int i =0; i<AllObjects.Count;i++)
            {
                if (!(AllObjects[i].isAlive())) AllObjects.RemoveAt(i);
            }
        }
        public void LoadLevel()
        {
            
            Box temp = new Box(800, 400);
            Tank temp2 = new Tank(this,100, 200, 0);
            BreakingBox temp3 = new BreakingBox(300, 300);
            Cannon temp4 = new Cannon(this, 400, 400);
            HealBox temp5 = new HealBox(100, 100);
            temp2.Dead += DeleteDeadObject;
            temp3.Dead += DeleteDeadObject;
            temp4.Dead += DeleteDeadObject;
            AllObjects.Add(temp2);
            AllObjects.Add(temp3);
            AllObjects.Add(temp);
            AllObjects.Add(temp4);
            AllObjects.Add(temp5);
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
