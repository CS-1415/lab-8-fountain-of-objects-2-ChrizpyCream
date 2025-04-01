using System;
using System.Collections.Generic;
using Lab08;

class FountainMazeGame
{
    static FountainRoom fountainRoom;
    static bool fountainOn = false;
    static Player player;
    static Room[,] Maze;
    static Monster[,] monsters;

    static void Main()
    {
        string sizeInput = GetValidSizeInput();
        Maze = PopulateMaze(sizeInput);
        monsters = PopulateMonsters(sizeInput);
        Inventory playerInventory = new Inventory(new Fist(), new Potion());
        player = new Player(0, 0, playerInventory);

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Your goal is to find the fountain, activate it, and escape.");

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"You are in the room at (Column={player.x}, Row={player.y}, Health={player.health}).");
            Maze[player.x, player.y].Information(fountainOn);
            CheckForMonsters();

            Console.WriteLine("What do you want to do? (move north, move east, move south, move west, enable fountain, check inventory, drink potion)");
            Console.ForegroundColor = ConsoleColor.Cyan;
            string action = Console.ReadLine();

            switch (action)
            {
                case "move north": if (player.y > 0) player.MoveNorth(); break;
                case "move south": if (player.y < Maze.GetLength(1) - 1) player.MoveSouth(); break;
                case "move west": if (player.x > 0) player.MoveWest(); break;
                case "move east": if (player.x < Maze.GetLength(0) - 1) player.MoveEast(); break;
                case "enable fountain": EnableFountain(); break;
                case "check inventory": player.inventory.CheckInventory(player.inventory, player); break;
                case "drink potion": DrinkPotion(); break;
                default: Console.WriteLine("Invalid input. Try again."); break;
            }
            
            if (monsters[player.x, player.y] != null)
            {
                monsters[player.x, player.y].AttackDialog(player, Maze, monsters);
            }
        }
    }

    static string GetValidSizeInput()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Would you like a small, medium, or large map?");
        string input = Console.ReadLine();
        return (input == "small" || input == "medium" || input == "large") ? input : GetValidSizeInput();
    }

  static Room[,] PopulateMaze(string size)
{
    int sizeValue = size == "small" ? 4 : size == "medium" ? 6 : 8;
    Room[,] maze = new Room[sizeValue, sizeValue];

    // Fix: Use different loop variable names to avoid conflicts
    for (int row = 0; row < sizeValue; row++)
        for (int col = 0; col < sizeValue; col++)
            maze[col, row] = new Room(col, row);

    maze[0, 0] = new Entrance(0, 0);

    Random random = new Random();
    int fountainX, fountainY;
    do
    {
        fountainX = random.Next(sizeValue);
        fountainY = random.Next(sizeValue);
    } while (fountainX == 0 && fountainY == 0);

    maze[fountainX, fountainY] = new FountainRoom(fountainX, fountainY);
    fountainRoom = (FountainRoom)maze[fountainX, fountainY];

    return maze;
}


    static Monster[,] PopulateMonsters(string size)
    {
        int sizeValue = size == "small" ? 4 : size == "medium" ? 6 : 8;
        Monster[,] monsters = new Monster[sizeValue, sizeValue];
        Random random = new Random();

        Inventory[] inventories = {
            new Inventory(new Sword(), new Potion()),
            new Inventory(new Fist(), new Potion()),
            new Inventory(new Sword(), new Potion(), new Potion())
        };

        for (int y = 0; y < sizeValue; y++)
        {
            for (int x = 0; x < sizeValue; x++)
            {
                if (random.Next(6) == 0)
                {
                    int monsterType = random.Next(3);
                    monsters[x, y] = monsterType switch
                    {
                        0 => new Gooner(x, y, inventories[0]),
                        1 => new Maelstrom(x, y, inventories[1]),
                        _ => new Amarok(x, y, inventories[2])
                    };
                }
            }
        }
        return monsters;
    }

    static void CheckForMonsters()
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        for (int x = player.x - 1; x <= player.x + 1; x++)
            for (int y = player.y - 1; y <= player.y + 1; y++)
                if (x >= 0 && y >= 0 && x < Maze.GetLength(0) && y < Maze.GetLength(1) && monsters[x, y] != null)
                    monsters[x, y].Information();
    }

    static void EnableFountain()
    {
        if (player.x == fountainRoom.x && player.y == fountainRoom.y)
        {
            fountainOn = true;
            Maze[fountainRoom.x, fountainRoom.y].fountainOn = true;
            Console.WriteLine("You have activated the fountain!");
        }
        else
        {
            Console.WriteLine("You are not in the fountain room.");
        }
    }

    static void DrinkPotion()
    {
        if (player.inventory.CountPotions(player.inventory) > 0)
        {
            player.health += 5;
            for (int i = 0; i < player.inventory.items.Count(); i++)
            {
                if (player.inventory.items[i].name == "potion")
                {
                    player.inventory.items.RemoveAt(i);
                    break;
                }
            }
            Console.WriteLine($"You have gained 5 health. You are now at {player.health} health.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You are out of potions.");
        }
    }
}
