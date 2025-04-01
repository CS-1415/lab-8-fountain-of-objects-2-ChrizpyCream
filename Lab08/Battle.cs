namespace Lab08;

public class Battle
{
    public Battle() { }
    public void TotalBattle(Player player, Monster[,] monster)
    {
        int tempMax = player.inventory.items[0].damageMax;
        player.equippedItem = player.inventory.items[0];
        for (int i = 1; i < player.inventory.items.Count(); i++)
        {
            if (player.inventory.items[i].damageMax > tempMax)
            {
                player.equippedItem = player.inventory.items[i];
            }
        }
        while (true)
        {
            if (player.health <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You have died. You lose");
                Environment.Exit(0);
            }
            PlayerTurn(player, monster[player.x, player.y]);
            if (monster[player.x, player.y].health <= 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("You have slain the monster");
                player.inventory.items.AddRange(monster[player.x, player.y].inventory.items);
                monster[player.x, player.y] = null;
                break;
            }
            MonsterTurn(player, monster[player.x, player.y]);
        }
    }
    public void PlayerTurn(Player player, Monster monster)
    {
        bool loop = true;
        while (loop)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("What would you like to do? (attack, drink potion, check health, check inventory)");
            Console.ForegroundColor = ConsoleColor.Cyan;
            string action = Console.ReadLine();
            switch (action)
            {
                case "attack":
                    Random attackRoll = new Random();
                    Random damageRoll = new Random();
                    if (attackRoll.Next(20) + 1 >= monster.armorClass)
                    {
                        int damageDealt = damageRoll.Next(player.equippedItem.damageMin, player.equippedItem.damageMax);
                        monster.health -= damageDealt;
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("You have hit the enemy for " + damageDealt + " damage!");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("You have missed.");
                    }
                    loop = false;
                    break;
                case "drink potion":
                    if (player.inventory.CountPotions(player.inventory) > 0)
                    {
                        player.health += 8;
                        for (int i = 0; i < player.inventory.items.Count(); i++)
                        {
                            if (player.inventory.items[i].name == "potion")
                            {
                                player.inventory.items.Remove(player.inventory.items[i]);
                                break;
                            }
                        }
                        Console.WriteLine("You have gained 8 health. You are now at " + player.health + " health.");
                        loop = false;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("You are out of potions.");
                    }
                    break;
                case "check health":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("You are at " + player.health + " health.");
                    break;
                case "check inventory":
                    player.inventory.CheckInventory(player.inventory, player);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Invalid input. Try again.");
                    break;
            }
        }
    }
    public void MonsterTurn(Player player, Monster monster)
    {
        Random attackRoll = new Random();
        Random damageRoll = new Random();
        if (attackRoll.Next(20) + 1 >= player.armorClass)
        {
            int damageDealt = damageRoll.Next(monster.equippedItem.damageMin, monster.equippedItem.damageMax);
            player.health -= damageDealt;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("You have been hit for " + damageDealt + " damage!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("The monster has missed.");
        }
    }
}