using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.GameElements
{
    class HUD
    {
        protected uint HP = 10;
        protected uint CountEnemy = 5;
        protected float ObjectSize = 0.14f;
        protected SFML.Graphics.ConvexShape FrameHP;
        protected SFML.Graphics.ConvexShape StripHP;
        protected SFML.Graphics.ConvexShape FrameEnemyC;
        protected SFML.Graphics.ConvexShape StripEnemyC;
        protected Text TextHP;
        protected Text CountEnemyText;
        protected Font Font;
        protected Texture TankIconTexture;
        protected Sprite TankIconSprite;
        public HUD()
        {
            //установка параметров строки здоровья, установка шрифта
            TankIconTexture = new SFML.Graphics.Texture("D:\\projects\\c# tank\\game\\Resources\\ObjectHUD\\HUD_tank.png");
            TankIconSprite = new SFML.Graphics.Sprite(TankIconTexture);
            // ObjectSprite.Position = this.Position; установ на позицию
            TankIconSprite.Scale = new SFML.System.Vector2f(ObjectSize, ObjectSize);
            CheckCorrectCreation();
        }
        private void CheckCorrectCreation()
        {
            if (TankIconTexture == null) Console.WriteLine("Error to load texture on class HUD");
            if (TankIconSprite == null) Console.WriteLine("Error to create sprite or on class HUD");
            if (Font == null) Console.WriteLine("Error to create FONT or on class HUD");
        }
    }
}
