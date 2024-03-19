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
        public Bullet(float x, float y,double angle, GameObjectType ObjectType,float SizeObject, string imagePath = "D:\\projects\\c# tank\\game\\Resources\\ObjectTexture\\bullet_Ver2.png") : base(x, y, ObjectType,SizeObject, imagePath)
        {
            Angle = angle;
            ObjectSprite.Scale = new Vector2f(SizeObject * (float)0.9, SizeObject * (float)1.3);
            ObjectSprite.Rotation = (float)Angle;
            this.Position.X = x;
            this.Position.Y = y;
            ObjectSprite.Position = this.Position;
        }
        protected override bool CheckKollision(GameObjectType Type)
        {
            //if (Type == GameObjectType.Player && getType() == GameObjectType.EnemyBullet)
            //{
            //    Kill();
            //    return true;
            //}
            //if (Type == GameObjectType.Player)
            //{
            //    return true;
            //}
            //if (Type == GameObjectType.PlayerBullet || Type == GameObjectType.EnemyBullet)
            //{
            //    return false;
            //}
            if (Type == GameObjectType.Player && getType() == GameObjectType.PlayerBullet) return false;
            Kill();
            return true;
        }
        private void Move(in List<Box> AllObjects)
        {
            float x_1 = ((float)(Position.X + Math.Sin(Angle * MathConst.M_PI / 180.0))) - 5; // *10
            float y_1 = ((float)(Position.Y - Math.Cos(Angle * MathConst.M_PI / 180.0) * 1)) - 5; // *10
            float x_2 = (float)(Position.X + Math.Cos((45 - Angle) * MathConst.M_PI / 180.0) / Math.Sqrt(2)); //*23
            float y_2 = (float)(Position.Y - Math.Sin((45 - Angle) * MathConst.M_PI / 180.0) / Math.Sqrt(2)); //*23

            float x_3 = 2 * x_1 - x_2;
            float y_3 = 2 * y_1 - y_2;

            Vector2f point = new Vector2f(x_1, y_1);
            Vector2f point_2 = new Vector2f(x_2, y_2);
            Vector2f point_3 = new Vector2f(x_3, y_3);
            SFML.Graphics.CircleShape circle = new SFML.Graphics.CircleShape();
            //SFML.Graphics.CircleShape circle2 = new SFML.Graphics.CircleShape();
            //SFML.Graphics.CircleShape circle3 = new SFML.Graphics.CircleShape();
            circle.Position = point;
            circle.Radius = 8;
            ////circle.FillColor = Color.Red;
            ////circle2.Position = point_2;
            ////circle2.Radius = 8;
            ////circle2.FillColor = Color.Red;
            ////circle3.Position = point_3;
            ////circle3.Radius = 8;
            ////circle3.FillColor = Color.Red;
            //KeyIsPress.temp.Draw(circle);
            //KeyIsPress.temp.Draw(circle2);
            //KeyIsPress.temp.Draw(circle3);

            foreach (var obj in AllObjects)
            {

                Vector2f position_point_obj = obj.getPosition();
                GameObjectType typeSomeObj = obj.getType();
                if ((typeSomeObj != ObjectType) && (typeSomeObj != GameObjectType.PlayerBullet))
                {
                    FloatRect objBounds = obj.getRect();
                    if (objBounds.Contains(point.X, point.Y)) // обьект получает ссылку сам на себя в евенте
                    {
                        obj.OnKollision(this.getType());
                        if (this.OnKollision(obj.getType())) return;
                        //if (this.OnKollision(obj.getType()))
                        //{
                        //    obj.OnKollision(this.getType());
                        //}
                        //return;
                    }
                    else if (objBounds.Contains(point_2.X, point_2.Y))
                    {
                        obj.OnKollision(this.getType());
                        if (this.OnKollision(obj.getType())) return;
                    }
                    else if (objBounds.Contains(point_3.X, point_3.Y))
                    {
                        obj.OnKollision(this.getType());
                        if (this.OnKollision(obj.getType())) return;
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
