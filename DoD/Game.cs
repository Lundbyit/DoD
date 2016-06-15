﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Game
    {
        //Fields
        Player player;
        Monster monster;
        Room[,] world;
        const int WorldWidth = 20;
        const int WorldHeight = 5;
        bool fightOn;
        Random rnd;

        //Contructors
        public Game()
        {
            player = new Player(100, 'P');
            rnd = new Random();
            CreateWorld();
        }
        //Public Methods
        public void Play()
        {
            do
            {
                Console.Clear();

                DrawWorld();
                PrintInventory();
                HandleMovement();

            } while (player.IsAlive);

            Console.WriteLine("Game Over");
        }

        //Private Methods
        private void PrintInventory()
        {
            int swords = 0;
            double weightSword = 0;
            double weightPotion = 0;
            int potions = 0;

            Console.WriteLine("Inventory:");
            foreach (Item items in player.Inventory)
            {
                if (items.Name == "Sword")
                {
                    swords++;
                    weightSword += items.Weight;
                }
                else if (items.Name == "Potion")
                {
                    potions++;
                    weightPotion += items.Weight;
                }
            }
            if (swords > 0)
            {
                Console.WriteLine(swords + "x Swords" + " Weight:" + weightSword);
            }
            if (potions > 0)
            {
                Console.WriteLine(potions + "x Potions" + " Weight:" + weightPotion);
            }
            Console.WriteLine($"Backpack size: {player.InventorySize} oz");
        }
        private void FightArena()
        {
            Console.WriteLine($"You encountered a {monster.Name}");
            while (player.IsAlive && monster.IsAlive)
            {
                if (player.Agility >= monster.Agility)
                {
                    player.Attack(monster, player.AttackDamage);
                    Console.WriteLine($"You did {player.AttackDamage}damage on {monster.Name}");
                    if (monster.IsAlive)
                    {
                        monster.Attack(player, monster.AttackDamage);
                        Console.WriteLine($"{monster.Name} did {monster.AttackDamage}damage on you");
                    }
                }
                else
                {
                    monster.Attack(player, monster.AttackDamage);
                    if (!monster.IsAlive)
                    {
                        return;
                    }
                    Console.WriteLine($"{monster.Name} did {monster.AttackDamage}damage on you");
                    if (player.IsAlive)
                    {
                        player.Attack(monster, player.AttackDamage);
                        Console.WriteLine($"You did {player.AttackDamage}damage on {monster.Name}");
                    }
                }

            }
            if (player.IsAlive)
            {
                Console.WriteLine($"You killed {monster.Name}");
            }
            else
            {
                Console.WriteLine("WASTED. GAME OVER");
            }
        }

        private void CreateWorld()
        {
            #region
            //List<Item> ItemList = new List<Item>();
            //Item Sword = new Item("Sword", 10);
            //Item Shield = new Item("Shield", 5);
            //Item Potion = new Item("Potion", 2.3);
            //Item Armor = new Item("Armor", 15);
            //Item Bow = new Item("Bow", 20);
            #endregion

            world = new Room[WorldWidth, WorldHeight];
            for (int y = 0; y < WorldHeight; y++)
            {
                for (int x = 0; x < WorldWidth; x++)
                {

                    Room room = new Room();
                    int RandomPlacement = rnd.Next(10);
                    world[x, y] = room;

                    if (RandomPlacement == 1 && x != 0 && y != 0) //If the room 
                    {
                        //Random random = new Random();
                        room.Item = WhichItem(rnd);

                    }
                }
            }
                monster = PlaceMonsters();
        }

        private Monster PlaceMonsters()
        {
            int monster = rnd.Next(0, 2);
            Monster C;

            if (monster == 0)
            {
                C = new EvilCucumber(10, "EvilCucumber");
            }
            else
                C = new CheekyTomato(10, "CheekyTomato");

            int monsterX = rnd.Next(0, WorldWidth);
            int monsterY = rnd.Next(0, WorldHeight);
            C.X = monsterX;
            C.Y = monsterY;
            return C;
        }

        private Item WhichItem(Random rnd)
        {
            int WhichItem = rnd.Next(2);
            if (WhichItem == 0)
            {
                Weapon Sword = new Weapon("Sword", 10, 2);
                return Sword;
            }
            else
            {
                Potion Potion = new Potion("Potion");
                return Potion;
            }

        }

        private void DrawWorld()
        {
            for (int y = 0; y < WorldHeight; y++)
            {
                for (int x = 0; x < WorldWidth; x++)
                {
                    Room room = world[x, y];
                    if (player.X == x && player.Y == y)
                    {
                        Console.Write(player.Icon);
                    }
                    else if (monster.X == x && monster.Y == y && monster.IsAlive)
                    {
                        Console.Write(monster.Icon);
                    }
                    else if (room.Item != null)
                    {
                        Console.Write("I");
                        //if (room.Item.Name == "Sword")
                        //{
                        //    Console.Write("S");
                        //}
                        //else if (room.Item.Name == "Potion")
                        //{
                        //    Console.Write("T");
                        //}
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
            if (fightOn)
            {
                FightArena();
            }

        }

        private void HandleMovement()
        {
            Console.WriteLine("Ange riktning...");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            int newX = player.X;
            int newY = player.Y;

            switch (keyInfo.Key)
            {
                case ConsoleKey.RightArrow: newX++; break;
                case ConsoleKey.LeftArrow: newX--; break;
                case ConsoleKey.DownArrow: newY++; break;
                case ConsoleKey.UpArrow: newY--; break;
            }
            if (newX >= 0 && newX < WorldWidth && newY >= 0 && newY < WorldHeight)
            {
                player.X = newX;
                player.Y = newY;
                if (newX == monster.X && newY == monster.Y)
                {
                    fightOn = true;
                }
                else if (world[newX, newY].Item != null)
                {
                    player.Inventory.Add(world[newX, newY].Item);
                    world[newX, newY].Item.UseItem(player);

                    player.InventorySize += world[newX, newY].Item.Weight;
                    world[newX, newY].Item = null;
                }

            }
        }
    }
}