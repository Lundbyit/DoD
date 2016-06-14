using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    abstract class Creature : GameObject
    {
        public Creature(int health, char icon, int agility) : base(icon)
        {
            this.Health = health;
            this.Agility = agility;
        }
        public int Health { get; set; }
        public bool IsAlive { get { return Health > 0; } }
        public int X { get; set; }
        public int Y { get; set; }
        public int Agility { get; set; }

        public List<Item> Inventory { get; private set; } = new List<Item>();

        public double InventorySize { get;  set; }

        //public void Fight(Monster enemy)
        //{
        //    enemy.Health = 0;
        //    Console.WriteLine($"You killed {enemy.Name}");
        //}
        public virtual void Attack(Creature enemy, int attackdamage)
        {
            enemy.Health -= attackdamage;
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
