using ConsoleFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    //Kolla CheekyTomato Battle
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
        //Vi behöver lösa cursor problemet på ett bättre sätt, flyttar ut den så att alla kan nå den
        int cursorLeftInventory = 50;
        int cursorTopInventory = 17;
        int cursorLeftStatus = 5;
        int cursorTopStatus = 1;

        //Contructors
        public Game()
        {
            cursorLeftInventory = 50;
            cursorTopInventory = 17;
            cursorLeftStatus = 5;
            cursorTopStatus = 1;
            

            Console.WriteLine();
            Console.WriteLine();
            AsciiArt.PrintCentered("Dump of");
            AsciiArt.PrintCentered("Doom");
            Console.SetCursorPosition(cursorLeftInventory, cursorTopInventory);
            Console.WriteLine("Press start...");
            Console.ReadKey();
            Console.Clear();
            cursorLeftInventory = 45;
            cursorTopInventory = 14;
            Console.SetCursorPosition(cursorLeftInventory, cursorTopInventory);
            Console.WriteLine("Vem vågar sig in i Dump of Doom?");
            Console.SetCursorPosition(cursorLeftInventory, cursorTopInventory + 1);
            string tmp = Console.ReadLine();
            player = new Player(100, 'P', tmp);
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
                PrintPlayerStatus();
                HandleMovement();

            } while (player.IsAlive && Monster.monsterCount > 0);
            Console.Clear();
            if (player.IsAlive)
            {
                Console.WriteLine("Congratulations, you won the game.");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("GAME OVER YOU SUCK");
            }
        }

        //Private Methods
        private void PrintInventory()
        {
            //int potions = 0;
            cursorLeftInventory = 40;
            cursorTopInventory = 1;
            Console.SetCursorPosition(WorldWidth + cursorLeftInventory, cursorTopInventory);
            Console.WriteLine($"Backpack size: {player.InventorySize} oz");
            Console.SetCursorPosition(WorldWidth + cursorLeftInventory, cursorTopInventory + 2);
            Console.WriteLine("Backpack:");

            foreach (Iluggable items in player.Inventory)
            {
                Console.SetCursorPosition(WorldWidth + cursorLeftInventory, cursorTopInventory + 3);
                Console.WriteLine($"{items.Name}  Weight: {items.Weight}");
                cursorTopInventory++;
            }
            
        }

        private void PrintPlayerStatus()
        {
            Console.SetCursorPosition(WorldWidth + cursorLeftStatus, cursorTopStatus);
            Console.WriteLine ($"Status : {player.Name}" );
            Console.SetCursorPosition(WorldWidth + cursorLeftStatus, cursorTopStatus + 1 );
            Console.WriteLine(".............................");
            Console.SetCursorPosition(WorldWidth + cursorLeftStatus, cursorTopStatus + 2);
            Console.WriteLine($"HP = {player.Health}");
            Console.SetCursorPosition(WorldWidth + cursorLeftStatus, cursorTopStatus + 3);
            Console.WriteLine($"Agility = {player.Agility}");
            Console.SetCursorPosition(WorldWidth + cursorLeftStatus, cursorTopStatus + 4);
            Console.WriteLine($"Attack = {player.AttackDamage}");

        }
        private void FightArena(Monster monster)
        {
            PrintInventory();
            cursorLeftInventory = 0;
            cursorTopInventory = 11;
            Console.SetCursorPosition(cursorLeftInventory, cursorTopInventory);
            TextUtils.AnimateLine($"You encountered a... ", 10);
            TextUtils.AnimateLine($"{monster.Name}", 50);
            while (player.IsAlive && monster.IsAlive)
            {

                if (player.Agility >= monster.Agility)
                {
                    Console.WriteLine(player.Attack(monster));
                    if (monster.IsAlive && monster.IsAlive)
                    {
                        Console.WriteLine(monster.Attack(player));
                    }
                }
                else
                {
                    Console.WriteLine(monster.Attack(player));
                    if (player.IsAlive && monster.IsAlive)
                    {
                        Console.WriteLine(player.Attack(monster));
                    }
                }
            }
            if (player.IsAlive)
            {
                //Console.Clear();
                player.Inventory.Add(monsters[monsterIndex]);
                TextUtils.AnimateLine($"{monster.Name} is dead");
                monsters.RemoveAt(monsterIndex);
                Console.ReadKey();
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

                    if (RndUtils.ReturnValue(0, 10) == 1 && x != 0 && y != 0) //If the room 
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
            {
                C = new CheekyTomato(10);
            }
            C.X = RndUtils.ReturnValue(0, WorldWidth); ;
            C.Y = RndUtils.ReturnValue(0, WorldHeight); ;
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
            if (!fightOn)
            {
                cursorLeftInventory = 0;
                cursorTopInventory = 10;
                Console.SetCursorPosition(cursorLeftInventory, cursorTopInventory);
                Console.WriteLine("Ange riktning...");
            }
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
