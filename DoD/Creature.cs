using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    abstract class Creature : GameObject
    {
        public Creature(int health, char icon, int agility, string name) : base(icon)
        {
            this.Name = name;
            this.Health = health;
            this.Agility = agility;
        }
        public string Name { get; private set; }
        public int Health { get; set; }
        public bool IsAlive { get { return Health > 0; } }
        public int X { get; set; }
        public int Y { get; set; }
        public int Agility { get; set; }

        public List<Iluggable> Inventory { get; private set; } = new List<Iluggable>();

        public double InventorySize { get;  set; }
        public virtual string Attack(Creature enemy)
        {
            enemy.Health -= this.AttackDamage;
            return $"{this.Name} did {this.AttackDamage} damage on {enemy.Name}";
        }
        public int AttackDamage { get; set; }

        public int Defence
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        } //Tvek
    }
}
