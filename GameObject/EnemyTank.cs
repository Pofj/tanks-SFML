using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game.GameElements;

namespace game.GameObject
{
    class EnemyTank : Tank
    {
        public EnemyTank(float x, float y, uint angle, GameObjectType ObjectType = GameObjectType.EnemyTank, string imagePath = "D:\\projects\\c# tank\\game\\Resources\\ObjectTexture\\enemy_tank.png", string ImagePathCannon = "D:\\projects\\c# tank\\game\\Resources\\ObjectTexture\\enemy_tank_cannon.png") : base(x, y, angle, ObjectType, imagePath, ImagePathCannon) { }
    }
}
