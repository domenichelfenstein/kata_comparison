namespace CSharp.ShoppingBasket.Implementation;

public class Basket
{
    private readonly Dictionary<string, int> items = new();

    public void AddItem(string productId)
    {
        if (this.items.ContainsKey(productId))
        {
            this.items[productId]++;
        }
        else
        {
            this.items.Add(productId, 1);
        }
    }

    public IEnumerable<BasketItem> GetItems()
    {
        foreach (var kvp in this.items)
        {
            yield return new BasketItem(kvp.Key, kvp.Value);
        }
    }

    public decimal GetTotal(Shop shop)
    {
        var total = 0m;
        foreach (var item in this.GetItems())
        {
            total += shop.GetProduct(item.ProductId).Price * item.Quantity;
        }

        return total;
    }
}

public record BasketItem(string ProductId, int Quantity);