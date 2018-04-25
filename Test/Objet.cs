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
        static int maxObjet = 10;
        static int objetCount = 0;
        public Texture2D ObjetTexture { get; set; }
        public Rectangle ObjetRectangle { get; set; }
        public delegate void PickUpHandler(bool b);
        public event PickUpHandler PickUpObject = delegate { };
        bool PickUp = true;
        private Types croissant;
        private Rectangle rectangle;

        public Objet(Types croissant, Rectangle rectangle)
        {
            this.croissant = croissant;
            this.rectangle = rectangle;
            Initialize(croissant, rectangle);
        }

        public enum Types
        {
            Croissant, Fromage, Lait, Soupe, Graines 
        }

        public void Initialize(Types types, Rectangle pObjetRectangle)
        {
            if (objetCount < maxObjet)
            {
                objetCount++;
            }
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
            //if (ennemis.Count < 6)
            //{
            //    //prevEnnemiSpawn = gameTime.TotalGameTime;
            //    AjoutEnnemi();
            //}

            //for (int i = 0; i < ennemis.Count; i++)
            //{
            //    ennemis[i].Update(gameTime, player, ennemis[i], ennemiTextureTemp, ennemiTexture);
            //    if (ennemis[i].Active == false)
            //    {
            //        ennemis.RemoveAt(i);
            //    }
            //}
            // l'objet est-il ramassé ?
            if (ObjetRectangle.Intersects(playerRectangle))
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
            spriteBatch.Draw(ObjetTexture, ObjetRectangle, Color.White);
        }
    }
}
