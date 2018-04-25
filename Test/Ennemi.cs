using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Test 
{
    class Ennemi
    {
        public Texture2D ennemiTexture;
        public Vector2 Position;
        public float ennemiVitesse;
        public bool Active;
        public int Damage = 1;
        public int Health;
        public int Width
        {
            get { return ennemiTexture.Width; }
        }

        public int Height
        {
            get { return ennemiTexture.Height; }
        }

        public void Initialize(Texture2D sprites, Vector2 position)
        {
            ennemiTexture = sprites;
            Position = position;
            Active = true;
            Health = 25;
            ennemiVitesse = 1.75f;
        }
        
        public void Update(GameTime gameTime, Player pPlayer, Ennemi pEnnemi, Texture2D pTexture2, Texture2D pTexture)
        {
            Vector2.Distance(ref pEnnemi.Position, ref pPlayer.Position, out float distance);

            if (distance < 130f)
            {
                pEnnemi.ennemiTexture = pTexture2;
                Vector2 direction = pPlayer.Position - Position;
                direction.Normalize();
                Position += direction * ennemiVitesse;
            }
            else { pEnnemi.ennemiTexture = pTexture; }
            
            if (Health <= 0)
            {
                Active = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ennemiTexture, Position, Color.White);
        }
    }
}
