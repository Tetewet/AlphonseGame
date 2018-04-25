using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Map //use rectangles for collisions !!! 
    {
        private int largeurTuile;//taille de la tuile
        private int hauteurTuile;//taille de la tuile
        private int largeurMap;//taille de la map
        private int hauteurMap;//taille de la map

        public Map(Texture pTileTexture, int pPosition, bool pIsWalkable, int pLargeurTuile, int pHauteurTuile, int pLargeurMap, int pHauteurMap)
        {
            largeurTuile = pLargeurTuile;
            hauteurTuile = pHauteurTuile;
            largeurMap = pLargeurMap;
            hauteurMap = pHauteurMap;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 positionTuile = Vector2.Zero;//position de la premiere tuile
            
            for (int x = 0; x < largeurMap; x++) 
            {
                for (int y = 0; y < hauteurMap; y++)
                {
                    spriteBatch.FillRectangle(positionTuile, new Size2(largeurTuile, hauteurTuile), Color.Black);
                    positionTuile.Y += hauteurTuile;
                }
                positionTuile.Y = 0;
                positionTuile.X += largeurTuile;
            }
            
        }
    }
}
