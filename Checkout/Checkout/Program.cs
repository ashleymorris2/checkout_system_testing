namespace Checkout;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}

public class Item(string name, int unitPrice, int? specialPrice = null, int? specialPriceThreshold = null)
{
    public string Name { get; } = name;
    public int UnitPrice { get; } = unitPrice;
    public int? SpecialPrice { get; } = specialPrice;
    public int SpecialPriceThreshold { get; } = specialPriceThreshold ?? 0;
}

class Till
{
    private readonly List<Item> _items = [];

    public void AddToCart(Item item)
    {
        ArgumentNullException.ThrowIfNull(item);

        _items.Add(item);
    }

    public int Checkout()
    {
        var total = 0;
        foreach (var itemGroup in _items.GroupBy(i => i.Name))
        {
            var item = itemGroup.First();
            var count = itemGroup.Count();
            if (item.SpecialPrice.HasValue)
            {
                total += (count / item.SpecialPriceThreshold) * item.SpecialPrice.Value +
                         (count % item.SpecialPriceThreshold) * item.UnitPrice;
            }
            else
            {
                total += count * item.UnitPrice;
            }
        }

        return total;
    }
}