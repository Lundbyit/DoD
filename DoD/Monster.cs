using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    //Monstercount If monstercount == 0; Avsluta spelet
    abstract class Monster : Creature
    {
        public Monster(int health, string name, int agility) : base(health, name[0], agility)
        {
            this.Name = name;
        }
        public string Name { get; private set; }
    }
}
