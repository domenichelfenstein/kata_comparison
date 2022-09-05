namespace CSharp.ShoppingBasket.Implementation;

public interface IDiscount
{
    decimal Calculate(int quantity, decimal total);
}

public class FivePercentDiscount : IDiscount
{
    public decimal Calculate(int quantity, decimal total)
    {
        if (total > 100)
        {
            return total * 0.05m;
        }

        return 0;
    }
}