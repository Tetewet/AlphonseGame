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
        public delegate void PickUpHandler(bool b);
        public event PickUpHandler PickUpObject = delegate { };
        bool PickUp = true;
        private Types objetType;
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
            if (playerRectangle.Intersects(ObjetRectangle))
            {
                PickUpObject(PickUp);
            }

            // ajouter un objet
            //public void AjoutEnnemi()
            //{
            //    int r = random.Next(0, spawnable.Count);
            //    var ennemiPosition = new Vector2(spawnable[r].Position.X, spawnable[r].Position.Y);
            //    Ennemi ennemi = new Ennemi();
            //    ennemi.Initialize(ennemiTexture, ennemiPosition);
            //    ennemis.Add(ennemi);
            //}
            // enlever l'objet si ramassé
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ObjetTexture, PositionObjet, Color.White);
        }
    }
}
