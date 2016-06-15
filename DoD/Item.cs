using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    abstract class Item : Iluggable
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public abstract void UseItem(Creature user);
        public Item(string name, double weight)
        {
            Name = name;
            Weight = weight;
        }
    }
}
