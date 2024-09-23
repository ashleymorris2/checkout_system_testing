namespace Checkout;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}

public class Item
{
    public string Name { get; set; }
    public int UnitPrice { get; set; }
    public int? SpecialPrice { get; set; }

    public Item(string name, int unitPrice, int? specialPrice)
    {
        Name = name;
        UnitPrice = unitPrice;
        SpecialPrice = specialPrice;
    }
}

class Till
{
    List<Item> Items = new List<Item>();

    public void AddToCart(Item item)
    {
        Items.Add(item);
    }

    public int Checkout()
    {
        var itemA = Items.Where(i => i.Name == "Item A").ToList();
        var itemAPrice = (itemA.Count / 3) * 25 + (itemA.Count % 3) * 10;
        
        var itemB = Items.Where(i => i.Name == "Item B").ToList();
        var itemBPrice = (itemB.Count / 2) * 30 + (itemB.Count % 2) * 20;
        
        return itemAPrice + itemBPrice + Items.Count(i => i.Name == "Item C") * 30;
    }
}