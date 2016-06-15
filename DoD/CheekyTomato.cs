﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class CheekyTomato : Monster
    {
        public CheekyTomato(int health) : base(health, "CheekyTomato", 10)
        {
            this.AttackDamage = 3; //Hårdkodad
        }
        public override string Attack(Creature enemy)
        {
            if ((this.AttackDamage * 2) < enemy.AttackDamage)
            {
                return$"{this.Name} ran away";
                this.Health = 0;
            }
            else
            {
                return base.Attack(enemy);
            }
        }
    }
}
