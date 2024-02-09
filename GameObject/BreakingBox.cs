using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game.GameElements;

namespace game.GameObject
{
    class BreakingBox : Box
    {
        private uint HP = 3;
        public BreakingBox(float x, float y, GameObjectType ObjectType = GameObjectType.BreakingBox, string imagePath = "D:\\projects\\c# tank\\game\\Resources\\ObjectTexture\\breaking_box.png") : base(x, y, ObjectType, imagePath) { }
    }
}
