using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class CheekyTomato : Monster
    {
        public CheekyTomato(int health, string name) : base(health, name, 10)
        {
            this.AttackDamage = 3; //Hårdkodad
        }
        public override void Attack(Creature enemy, int attackdamage)
        {
            if ((this.AttackDamage * 2) < enemy.AttackDamage)
            {
                Console.WriteLine($"{this.Name} ran away");
                this.Health = 0;
            }
            else
            {
                base.Attack(enemy, attackdamage);
            }
        }
    }
}
