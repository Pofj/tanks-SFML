using game.GameElements;
using game.SpaceConstant;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace game.GameObject
{
    class Tank : Box
    {
        private float TimeSinceLastShot = 0.11f;
        private uint Speed = 4;
        private double Angle;
        private double AngleCannon;
        private uint HP = 10;
        private float SizeCannon = 0.14f;
        private float SizeTank = 0.11f;
        private Sprite CannonSprite;
        private Texture CannonTexture;
        private double CursorPositionX, CursorPositionY;
        private double a=1, b=1;
        public Tank(float x, float y, uint angle, GameObjectType ObjectType = GameObjectType.Player, string imagePath = "D:\\projects\\c# tank\\game\\Resources\\ObjectTexture\\ally_tank.png", string ImagePathCannon = "D:\\projects\\c# tank\\game\\Resources\\ObjectTexture\\tank_cannon.png") : base(x, y, ObjectType, imagePath)
        {
            this.Angle = angle;
            AngleCannon = angle;
            CannonTexture = new SFML.Graphics.Texture(ImagePathCannon);
            CannonSprite = new SFML.Graphics.Sprite(CannonTexture);
            CheckCorrectTankCreation();
            CannonSprite.Origin = new Vector2f(133, 399);
            CannonSprite.Position = this.Position;
            CannonSprite.Scale = new SFML.System.Vector2f(SizeCannon, SizeCannon);
            ObjectSprite.Scale = new SFML.System.Vector2f(SizeTank, SizeTank);
            //this.ObjectSprite.Origin = new Vector2f(ObjectSprite.GetLocalBounds().Width / 2, ObjectSprite.GetLocalBounds().Height / 2);
        }
        protected void CheckCorrectTankCreation()
        {
            if (ObjectTexture == null) Console.WriteLine("Error to load texture on class {0}", ObjectType);
            if (ObjectSprite == null) Console.WriteLine("Error to create sprite or on class {0}", ObjectType);
        }
        public override void Show(in RenderWindow window)
        {
            if (isAlive())
            {
                window.Draw(ObjectSprite);
                window.Draw(CannonSprite);
            }
        }

        public void UpdateCursorPos(SFML.Window.MouseMoveEventArgs e) //обработка события движения курсора
        {
            
            CursorPositionX = e.X; CursorPositionY = e.Y;
        }
        public override void Action(in List<game.GameObject.Box> AllObjects)
        {
            Move(AllObjects);
            CannonRotation();
            if (KeyIsPress.leftClick)
            {
                Fire(AllObjects);
                KeyIsPress.leftClick = false;
            }

        }
        private void Fire(in List<game.GameObject.Box> AllObjects)
        {
            if ((GlobalTimer.ClockGL.ElapsedTime.AsSeconds() - TimeSinceLastShot) < 0.5) return;
            TimeSinceLastShot = GlobalTimer.ClockGL.ElapsedTime.AsSeconds();
            Bullet temp_bullet = new Bullet(Position.X + 50 * (float)Math.Sin(AngleCannon * SpaceConstant.MathConst.M_PI / 180.0),
                Position.Y - 50 * (float)Math.Cos(AngleCannon *  SpaceConstant.MathConst.M_PI / 180.0),AngleCannon, GameObjectType.PlayerBullet);
            
            AllObjects.Add(temp_bullet);
        }
        public void Move(in List<game.GameObject.Box> AllObjects)
        {
            if (!(KeyIsPress.isAPressed && KeyIsPress.isDPressed))
            {
                if (KeyIsPress.isAPressed)
                {
                    BodyRotation(false);
                }
                if (KeyIsPress.isDPressed)
                {
                    BodyRotation(true);
                }
            }
            if (!(KeyIsPress.isWPressed && KeyIsPress.isSPressed))
            {
                if (KeyIsPress.isWPressed)
                {

                    float x_1 = (float)(Position.X + Math.Sin(Angle * SpaceConstant.MathConst.M_PI / 180.0) * 33);
                    float y_1 = (float)(Position.Y - Math.Cos(Angle * SpaceConstant.MathConst.M_PI / 180.0) * 33);
                    float x_2 = (float)(Position.X + Math.Cos((45 - Angle) * SpaceConstant.MathConst.M_PI / 180.0) * 66 / Math.Sqrt(2));
                    float y_2 = (float)(Position.Y - Math.Sin((45 - Angle) * SpaceConstant.MathConst.M_PI / 180.0) * 66 / Math.Sqrt(2));

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
                    CannonSprite.Position = new Vector2f(Position.X, Position.Y);
                }
                if (KeyIsPress.isSPressed)
                {
                    float x_1 = (float)(Position.X - Math.Sin(Angle * SpaceConstant.MathConst.M_PI / 180.0) * 33);
                    float y_1 = (float)(Position.Y + Math.Cos(Angle * SpaceConstant.MathConst.M_PI / 180.0) * 33);
                    float x_2 = (float)(Position.X - Math.Cos((45 - Angle) * SpaceConstant.MathConst.M_PI / 180.0) * 66 / Math.Sqrt(2));
                    float y_2 = (float)(Position.Y + Math.Sin((45 - Angle) * SpaceConstant.MathConst.M_PI / 180.0) * 66 / Math.Sqrt(2));

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
                    Position.X -= (float)Math.Sin(Angle * MathConst.M_PI / 180.0) * Speed;
                    Position.Y += (float)Math.Cos(Angle * MathConst.M_PI / 180.0) * Speed;
                    ObjectSprite.Position = new Vector2f(Position.X, Position.Y);
                    CannonSprite.Position = new Vector2f(Position.X, Position.Y);
                }
            }
        }
        public void BodyRotation(bool ClockWise)
        {
            if (ClockWise)
            {
                Angle = Angle + 4;
                if (Angle == 360) Angle = 0;
            }
            else
            {
                Angle = Angle - 4;
                if (Angle == -360) Angle = 0;
            }
            this.ObjectSprite.Rotation = (float)Angle;
        }
        private void CannonRotation()
        {
            a = Math.Sqrt(Math.Pow((CursorPositionX - Position.X), 2) + Math.Pow((CursorPositionY - Position.Y), 2));
            b = Math.Abs(CursorPositionY - Position.Y);

            if (CursorPositionY > Position.Y)
            {
                AngleCannon = 180 - Math.Acos(b / a) * (180 / MathConst.M_PI);
                if (CursorPositionX < Position.X)
                {
                    AngleCannon = 360 - AngleCannon;
                }
            }
            else
            {
                AngleCannon = Math.Acos(b / a) * (180 / MathConst.M_PI);
                if (CursorPositionX < Position.X)
                {
                    AngleCannon = 360 - AngleCannon;
                }
            }

            if (AngleCannon == 360) AngleCannon = 0;
            CannonSprite.Rotation = (float)AngleCannon;
        }
    }
}
