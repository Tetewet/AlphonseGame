using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Test 
{
    class Projectile
    {
        public Texture2D projectileTexture;
        float projectileVitesse = 10f;
        public Rectangle Position;
        public int Damage = 25;
        public bool Active;
        public int Range = 100;
        public int ProjectileWidth
        {
            get { return projectileTexture.Width; }
        }
        public int ProjectileHeight
        {
            get { return projectileTexture.Height; }
        }

        public void Initialize(Texture2D sprites, Rectangle position)
        {
            projectileTexture = sprites;
            Position = position;
            Active = true;
        }

        public void Update(GameTime gameTime)
        {
            Position.X += (int)projectileVitesse;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(projectileTexture, Position, Color.White);
        }

    }
}
