using KataCheckout.Interfaces;

namespace KataCheckout.Models;

public class SpecialPricingRule(string sku, int unitPrice, int specialQuantity, int specialPrice) : IPricingRule
{
    public string SKU { get; } = sku;
    public int UnitPrice { get; } = unitPrice;
    public int SpecialQuantity { get; } = specialQuantity;
    public int SpecialPrice { get; } = specialPrice;
    

    
     public int CalculatePrice(int quantity)
        {
            // Calculate how many complete special offers can be applied
            int specialOfferCount = quantity / SpecialQuantity;
            
            // Calculate the remaining items that don't form a complete special offer
            int remainingItems = quantity % SpecialQuantity;
            
            // Calculate the total price
            return (specialOfferCount * SpecialPrice) + (remainingItems * UnitPrice);
        }

}
