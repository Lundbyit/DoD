using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    class Player : Creature
    {
        public Player(int health, char icon, string name) : base(health, icon, 8, name) //Agility är hårdkodad nu, ändra senare
        {
            this.AttackDamage = 5; //Hårdkodad nu
        }
        //Gör det lättare att se till att de inte har två vapen osv.
        public bool HaveWeapon { get; set; }
        public bool HaveShield { get; set; }
        public bool HaveArmor { get; set; }



    }
}
