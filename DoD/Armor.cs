using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Armor : Item
    {
        public Armor(string name, int weight, int defence) : base(name, weight)
        {
            this.Defence = defence;
        }
        public int Defence
        {
            get
            {
                throw new System.NotImplementedException();
            }

            private set
            {
            }
        }
        public override void UseItem(Creature user)
        {
        }
    }
}
