using game.GameElements;
using game.Global;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.GameObject
{
    class Box
    {
        //public delegate void ObjectHandler(GameObjectType Type);
        //public event ObjectHandler? Kollision;

        public delegate bool KollisionHandler(GameObjectType Type);
        public event KollisionHandler Kollision; //события коллизии
        public delegate void ObjectHandler();
        public event ObjectHandler Dead;

        protected bool alive = true;
        protected GameObjectType ObjectType;

        protected SFML.System.Vector2f Position;
        protected SFML.Graphics.Texture ObjectTexture;
        protected SFML.Graphics.Sprite ObjectSprite;
        protected float SizeObject;

        //objWithEvent* game;
        public Box(float x, float y, GameObjectType ObjectType = GameObjectType.Box,float SizeObject = 0.8f, string imagePath = "D:\\projects\\c# tank\\game\\Resources\\ObjectTexture\\default_box.png")
        {
            this.SizeObject = SizeObject;
            this.ObjectType = ObjectType;
            this.Position.X = x * InfoAboutResolution.RatioWidthResolution;
            this.Position.Y = y * InfoAboutResolution.RatioWidthResolution;
            float SizeObjectX = SizeObject * InfoAboutResolution.RatioWidthResolution;
            float SizeObjectY = SizeObject * InfoAboutResolution.RatioWidthResolution;

            //InfoAboutResolution.RatioWidthResolution = (float)InfoAboutResolution.CurrentWidth / (float)InfoAboutResolution.OriginalWidth / (InfoAboutResolution.CurrentRatio > InfoAboutResolution.OriginalRatio ? (InfoAboutResolution.CurrentRatio / InfoAboutResolution.OriginalRatio) : 1);
            //InfoAboutResolution.RatioHeightResolution = (float)InfoAboutResolution.CurrentHeight / (float)InfoAboutResolution.OriginalHeight / (InfoAboutResolution.CurrentRatio < InfoAboutResolution.OriginalRatio ? (InfoAboutResolution.CurrentRatio / InfoAboutResolution.OriginalRatio) : 1);





            //float SizeObjectX = SizeObject * (float)InfoAboutResolution.CurrentWidth / (float)InfoAboutResolution.OriginalWidth;
            //float SizeObjectY = SizeObject * (float)InfoAboutResolution.CurrentHeight / (float)InfoAboutResolution.OriginalHeight;
            ObjectTexture = new SFML.Graphics.Texture(imagePath);
            ObjectSprite = new SFML.Graphics.Sprite(ObjectTexture);
            CheckCorrectCreation();
            
            this.ObjectSprite.Origin = new Vector2f(ObjectSprite.GetLocalBounds().Width / 2, ObjectSprite.GetLocalBounds().Height / 2);
            ObjectSprite.Position = this.Position;
            ObjectSprite.Scale = new SFML.System.Vector2f(SizeObjectX, SizeObjectY);
            Kollision += CheckKollision;
        }
        protected void Kill()
        {
            this.alive = false;
            if (Dead != null) Dead.Invoke();
        }
        public virtual bool OnKollision(GameObjectType Typе)
        {
            return Kollision?.Invoke(Typе) ?? false;
        }
        protected virtual bool CheckKollision(GameObjectType Type)
        {
            return true;
        }
        private void CheckCorrectCreation()
        {
            if (ObjectTexture == null) Console.WriteLine("Error to load texture on class {0}", ObjectType);
            if (ObjectSprite == null) Console.WriteLine("Error to create sprite or on class {0}", ObjectType);
        }
        public bool isAlive()
        {
            return alive;
        }
        public GameObjectType getType()
        {
            return ObjectType;
        }
        public Vector2f getPosition()
        {
            return Position;
        }
        public virtual void ChangeSize()
        {
            float SizeObjectX = SizeObject * InfoAboutResolution.RatioWidthResolution;
            float SizeObjectY = SizeObject * InfoAboutResolution.RatioWidthResolution;
            //ObjectSprite = new SFML.Graphics.Sprite(ObjectTexture);
            //this.ObjectSprite.Origin = new Vector2f(ObjectSprite.GetLocalBounds().Width / 2, ObjectSprite.GetLocalBounds().Height / 2);
            //ObjectSprite.Position = this.Position;
            ObjectSprite.Scale = new SFML.System.Vector2f(SizeObjectX, SizeObjectY);
            //Position.X = Position.X * ((float)InfoAboutResolution.PreviousWidth / InfoAboutResolution.CurrentWidth);
            //Position.Y = Position.Y * ((float)InfoAboutResolution.PreviousHeight / InfoAboutResolution.CurrentHeight);
            //ObjectSprite.Position = this.Position;
        }
        public virtual void Show(in RenderWindow window)
        {
            if (alive)
            {
                ObjectSprite.Position = this.Position;
                window.Draw(ObjectSprite);
            }
        }
        public FloatRect getRect()
        {
            return ObjectSprite.GetGlobalBounds();
        }
        public virtual void Action(in List<game.GameObject.Box> AllObjects)
        {
            return;
        }

    }
}
