using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
 
namespace Test
{
    class Player
    {
        public Texture2D PlayerTexture; //texture du joueur
        public Rectangle Position; //position du joueur
        public bool Active; //etat du joueur
        public int Health;
        public int Width
        {
            get { return PlayerTexture.Width; }
        }
        public int Height
        {
            get { return PlayerTexture.Height; }
        }

        public void Initialize(Texture2D texture, Rectangle position)
        {
            PlayerTexture = texture;
            Position = position;
            Active = true;
            Health = 5;
        }

        public void Update()
        {
            
        }

        

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlayerTexture, Position, Color.White);
        }
    }
}
