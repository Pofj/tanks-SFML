using game.GameElements;
using game.Global;
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
        public delegate void TankHandler();
        public event TankHandler TakeDamage;
        public event TankHandler GetHeal;
        public delegate void HandlerHUD(Vector2f ViewCenter);
        public event HandlerHUD TankMove;
        static protected Game GamePt;
        protected float TimeSinceLastShot = 0.11f;
        private float Speed = 4;
        protected double Angle;
        protected double AngleCannon;
        protected uint HP = 10;
        private float SizeCannon = 0.14f;
        protected Sprite CannonSprite;
        private Texture CannonTexture;
        private double a=1, b=1;
        public Tank(in Game GamePt,float x, float y, uint angle, GameObjectType ObjectType = GameObjectType.Player, float SizeObject = 0.11f, string imagePath = "D:\\projects\\c# tank\\game\\Resources\\ObjectTexture\\ally_tank.png", string ImagePathCannon = "D:\\projects\\c# tank\\game\\Resources\\ObjectTexture\\tank_cannon.png") : base(x, y, ObjectType,SizeObject, imagePath)
        {
            Tank.GamePt = GamePt;
            this.Angle = angle;
            AngleCannon = angle;
            Speed = Speed * InfoAboutResolution.RatioWidthResolution;

            float SizeCannonX = 0.14f * InfoAboutResolution.RatioWidthResolution;
            float SizeCannonY = 0.14f * InfoAboutResolution.RatioWidthResolution;


            CannonTexture = new SFML.Graphics.Texture(ImagePathCannon);
            CannonSprite = new SFML.Graphics.Sprite(CannonTexture);
            CheckCorrectTankCreation();

            CannonSprite.Origin = new Vector2f(133, 399);
            CannonSprite.Position = this.Position;
            CannonSprite.Scale = new SFML.System.Vector2f(SizeCannonX, SizeCannonY);
            //this.ObjectSprite.Origin = new Vector2f(ObjectSprite.GetLocalBounds().Width / 2, ObjectSprite.GetLocalBounds().Height / 2);
        }
        protected void CheckCorrectTankCreation()
        {
            if (ObjectTexture == null) Console.WriteLine("Error to load texture on class {0}", ObjectType);
            if (ObjectSprite == null) Console.WriteLine("Error to create sprite or on class {0}", ObjectType);
        }
        //public override void ChangeSize()
        //{
        //    float SizeCannonX = SizeCannon * InfoAboutResolution.RatioWidthResolution;
        //    float SizeCannonY = SizeCannon * InfoAboutResolution.RatioWidthResolution;
        //    CannonSprite.Scale = new SFML.System.Vector2f(SizeCannonX, SizeCannonY);
        //    base.ChangeSize();
        //}
        public override void Show(in RenderWindow window)
        {
            if (isAlive())
            {
                window.Draw(ObjectSprite);
                window.Draw(CannonSprite);
            }
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
            //int k_x = (int)(88 * InfoAboutResolution.RatioWidthResolution);
            //int k_y = (int)(88 * InfoAboutResolution.RatioHeightResolution);
            float x = Position.X + 47 * InfoAboutResolution.RatioWidthResolution * (float)Math.Sin(AngleCannon * MathConst.M_PI / 180.0); //50
            float y = Position.Y - 47 * InfoAboutResolution.RatioWidthResolution * (float)Math.Cos(AngleCannon * MathConst.M_PI / 180.0);
            Bullet temp_bullet = new Bullet(x,y,AngleCannon, GameObjectType.PlayerBullet,CannonSprite.Scale.X);
            temp_bullet.Dead += GamePt.DeleteDeadObject;
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

                    float x_1 = (float)(Position.X + Math.Sin(Angle * MathConst.M_PI / 180.0) * (29 * InfoAboutResolution.RatioWidthResolution)); // 29 33
                    float y_1 = (float)(Position.Y - Math.Cos(Angle * MathConst.M_PI / 180.0) * (29 * InfoAboutResolution.RatioWidthResolution));
                    float x_2 = (float)((Position.X + Math.Cos((45 - Angle) * MathConst.M_PI / 180.0) * (58 * InfoAboutResolution.RatioWidthResolution) / Math.Sqrt(2)) - 2.5); // 58 66
                    float y_2 = (float)(Position.Y - Math.Sin((45 - Angle) * MathConst.M_PI / 180.0) * (58 * InfoAboutResolution.RatioWidthResolution) / Math.Sqrt(2));

                    float x_3 = (2 * x_1 - x_2);
                    float y_3 = 2 * y_1 - y_2;

                    Vector2f point = new Vector2f(x_1, y_1);
                    Vector2f point_2 = new Vector2f(x_2, y_2);
                    Vector2f point_3 = new Vector2f(x_3, y_3);
                    SFML.Graphics.CircleShape circle = new SFML.Graphics.CircleShape();
                    SFML.Graphics.CircleShape circle2 = new SFML.Graphics.CircleShape();
                    SFML.Graphics.CircleShape circle3 = new SFML.Graphics.CircleShape();
                    circle.Position = point;
                    circle.Radius = 3;
                    circle.FillColor = Color.Red;
                    circle2.Position = point_2;
                    circle2.Radius = 3;
                    circle2.FillColor = Color.Red;
                    circle3.Position = point_3;
                    circle3.Radius = 3;
                    circle3.FillColor = Color.Red;
                    KeyIsPress.temp.Draw(circle);
                    KeyIsPress.temp.Draw(circle2);
                    KeyIsPress.temp.Draw(circle3);
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
                    Vector2f Pos = new Vector2f(Position.X, Position.Y);
                    ObjectSprite.Position = Pos;
                    CannonSprite.Position = Pos;
                    TankMove.Invoke(Pos);
                }
                if (KeyIsPress.isSPressed)
                {
                    float x_1 = (float)(Position.X - Math.Sin(Angle * MathConst.M_PI / 180.0) * 29 * InfoAboutResolution.RatioWidthResolution);
                    float y_1 = (float)(Position.Y + Math.Cos(Angle * MathConst.M_PI / 180.0) * 29 * InfoAboutResolution.RatioWidthResolution);
                    float x_2 = (float)(Position.X - Math.Cos((45 - Angle) * MathConst.M_PI / 180.0) * 58 * InfoAboutResolution.RatioWidthResolution / Math.Sqrt(2));
                    float y_2 = (float)(Position.Y + Math.Sin((45 - Angle) * MathConst.M_PI / 180.0) * 58 * InfoAboutResolution.RatioWidthResolution / Math.Sqrt(2));

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
                    Vector2f Pos = new Vector2f(Position.X, Position.Y);
                    ObjectSprite.Position = Pos;
                    CannonSprite.Position = Pos;
                    TankMove.Invoke(Pos); ;
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
            a = Math.Sqrt(Math.Pow((InfoAboutMouse.CursorPositionX - InfoAboutResolution.CurrentWidth/2), 2) + Math.Pow((InfoAboutMouse.CursorPositionY - InfoAboutResolution.CurrentHeight/2), 2)); if (a == 0) a = 0.0001;
            b = Math.Abs(InfoAboutMouse.CursorPositionY - InfoAboutResolution.CurrentHeight/2);
            //a = Math.Sqrt(Math.Pow((InfoAboutMouse.CursorPositionX - Position.X), 2) + Math.Pow((InfoAboutMouse.CursorPositionY - Position.Y), 2)); if (a == 0) a = 0.0001;
            //b = Math.Abs(InfoAboutMouse.CursorPositionY - Position.Y);

            if (InfoAboutMouse.CursorPositionY > (InfoAboutResolution.CurrentHeight / 2))
            {
                AngleCannon = 180 - Math.Acos(b / a) * (180 / MathConst.M_PI);
                if (InfoAboutMouse.CursorPositionX < (InfoAboutResolution.CurrentWidth / 2))
                {
                    AngleCannon = 360 - AngleCannon;
                }
            }
            else
            {
                AngleCannon = Math.Acos(b / a) * (180 / MathConst.M_PI);
                if (InfoAboutMouse.CursorPositionX < (InfoAboutResolution.CurrentWidth / 2))
                {
                    AngleCannon = 360 - AngleCannon;
                }
            }

            if (AngleCannon == 360) AngleCannon = 0;
            CannonSprite.Rotation = (float)AngleCannon;
        }
        protected override bool CheckKollision(GameObjectType Type)
        {
            if (Type == GameObjectType.EnemyBullet)
            {
                GetDamage();
                return true;
            }
            if (Type == GameObjectType.HealBox)
            {
                if (HP <= 7)
                {
                    Heal();
                    return true;
                }
                return false;
            }
            return false;
        }
        protected void GetDamage()
        {
            HP -= 1;
            if (HP == 0)
            {
                Kill();
            }
            TakeDamage?.Invoke();
        }
        private void Heal()
        {
            HP += 3;
            GetHeal?.Invoke();
        }
    }
}
