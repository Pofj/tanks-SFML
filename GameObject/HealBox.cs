﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game.GameElements;

namespace game.GameObject
{
    class HealBox : Box
    {
        public HealBox(float x, float y, GameObjectType ObjectType = GameObjectType.HealBox,float SizeObject = 0.8f, string imagePath = "D:\\projects\\c# tank\\game\\Resources\\ObjectTexture\\heal_box.png") : base(x, y, ObjectType,SizeObject, imagePath) { }

        protected override bool CheckKollision(GameObjectType Type)
        {
            if (Type == GameObjectType.Player)
            {
                Kill();
            }
            return true;
        }

    }
}
