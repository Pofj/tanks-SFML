using game.Global;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace game.GameElements
{
    class HUD
    {
        protected uint HP = 9;
        protected uint CountEnemy = 5;
        protected float ObjectSize = 0.12f;
        protected SFML.Graphics.ConvexShape FrameHP;
        protected SFML.Graphics.ConvexShape StripHP;
        protected SFML.Graphics.ConvexShape FrameEnemyC;
        protected SFML.Graphics.ConvexShape StripEnemyC;
        protected SFML.Graphics.Text TextHP;
        protected SFML.Graphics.Text CountEnemyText;
        protected SFML.Graphics.Font TextFont;
        protected Texture TankIconTexture;
        protected Sprite TankIconSprite;
        //private Vector2f LeftTopPoint;
        //private Vector2f RightTopPoint;
        //private Vector2f LeftBottomPoint;
        //private Vector2f RightBottomPoint;
        public HUD(uint CountEnemy, Vector2f ViewCenter)
        {
            this.CountEnemy = CountEnemy;

            FrameHP = new SFML.Graphics.ConvexShape();
            StripHP = new SFML.Graphics.ConvexShape();
            FrameEnemyC = new SFML.Graphics.ConvexShape();
            StripEnemyC = new SFML.Graphics.ConvexShape();
            TextHP = new SFML.Graphics.Text();
            CountEnemyText = new SFML.Graphics.Text();


            float k = (float)2.1;
            float x = ViewCenter.X;
            float y = ViewCenter.Y;
            uint Width = InfoAboutResolution.CurrentWidth;
            uint Height = InfoAboutResolution.CurrentHeight;
            Vector2f LeftTopPoint = new Vector2f(x - Width / (float)2.15, y - Height / (float)2.15);
            Vector2f RightTopPoint = new Vector2f(LeftTopPoint.X + Width / 3, LeftTopPoint.Y);
            Vector2f RightBottomPoint = new Vector2f(RightTopPoint.X - Width / 80, y - Height / (float)2.3);
            Vector2f LeftBottomPoint = new Vector2f(LeftTopPoint.X - Width / 80, RightBottomPoint.Y);

            FrameHP.SetPointCount(4);
            FrameHP.SetPoint(0, LeftTopPoint);
            FrameHP.SetPoint(1, RightTopPoint);
            FrameHP.SetPoint(2, RightBottomPoint);
            FrameHP.SetPoint(3, LeftBottomPoint);
            FrameHP.OutlineColor = new Color(10, 10, 10);
            FrameHP.OutlineThickness = Width/130;
            FrameHP.FillColor = new Color(33, 33, 33);

            StripHP.SetPointCount(4);
            StripHP.SetPoint(0, LeftTopPoint);
            StripHP.SetPoint(1, RightTopPoint);
            StripHP.SetPoint(2, RightBottomPoint);
            StripHP.SetPoint(3, LeftBottomPoint);
            StripHP.FillColor = new Color(255, 200, 0, 255);

            float PlusX = 0;
            if (this.CountEnemy > 10)
            {
                int temp = CountEnemy.ToString().Length;
                PlusX = 10 * temp * k;
            }
            LeftTopPoint = new Vector2f(875 * k - PlusX, 20 * k);
            RightTopPoint = new Vector2f(875 * k + 135 * k, 20 * k);
            RightBottomPoint = new Vector2f(875 * k + 135 * k, 110 * k);
            LeftBottomPoint = new Vector2f(875 * k + 20 * k - PlusX, 110 * k);
            FrameEnemyC.SetPointCount(4);
            FrameEnemyC.SetPoint(0, LeftTopPoint);
            FrameEnemyC.SetPoint(1, RightTopPoint);
            FrameEnemyC.SetPoint(2, RightBottomPoint);
            FrameEnemyC.SetPoint(3, LeftBottomPoint);
            FrameEnemyC.OutlineColor = new Color(10, 10, 10);
            FrameEnemyC.OutlineThickness = 5.0f * k;
            FrameEnemyC.FillColor = new Color(33, 33, 33);

            StripEnemyC.SetPointCount(4);
            StripEnemyC.SetPoint(0, LeftTopPoint);
            StripEnemyC.SetPoint(1, RightTopPoint);
            StripEnemyC.SetPoint(2, RightBottomPoint);
            StripEnemyC.SetPoint(3, LeftBottomPoint);
            StripEnemyC.FillColor = new Color(255, 200, 0, 255);


            TextFont = new SFML.Graphics.Font("D:\\projects\\c# tank\\game\\Resources\\Fonts\\impact.ttf");
            TextHP.Font = TextFont;
            TextHP.Font = TextFont;
            CountEnemyText.Font= TextFont;
            TextHP.CharacterSize = (uint)(31 * k);
            CountEnemyText.CharacterSize = (uint)(60 * k);
            TextHP.FillColor = new Color(33, 33, 33);
            CountEnemyText.FillColor = new Color(33, 33, 33);
            TextHP.Style = SFML.Graphics.Text.Styles.Bold;
            CountEnemyText.Style = SFML.Graphics.Text.Styles.Bold;

            TextHP.Position = new Vector2f(35*k, 16*k);
            CountEnemyText.Position = new Vector2f(895 * k + 75 * k - PlusX, 25 * k);



            //установка параметров строки здоровья, установка шрифта
            TankIconTexture = new SFML.Graphics.Texture("D:\\projects\\c# tank\\game\\Resources\\ObjectHUD\\HUD_tank.png");
            TankIconSprite = new SFML.Graphics.Sprite(TankIconTexture);
            TankIconSprite.Position = new Vector2f(885 * k - PlusX,47 * k);
            TankIconSprite.Scale = new SFML.System.Vector2f(ObjectSize * k, ObjectSize * k);
            CheckCorrectCreation();
        }
        private void CheckCorrectCreation()
        {
            if (TankIconTexture == null) Console.WriteLine("Error to load texture on class HUD");
            if (TankIconSprite == null) Console.WriteLine("Error to create sprite or on class HUD");
            if (TextFont == null) Console.WriteLine("Error to create FONT or on class HUD");
        }
        public void SetInfoBot(uint Count)
        {
            CountEnemy = Count;
        }
        public void PopHUD(Vector2f ViewCenter)
        {
            float k = (float)2.1;
            float x = ViewCenter.X;
            float y = ViewCenter.Y;
            uint Width = InfoAboutResolution.CurrentWidth;
            uint Height = InfoAboutResolution.CurrentHeight;
            Vector2f LeftTopPoint = new Vector2f(x - (Width / (float)2.15), y - (Height / (float)2.15));
            Vector2f RightTopPoint = new Vector2f(LeftTopPoint.X + Width / 3, LeftTopPoint.Y);
            Vector2f RightBottomPoint = new Vector2f(RightTopPoint.X - Width / 80, y - Height / (float)2.3);
            Vector2f LeftBottomPoint = new Vector2f(LeftTopPoint.X - Width / 80, RightBottomPoint.Y);

            FrameHP.SetPoint(0, LeftTopPoint);
            FrameHP.SetPoint(1, RightTopPoint);
            FrameHP.SetPoint(2, RightBottomPoint);
            FrameHP.SetPoint(3, LeftBottomPoint);

            StripHP.SetPoint(0, LeftTopPoint);
            StripHP.SetPoint(1, RightTopPoint);
            StripHP.SetPoint(2, RightBottomPoint);
            StripHP.SetPoint(3, LeftBottomPoint);
        }
        public void GetDamage()
        {
            HP -= 1;
        }
        public void GetHeal()
        {
            HP += 3;
        }
        public void MoveHUD(Vector2f ViewCenter)
        {
            //float k = (float)2.1;
            //float x = ViewCenter.X;
            //float y = ViewCenter.Y;
            //uint Width = InfoAboutResolution.CurrentWidth;
            //uint Height = InfoAboutResolution.CurrentHeight;
            //Vector2f LeftTopPoint = new Vector2f(x - Width / (float)2.15, y - Height / (float)2.1);
            //Vector2f RightTopPoint = new Vector2f(LeftTopPoint.X + Width / 3, LeftTopPoint.Y);
            //Vector2f RightBottomPoint = new Vector2f(RightTopPoint.X - Width / 38, y - Height / (float)2.3);
            //Vector2f LeftBottomPoint = new Vector2f(LeftTopPoint.X - Width / 38, RightBottomPoint.Y);

            //FrameHP.SetPointCount(4);
            //FrameHP.SetPoint(0, LeftTopPoint);
            //FrameHP.SetPoint(1, RightTopPoint);
            //FrameHP.SetPoint(2, RightBottomPoint);
            //FrameHP.SetPoint(3, LeftBottomPoint);
        }
        public void Show(in RenderWindow window)
        {
            window.Draw(FrameEnemyC);
            window.Draw(StripEnemyC);
            window.Draw(FrameHP);
            window.Draw(StripHP);
            window.Draw(TextHP);
            window.Draw(CountEnemyText);
            window.Draw(TankIconSprite);
        }
    }
}
