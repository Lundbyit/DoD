using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    class Weapon : Item
    {
        public Weapon(string name, int weight, int attack) : base(name, weight)
        {
            this.Attack = attack;
        }
        public int Attack { get; set; }
        public override void UseItem(Creature user)
        {
            user.AttackDamage += this.Attack;
        }


    }
}
