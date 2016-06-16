using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    class Potion : Item
    {
        public Potion(string name) : base(name, 1)
        {
            
        }
        public override void UseItem(Creature user)
        {
        }

    }
}
