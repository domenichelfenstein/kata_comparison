namespace CSharp.ShoppingBasket.Implementation;

public class Shop
{
    private readonly Dictionary<string, Product> products = new();
    private readonly List<IDiscount> discounts = new();

    public void RegisterDiscount(IDiscount discount)
    {
        this.discounts.Add(discount);
    }

    public void AddProduct(Product product)
    {
        this.products.Add(product.Id, product);
    }

    public Product GetProduct(string productId)
    {
        var product = this.products[productId];
        if(product == null)
        {
            throw new ArgumentException($"Product with id {productId} not found");
        }
        
        return product;
    }

    public Basket GetBasket()
    {
        return new Basket();
    }

    public decimal Checkout(Basket basket)
    {
        var totalBeforeDiscounts = basket.GetTotal(this);
        var nrOfItems = basket.GetItems().Count();

        var totalDiscount = 0m;
        foreach (var discount in discounts)
        {
            totalDiscount += discount.Calculate(nrOfItems, totalBeforeDiscounts);
        }

        var total = totalBeforeDiscounts - totalDiscount;
        
        return Math.Round(total, 2);
    }
}

public record Product(string Id, string Name, decimal Price);