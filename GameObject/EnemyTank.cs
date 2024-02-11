using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using game.GameElements;
using game.Global;
using SFML.System;

namespace game.GameObject
{
    class EnemyTank : Tank
    {
        public EnemyTank(Game GamePt,float x, float y, uint angle, GameObjectType ObjectType = GameObjectType.EnemyTank, string imagePath = "D:\\projects\\c# tank\\game\\Resources\\ObjectTexture\\enemy_tank.png", string ImagePathCannon = "D:\\projects\\c# tank\\game\\Resources\\ObjectTexture\\enemy_tank_cannon.png") : base(GamePt,x, y, angle, ObjectType, imagePath, ImagePathCannon) { }

        public override void Action(in List<game.GameObject.Box> AllObjects)
        {
            Vector2f PosTank = AllObjects[0].getPosition();
            float r = (float)Math.Sqrt(Math.Pow(Position.X - PosTank.X, 2) +
                Math.Pow(Position.Y - PosTank.Y, 2));
            bool IsHide = false;
            if (r < 500)
            {
                for (int i = 1; i < AllObjects.Count; i++)
                {
                    GameObjectType type = AllObjects[i].getType();
                    if (type == GameObjectType.PlayerBullet || type == GameObjectType.EnemyBullet || type == GameObjectType.BreakingBox) continue;
                    Vector2f pos_obj = AllObjects[i].getPosition();
                    if (pos_obj == Position) continue;
                    float r_obj = (float)Math.Sqrt(Math.Pow(Position.X - pos_obj.X, 2) + Math.Pow(Position.Y - pos_obj.Y, 2));
                    if (r_obj < 500)
                    {
                        if (Math.Abs(PosTank.X - Position.X) < 1)
                        {

                        }
                        double y;
                        SFML.Graphics.FloatRect rect_obj = AllObjects[i].getRect();
                        for (int j = (int)Math.Min(PosTank.X, Position.X); j <= Math.Max(PosTank.X, Position.X); j++)
                        {
                            y = (((j - Position.X) * (PosTank.Y - Position.Y)) / (PosTank.X - Position.X)) + Position.Y;
                            if (rect_obj.Contains(j, (float)y))
                            {
                                //  allObject[i]->alive = false;
                                IsHide = true;
                                goto GoToPoint;
                            }
                        }
                    }
                }
            GoToPoint:
                if (!IsHide)
                {
                    float x = PosTank.X - Position.X;
                    float y = Position.Y - PosTank.Y;
                    float temp = (PosTank.X - Position.X) / r;
                    Angle = (float)(Math.Asin(x / r) * 180 / Math.PI);
                    if (x > 0 && y < 0)
                    {
                        Angle = 180 - Angle;
                    }
                    else if (x < 0 && y < 0)
                    {
                        Angle = 180 - Angle;
                    }
                    if (Angle < 0)
                    {
                        Angle = 360 + Angle;
                    }
                    this.
                    CannonSprite.Rotation = (float)Angle;
                    Fire(AllObjects);
                }
            }
            return;
        }
        private void Fire(in List<game.GameObject.Box> AllObjects)
        {
            if ((GlobalTimer.ClockGL.ElapsedTime.AsSeconds() - TimeSinceLastShot) < 0.5) return;
            TimeSinceLastShot = GlobalTimer.ClockGL.ElapsedTime.AsSeconds();
            Bullet temp_bullet = new Bullet(Position.X + 50 * (float)Math.Sin(Angle * MathConst.M_PI / 180.0),
                Position.Y - 50 * (float)Math.Cos(Angle * MathConst.M_PI / 180.0), Angle, GameObjectType.EnemyBullet);
            temp_bullet.Dead += GamePt.DeleteDeadObject;
            AllObjects.Add(temp_bullet);
        }

        protected override bool CheckKollision(GameObjectType Type)
        {
            if (Type == GameObjectType.PlayerBullet) GetDamage();
            return true;
        }
    }

}
