namespace Lab08;

public class Maze
{
    public int length;
    public int width;
    public Maze(int length, int width)
    {
        this.length = length;
        this.width = width;
    }
}

public class Room
{
    public bool fountainOn;
    public int x;
    public int y;

    public Room(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public virtual void Information(bool fountainOn) { }
}

public class FountainRoom : Room
{
    public FountainRoom(int x, int y) : base(x, y)
    {
        this.x = x;
        this.y = y;
    }
    public override void Information(bool fountainOn)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        if (!fountainOn)
        {
            Console.WriteLine("You hear water dripping in this room. The Fountain of Objects is here!");
        }
        else
        {
            Console.WriteLine("You hear the rushing waters from the Fountain of Objects It has been reactivated!");
        }
    }
}

public class Entrance : Room
{
    public Entrance(int x, int y) : base(x, y)
    {
        this.x = x;
        this.y = y;
    }
    public override void Information(bool fountainOn)
    {
        if (!fountainOn)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You see light coming from the cavern entrance.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("The Fountain of objects has been reactivated, and you have escaped with your life! \nYou win!");
            Environment.Exit(0);
        }
    }
}

// public class Pit : Room
// {
//     public Pit(int x, int y) : base(x, y)
//     {
//         this.x = x;
//         this.y = y;
//     }
//     public override void Information(bool fountainOn)
//     {
//         Console.ForegroundColor = ConsoleColor.Red;
//         Console.WriteLine("You have fallen to your death. You lose.");
//         Environment.Exit(0);
//     }
// }