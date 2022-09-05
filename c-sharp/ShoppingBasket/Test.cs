using CSharp.ShoppingBasket.Implementation;

namespace CSharp.ShoppingBasket;

public class Test
{
    private readonly Shop shop;

    public Test()
    {
        this.shop = new Shop();
        this.shop.RegisterDiscount(new FivePercentDiscount());
    }
    
    [Fact]
    public void EmptyBasket_HasZeroTotal()
    {
        var basket = this.shop.GetBasket();
        Assert.Equal(0, shop.Checkout(basket));
    }
    
    [Fact]
    public void BasketWithOneItem_HasCorrectTotal()
    {
        this.shop.AddProduct(new Product("001", "Apple", 2));
        var basket = this.shop.GetBasket();
        basket.AddItem("001");
        Assert.Equal(2, shop.Checkout(basket));
    }
    
    [Fact]
    public void BasketTwoDifferentItems_HasCorrectTotal()
    {
        this.shop.AddProduct(new Product("001", "Apple", 2));
        this.shop.AddProduct(new Product("002", "Pear", 3));
        var basket = this.shop.GetBasket();
        basket.AddItem("001");
        basket.AddItem("002");
        Assert.Equal(5, shop.Checkout(basket));
    }
    
    [Fact]
    public void BasketMultipleSameItems_HasCorrectTotal()
    {
        this.shop.AddProduct(new Product("001", "Apple", 2));
        var basket = this.shop.GetBasket();
        basket.AddItem("001");
        basket.AddItem("001");
        basket.AddItem("001");
        Assert.Equal(6, shop.Checkout(basket));
    }
    
    [Fact]
    public void Discount_GetsApplied()
    {
        this.shop.AddProduct(new Product("001", "A", 10));
        this.shop.AddProduct(new Product("002", "B", 25));
        this.shop.AddProduct(new Product("003", "C", 9.99m));
        var basket = this.shop.GetBasket();
        basket.AddItem("001");
        basket.AddItem("001");
        basket.AddItem("001");
        basket.AddItem("001");
        basket.AddItem("001");
        basket.AddItem("002");
        basket.AddItem("002");
        basket.AddItem("003");
        basket.AddItem("003");
        basket.AddItem("003");
        basket.AddItem("003");
        basket.AddItem("003");
        basket.AddItem("003");
        Assert.Equal(151.94m, shop.Checkout(basket));
    }
}