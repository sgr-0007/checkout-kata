using KataCheckout.Interfaces;

namespace KataCheckout.Models;

    public class UnitPricingRule(string sku, int unitPrice) : IPricingRule
{
    public string SKU { get; } = sku;
    public int UnitPrice { get; } = unitPrice;
    public int CalculatePrice(int quantity)
    {
        return UnitPrice * quantity;
    }
}
