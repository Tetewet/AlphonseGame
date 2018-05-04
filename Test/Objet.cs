using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{ 
    class Objet
    {
        public int maxObjet = 5;
        //static int objetCount = 0;
        public Texture2D ObjetTexture;
        public Rectangle ObjetRectangle;
        public Vector2 PositionObjet;
        public bool Active;
        public delegate void PickUpHandler(Types t);
        public event PickUpHandler PickUpObject = delegate { };
        //bool PickUp = true;
        public Types objetType;
        //private Rectangle rectangle;

        public Objet(Texture2D pObjetTexture, Types pEnum, Vector2 pPosition, Rectangle pRectangle)
        {
            ObjetTexture = pObjetTexture;
            objetType = pEnum;
            ObjetRectangle = pRectangle;
            PositionObjet = pPosition;
            Active = true;
        }

        public enum Types
        {
            Croissant, Fromage, Lait, Soupe, Graines 
        }

        

        //public bool IsAddObject(Random random)
        //{
        //    if (random.Next(0, 1) == 0) { return false; }
        //    return true;
        //}

        //public static Objet Spawn(Types pTypes)
        //{
        //    if (objetCount < maxObjet)
        //    {
        //        return new Objet(pTypes);
        //    }
        //    else return null;
        //}

        public void Update(GameTime gameTime, Rectangle playerRectangle)
        {
            // l'objet est-il ramassé ?
            if (Active == true)
            {
                if (playerRectangle.Intersects(ObjetRectangle))
                {
                    PickUpObject(objetType);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active == true)
            {
                spriteBatch.Draw(ObjetTexture, PositionObjet, Color.White);
            }
        }
    }
}
