using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game.GameElements;
using game.Global;
using SFML.Graphics;
using SFML.System;

namespace game.GameObject
{
    class Bullet : Box
    {
        private uint Speed = 5;
        private double Angle;
        public Bullet(float x, float y,double angle, GameObjectType ObjectType, string imagePath = "D:\\projects\\c# tank\\game\\Resources\\ObjectTexture\\bullet.png") : base(x, y, ObjectType, imagePath) 
        {
            Angle = angle;
            ObjectSprite.Scale = new SFML.System.Vector2f(0.12f, 0.18f);
            ObjectSprite.Rotation = (float)Angle;
        }
        protected override bool CheckKollision(GameObjectType Type)
        {
            Kill();
            return true;
        }
        private void Move(in List<Box> AllObjects)
        {
            float x_1 = (float)(Position.X + Math.Sin(Angle * MathConst.M_PI / 180.0) * 10);
            float y_1 = (float)(Position.Y - Math.Cos(Angle * MathConst.M_PI / 180.0) * 10);
            float x_2 = (float)(Position.X + Math.Cos((45 - Angle) * MathConst.M_PI / 180.0) * 23 / Math.Sqrt(2));
            float y_2 = (float)(Position.Y - Math.Sin((45 - Angle) * MathConst.M_PI / 180.0) * 23 / Math.Sqrt(2));

            float x_3 = 2 * x_1 - x_2;
            float y_3 = 2 * y_1 - y_2;

            Vector2f point = new Vector2f(x_1, y_1);
            Vector2f point_2 = new Vector2f(x_2, y_2);
            Vector2f point_3 = new Vector2f(x_3, y_3);

            foreach (var obj in AllObjects)
            {

                Vector2f position_point_obj = obj.getPosition();
                GameObjectType typeSomeObj = obj.getType();
                if ((typeSomeObj != ObjectType) && (typeSomeObj != GameObjectType.PlayerBullet))
                {
                    FloatRect objBounds = obj.getRect();
                    if (objBounds.Contains(point.X, point.Y)) // обьект получает ссылку сам на себя в евенте
                    {
                        if (this.OnKollision(obj.getType()))
                        {
                            obj.OnKollision(this.getType());
                        }
                        return;
                    }
                    else if (objBounds.Contains(point_2.X, point_2.Y))
                    {
                        if (this.OnKollision(obj.getType()))
                        {
                            obj.OnKollision(this.getType());
                        }
                        return;
                    }
                    else if (objBounds.Contains(point_3.X, point_3.Y))
                    {
                        if (this.OnKollision(obj.getType()))
                        {
                            obj.OnKollision(this.getType());
                        }
                        return;
                    }
                }
            }
            Position.X += (float)Math.Sin(Angle * MathConst.M_PI / 180.0) * Speed;
            Position.Y -= (float)Math.Cos(Angle * MathConst.M_PI / 180.0) * Speed;
            ObjectSprite.Position = new Vector2f(Position.X, Position.Y);
        }
        public override void Action(in List<Box> AllObjects)
        {
            Move(AllObjects);
        }
    }
}
