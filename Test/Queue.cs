using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Test
{
    class Queue<T>
    {
        //il faut implementer une enqueue
        //elle doit prendre un t en parametre 
        // elle doit doubler un tableau et le remettre dans un autre tableau
        
        T[] data;

        public void Enqueue (T t)
        {
            
        }

        public void Print()
        {
            foreach (T t in data)
            {
                Console.WriteLine(t.ToString());
            }
        }


    }
}
