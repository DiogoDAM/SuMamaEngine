using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SuMamaLib.Utils.Sprites
{
    public class Sprite
    {
        public Texture2D Texture { get; private set; }
        public Point StartPos;
        public Point Size;

        public Sprite(Texture2D texture, Rectangle rect)
        {
            SetTexture(texture);
            StartPos = new Point(rect.X, rect.Y);
            Size = new Point(rect.Width, rect.Height);
        }

        public Sprite(Texture2D texture, Point p, Point s)
        {
            SetTexture(texture);
            StartPos = p;
            Size = s;
        }

        public Sprite(Texture2D texture, Point p, int w, int h)
        {
            SetTexture(texture);
            StartPos = p;
            Size = new Point(w, h);
        }

        public void SetTexture(Texture2D texture)
        {
            if(texture == null) { throw new NullReferenceException("Texture is null");}
            Texture = texture;
        }

		public Rectangle GetBounds()
		{
			return new Rectangle(StartPos, Size);
		}
    }

}
