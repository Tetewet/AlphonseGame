using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Inventaire<T, K>
    {
        T[] enumObjet;
        K[] nombreObjet;
        int w, x, y, z;
        
        public Inventaire()
        {
            w = 0; //nombre actuel d'objet T - items différents
            x = 10; //nombre maximal d'objet T - items différents
            y = 0; //nombre actuel d'objet K - stack d'item
            z = 5; //nombre maximal d'objet K - stack d'item
        }

        //check si l'inventaire est complet
        private bool IsInventoryFull()
        {
            if (w == x)
            {
                return true;
            }
            return false;
        }

        //check si le stack d'items est complet
        private bool IsItemFullStack()
        {
            if (y == z)
            {
                return true;
            }
            return false;
        }

        public void Add(T pObjet1, K pObjet2)
        {
            if (!IsInventoryFull() && !IsItemFullStack())
            {
                enumObjet[w++] = pObjet1;
                nombreObjet[y++] = pObjet2;
            }
        }
    }
}
