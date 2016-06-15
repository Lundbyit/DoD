﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    abstract class Item
    {
        public string Name { get; private set; }
        public double Weight { get; private set; }
        public abstract void UseItem(Creature user);
        public Item(string name, double weight)
        {
            Name = name;
            Weight = weight;
        }
    }
}