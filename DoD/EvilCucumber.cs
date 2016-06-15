using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class EvilCucumber : Monster
    {
        public EvilCucumber(int health): base(health, "Evilcucumber", 5)
        {
            this.AttackDamage = 5; //Hårdkodad
        }
        
    }
}
