using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    abstract class Item : Iluggable
    {
        public string Name { get; private set; }
        public double Weight { get; private set; }
        public abstract void UseItem(Creature user);
        public Item(string name, double weight)
        {
            Name = name;
            Weight = weight;
        }
        //public void Discard()
        //{
        //Man ska kunna kasta bort grejer
        //}

        //public void Equip()
        //{
        //Man ska kunna equipa, sätter
        //}
    }
}
