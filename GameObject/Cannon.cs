using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game.GameElements;

namespace game.GameObject
{
    class Cannon : Box
    {
        private uint HP = 3;
        private float angle;
        private float TimePastShot;
        public Cannon(float x, float y, GameObjectType ObjectType = GameObjectType.EnemyCannon, string imagePath = "D:\\projects\\c# tank\\game\\Resources\\ObjectTexture\\cannon.png") : base(x, y, ObjectType, imagePath) { }
    }
}
