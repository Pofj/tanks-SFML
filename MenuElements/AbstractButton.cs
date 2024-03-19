using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using game.GameElements;
using game.Global;
using SFML.Graphics;

namespace game.MenuElements
{
    abstract class Button
    {
        protected Texture TextureNormal;
        protected Sprite SpriteNormal;
        protected Texture TextureHover;
        protected Sprite SpriteHover;
        protected Texture TextureUnavailable;
        protected Sprite SpriteUnavailable;
        protected bool Active;
        public Button(float x, float y, float SizeObjectX, float SizeObjectY, string ImagePathNormal, string ImagePathHover, string ImagePathUnavailable)
        {
            TextureNormal = new Texture(ImagePathNormal);
            SpriteNormal = new Sprite(TextureNormal);
            TextureHover = new Texture(ImagePathHover);
            SpriteHover = new Sprite(TextureHover);
            TextureUnavailable = new Texture(ImagePathUnavailable);
            SpriteUnavailable = new Sprite(TextureUnavailable);
            x = x * InfoAboutResolution.RatioWidthResolution;
            y = y * InfoAboutResolution.RatioWidthResolution;
            SFML.System.Vector2f temp = new SFML.System.Vector2f(x, y);
            SpriteNormal.Position = temp;
            SpriteHover.Position = temp;
            SpriteUnavailable.Position = temp;

            float ScaleX = SizeObjectX * InfoAboutResolution.RatioWidthResolution;
            float ScaleY = SizeObjectY * InfoAboutResolution.RatioWidthResolution;
            temp = new SFML.System.Vector2f(ScaleX, ScaleY);
            SpriteNormal.Scale = temp;
            SpriteHover.Scale = temp;
            SpriteUnavailable.Scale = temp;
        }

        public void Show(in RenderWindow window)
        {
            if (Active)
            {
                window.Draw(SpriteUnavailable);
                return;
            } 
            else if (UnderMouse())
            {

                window.Draw(SpriteHover);
                return;
            }
            else
            {
                window.Draw(SpriteNormal);
                return;
            }
        }
        private bool UnderMouse()
        {
;            return SpriteNormal.GetGlobalBounds().Contains((float)InfoAboutMouse.CursorPositionX, (float)InfoAboutMouse.CursorPositionY); 
        }
        public abstract void Click(object sender, EventArgs e);

    }
}
