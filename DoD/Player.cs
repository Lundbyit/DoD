using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Player : Creature
    {
        public Player(int health, char icon) : base(health, icon, 8) //Agility är hårdkodad nu, ändra senare
        {
            this.AttackDamage = 5; //Hårdkodad nu
        }

    }
}
