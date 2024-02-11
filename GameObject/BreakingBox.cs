using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game.GameElements;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace game.GameObject
{
    class BreakingBox : Box
    {
        private uint HP = 3;
        public BreakingBox(float x, float y, GameObjectType ObjectType = GameObjectType.BreakingBox, string imagePath = "D:\\projects\\c# tank\\game\\Resources\\ObjectTexture\\3_hp_breaking_box.png") : base(x, y, ObjectType, imagePath) { }

        private void GetDamage()
        {
            HP -= 1;
            if (HP == 2)
            {
                ChangeTexture("D:\\projects\\c# tank\\game\\Resources\\ObjectTexture\\2_hp_breaking_box.png");
            }
            else if (HP == 1)
            {
                ChangeTexture("D:\\projects\\c# tank\\game\\Resources\\ObjectTexture\\1_hp_breaking_box.png");
            }
            else
            {
                alive = false;
                Kill();
            }
        }
        protected override bool CheckKollision(GameObjectType Type)
        {
            if (Type == GameObjectType.PlayerBullet) GetDamage();
            if (Type == GameObjectType.EnemyBullet) GetDamage();
            return true;
        }
        void ChangeTexture(string imagePath)
        {
                ObjectTexture = new SFML.Graphics.Texture(imagePath);
                ObjectSprite = new SFML.Graphics.Sprite(ObjectTexture);
                this.ObjectSprite.Origin = new Vector2f(ObjectSprite.GetLocalBounds().Width / 2, ObjectSprite.GetLocalBounds().Height / 2);
                ObjectSprite.Position = this.Position;
                ObjectSprite.Scale = new SFML.System.Vector2f(SizeObject, SizeObject);
            
        }
    }
}

