using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Test
{
    abstract class TuileBase
    {
        public Texture2D textureTuile; // texture de la tuile
        public Vector2 Position; //position de la tuile
        public int LargeurTuile // taille de la tuile
        {
            get { return textureTuile.Width; }
        }
        public int HauteurTuile
        {
            get { return textureTuile.Height; }
        }

        public TuileBase(Texture2D pTextureTuile, Vector2 pPosition)
        {
            textureTuile = pTextureTuile;
            Position = pPosition;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureTuile, Position, Color.White);
        }
    }
}
