namespace KataCheckout.Interfaces;

public interface IPricingRule
{
    string SKU { get; }
    int CalculatePrice(int quantity);
}

