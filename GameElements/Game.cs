using game.GameObject;
using game.Global;
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
        private float SizeBackground = 1f;
        private bool GameIsOn = true;
        private uint AliveBotCount = 1;
        private Texture BackgroundTexture;
        private Sprite BackgroundSprite;
        private View ViewPlayer;
        HUD PlayerHUD;
        List<game.GameObject.Box> AllObjects;
        public Game()
        {
            float SizeBackgroundX = SizeBackground * InfoAboutResolution.RatioWidthResolution;
            float SizeBackgroundY = SizeBackground * InfoAboutResolution.RatioWidthResolution;

            AllObjects = new List<game.GameObject.Box>();
            BackgroundTexture = new Texture("D:\\projects\\c# tank\\game\\Resources\\Backgrounds\\background.png");
            BackgroundSprite = new Sprite(BackgroundTexture);
            CheckCorrectCreation();

            BackgroundSprite.Scale = new SFML.System.Vector2f(SizeBackgroundX, SizeBackgroundY);
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
        private void CheckCorrectCreation()
        {
            if (BackgroundTexture == null) Console.WriteLine("Error to load texture on game class}");
            if (BackgroundSprite == null) Console.WriteLine("Error to create sprite or on class game class");
        }
        public void ChangeSize(object sender, EventArgs e)
        {
            ViewPlayer.Size = new Vector2f(InfoAboutResolution.CurrentWidth, InfoAboutResolution.CurrentHeight);
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
            Box temp = new Box(100, 400);
            Tank temp2 = new Tank(this, 100, 200, 0);
            ViewPlayer = new View(temp2.getPosition(), new Vector2f(InfoAboutResolution.CurrentWidth, InfoAboutResolution.CurrentHeight));
            PlayerHUD = new HUD(12,ViewPlayer.Center);
            temp2.TankMove += PlayerHUD.MoveHUD;
            temp2.TakeDamage += PlayerHUD.GetDamage;
            temp2.GetHeal += PlayerHUD.GetHeal;
            BreakingBox temp3 = new BreakingBox(300, 300);
            Cannon temp4 = new Cannon(this, 400, 400);
            temp4.Dead += this.BotDead;
            HealBox temp5 = new HealBox(107, 107);
            temp2.Dead += DeleteDeadObject;
            temp3.Dead += DeleteDeadObject;
            temp4.Dead += DeleteDeadObject;
            AllObjects.Add(temp2);
            AllObjects.Add(temp3);
            AllObjects.Add(temp);
            // AllObjects.Add(temp4);
            AllObjects.Add(temp5);
        }
        private void Update()
        {
            for (int i = 0; i < AllObjects.Count; i++)
            {
                AllObjects[i].Action(AllObjects);
            }
            UpdateView();
            PlayerHUD.PopHUD(ViewPlayer.Center);
        }
        private void BotDead()
        {
            PlayerHUD.SetInfoBot(AliveBotCount);
            if (AliveBotCount == 0)
            {
                EndGame();
            }
        }
        private void UpdateView()
        {
            ViewPlayer.Center = AllObjects[0].getPosition();
        }
        public void Show(in RenderWindow window)
        {
          //  Update(); verno
            window.SetView(ViewPlayer);
            window.Draw(BackgroundSprite);
            KeyIsPress.temp2.Draw(BackgroundSprite);
            Update();
            for (int i = 0; i < AllObjects.Count; i++)
            {
                AllObjects[i].Show(window);
                AllObjects[i].Show(KeyIsPress.temp2);
            }
            PlayerHUD.Show(window);
            PlayerHUD.Show(KeyIsPress.temp2);
        }
    }
}
