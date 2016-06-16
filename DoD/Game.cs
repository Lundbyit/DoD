using ConsoleFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    //randomUtils Returnar true, return number
    //asdf
    class Game
    {
        //Fields
        Player player;
        List<Monster> monsters = new List<Monster>();
        Room[,] world;
        const int WorldWidth = 30;
        const int WorldHeight = 10;
        bool fightOn;
        int monsterIndex;

        //Contructors
        public Game()
        {
            player = new Player(100, 'P');
            CreateWorld();
        }
        //Public Methods
        public void Play()
        {
            AsciiArt.PrintCentered("HAHAHA");
            Console.ReadKey();

            do
            {
                Console.Clear();

                DrawWorld();
                PrintInventory();
                HandleMovement();

            } while (player.IsAlive && Monster.monsterCount > 0);
            Console.Clear();
            if (player.IsAlive)
            {
                Console.WriteLine("Congratulations, you won the game.");
            }
            else
            Console.WriteLine("GAME OVER YOU SUCK");
        }

        //Private Methods
        private void PrintInventory()
        {
            const int cursorLeft = 20;
            int cursorTop = 1;
            //int potions = 0;
            Console.SetCursorPosition(WorldWidth + cursorLeft, cursorTop);
            Console.WriteLine($"Backpack size: {player.InventorySize} oz");
            Console.SetCursorPosition(WorldWidth + cursorLeft, cursorTop + 2);
            Console.WriteLine("Backpack:");

            foreach (Iluggable items in player.Inventory)
            {
                Console.SetCursorPosition(WorldWidth + cursorLeft, cursorTop + 3);
                Console.WriteLine($"{items.Name}  Weight: {items.Weight}");
                cursorTop++;
            }

            
        }
        private void FightArena(Monster monster)
        {
            TextUtils.AnimateLine($"You encountered a {monster.Name}", 10);
            while (player.IsAlive && monster.IsAlive)
            {
                
                if (player.Agility >= monster.Agility)
                {
                    //Vi kan göra en metod 
                    player.Attack(monster);
                    TextUtils.AnimateLine($"You did {player.AttackDamage} damage on {monster.Name}");
                    if (monster.IsAlive)
                    {
                        monster.Attack(player);
                        TextUtils.AnimateLine($"{monster.Name} did {monster.AttackDamage} damage on you");
                    }
                }
                else
                {
                    monster.Attack(player);
                    //Köra en if monster isAlive(?)
                    if (!monster.IsAlive)
                    {
                        monsters.RemoveAt(monsterIndex);
                        return;
                    }
                    TextUtils.AnimateLine($"{monster.Name} did {monster.AttackDamage} damage on you");
                    if (player.IsAlive)
                    {
                        player.Attack(monster);
                        TextUtils.AnimateLine($"You did {player.AttackDamage} damage on {monster.Name}");
                    }
                }

            }
            if (player.IsAlive)
            {
                player.Inventory.Add(monsters[monsterIndex]);
                TextUtils.AnimateLine($"You killed {monster.Name}");
                monsters.RemoveAt(monsterIndex);
                fightOn = false;
                Monster.monsterCount--;
            }
            else
            {
                TextUtils.AnimateLine("You died.");
                fightOn = false;
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
                    world[x, y] = room;

                    if (RndUtils.ReturnValue (0, 10) == 1 && x != 0 && y != 0) //If the room 
                    {
                        room.Item = WhichItem();

                    }
                }
            }
            for (int i = 0; i < 4; i++)
            {
                Monster monster;
                monster = PlaceMonsters();
                monsters.Add(monster);
            }
        }

        private Monster PlaceMonsters()
        {
            Monster C;

            if (RndUtils.Try(50))
            {
                C = new EvilCucumber(10);
            }
            else
                C = new CheekyTomato(10);

            int monsterX = RndUtils.ReturnValue(0, WorldWidth);
            int monsterY = RndUtils.ReturnValue(0, WorldHeight);
            C.X = monsterX;
            C.Y = monsterY;
            return C;
        }

        private Item WhichItem()
        {
            
            if (RndUtils.Try(50))
            {
                Weapon Sword = new Weapon("Sword", RndUtils.ReturnValue(5,15), 2);
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
                    bool monsterExists = false;
                    int monsterI = -1;
                    for (int i = 0; i < monsters.Count(); i++)
                    {
                        if (monsters[i].X == x && monsters[i].Y == y)
                        {
                            monsterExists = true;
                            monsterI = i;
                        }
                    }
                    if (player.X == x && player.Y == y)
                    {
                        Console.Write(player.Icon);
                    }
                    else if (monsterExists)
                    {
                        Console.Write(monsters[monsterI].Name[0]);
                        monsterExists = false;
                    }
                    else if (room.Item != null)
                    {
                        Console.Write("I");
                    }
                    else
                    {
                        Console.Write(".");
                    }

               
                        //Console.SetCursorPosition(monster.X, monster.Y);
                        //Console.Write(monster.Icon);
                    

                }
                Console.WriteLine();
            }
            if (fightOn)
            {
                FightArena(monsters[monsterIndex]);
            }

        }

        private void HandleMovement()
        {
            Console.WriteLine();
            Console.WriteLine();
            //TextUtils.AnimateLine("Ange riktning...");
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

                if (world[newX, newY].Item != null)
                {
                    player.Inventory.Add(world[newX, newY].Item);
                    world[newX, newY].Item.UseItem(player);

                    player.InventorySize += world[newX, newY].Item.Weight;
                    world[newX, newY].Item = null;
                }

                for (int i = 0; i < monsters.Count(); i++)
                {
                    if (newX == monsters[i].X && newY == monsters[i].Y)
                    {
                        fightOn = true;
                        monsterIndex = i;
                    }
                }
            }
        }
    }
}
