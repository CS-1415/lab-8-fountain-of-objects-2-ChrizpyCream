﻿namespace Lab08;

public class Monster
{
    public int x;
    public int y;
    public int health;
    public Inventory inventory;
    public Item equippedItem;
    public Battle battle = new Battle();
    public int armorClass;
    public Monster(int x, int y, Inventory inventory)
    {
        this.x = x;
        this.y = y;
        this.inventory = inventory;
    }
    public virtual void Information() { }
    public virtual void AttackDialog(Player player, Room[,] Maze, Monster[,] monsters) { }
}

public class Maelstrom : Monster
{
    public Maelstrom(int x, int y, Inventory inventory) : base(x, y, inventory)
    {
        this.x = x;
        this.y = y;
        this.inventory = inventory;
        equippedItem = new Fist();
        health = 12;
        armorClass = 5;
    }
    public override void Information()
    {
        Console.WriteLine("You feel the wind of a maelstrom nearby.");
    }
    public override void AttackDialog(Player player, Room[,] Maze, Monster[,] monsters)
    {
        Random moveChance = new Random();
        if (moveChance.Next(3) != 0)
        {
            if (player.y > 0 && player.y <= Maze.GetLength(1) - 1)
                player.MoveNorth();
            if (player.x >= 0 && player.x < Maze.GetLength(0) - 1)
                player.MoveEast();
            if (player.x >= 0 && player.x < Maze.GetLength(0) - 1)
                player.MoveEast();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("You have been moved by a maelstrom to column " + player.x + " and row " + player.y);

            if (y >= 0 && y <= Maze.GetLength(1) - 1)
                y++;
            if (x >= 1 && x <= Maze.GetLength(0))
                x--;
            if (x >= 1 && x <= Maze.GetLength(0))
                x--;
            if (monsters[player.x, player.y] != null)
                monsters[player.x, player.y].AttackDialog(player, Maze, monsters);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You have encountered a Maelstrom. They rush at you in a gust of wind and attack.");
            battle.TotalBattle(player, monsters);
        }
    }
}

public class Amarok : Monster
{
    public Amarok(int x, int y, Inventory inventory) : base(x, y, inventory)
    {
        this.x = x;
        this.y = y;
        this.inventory = inventory;
        equippedItem = new Sword();
        health = 24;
        armorClass = 13;
    }
    public override void Information()
    {
        Console.WriteLine("You smell the disease of an amarock nearby.");
    }
    public override void AttackDialog(Player player, Room[,] Maze, Monster[,] monsters)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("You have encountered an Amarok. They leap at you and attack.");
        battle.TotalBattle(player, monsters);
    }
}

public class Gooner : Monster
{
    public Gooner(int x, int y, Inventory inventory) : base(x, y, inventory)
    {
        this.x = x;
        this.y = y;
        this.inventory = inventory;
        equippedItem = new Sword();
        health = 20;
        armorClass = 10;
    }
    public override void Information()
    {
        Console.WriteLine("You hear the plapping of a gooner nearby.");
    }
    public override void AttackDialog(Player player, Room[,] Maze, Monster[,] monsters)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("A Gooner creeps from the shadows and moves to attack.");
        battle.TotalBattle(player, monsters);
    }
}