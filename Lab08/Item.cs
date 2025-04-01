namespace Lab08;

public class Item
{
    public int damageMax;
    public int damageMin;
    public string name;
    public Item(int damageMax, int damageMin, string name)
    {
        this.damageMax = damageMax;
        this.damageMin = damageMin;
        this. name = name;
    }
    
}

public class Potion : Item
{
    public string name = "potion";
    public int healing;
    public Potion(int damageMax = 0, int damageMin = 0, string name = "potion") : base(damageMax, damageMin, name)
    {
        this.damageMax = damageMax;
        this.damageMin = damageMin;
        this.name = name;
        healing = 5;
    }
}
public class Fist : Item
{
    public string name;
    public int damageMin;
    public int damageMax;
    public Fist(int damageMax = 4, int damageMin = 1, string name = "fist") : base(damageMax, damageMin, name)
    {
        this.damageMax = damageMax;
        this.damageMin = damageMin;
        this.name = name;
    }
}
public class Sword : Item
{
    public string name;
    public int damageMin;
    public int damageMax;
    public Sword(int damageMax = 8, int damageMin = 2, string name = "sword") : base(damageMax, damageMin, name)
    {
        this.damageMax = damageMax;
        this.damageMin = damageMin;
        this.name = name;
    }
}