namespace Lab08;

public class Player
{
    public int x;
    public int y;
    public Inventory inventory;
    public Item equippedItem;
    public int health;
    public int armorClass;
    public Player(int x, int y, Inventory inventory)
    {
        this.x = x;
        this.y = y;
        this.inventory = inventory;
        equippedItem = new Fist();
        health = 40;
        armorClass = 15;
    }

    public void MoveNorth()
    {
        y--;
    }
    public void MoveSouth()
    {
        y++;
    }
    public void MoveWest()
    {
        x--;
    }
    public void MoveEast()
    {
        x++;
    }
}