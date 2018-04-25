using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Test 
{
    class Water_Tile : TuileBase
    {
        public new Texture2D textureTuile;
        public new Vector2 Position;

        public Water_Tile(Texture2D textureTuile, Vector2 position) : base(textureTuile, position)
        {
            this.textureTuile = textureTuile;
            this.Position = position;
        }

        public new int LargeurTuile
        {
            get { return textureTuile.Width; }
        }

        public new int HauteurTuile
        {
            get { return textureTuile.Height; }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureTuile, Position, Color.White);
        }
    }
}
