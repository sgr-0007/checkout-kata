using KataCheckout.Interfaces;

namespace KataCheckout.Models;

/// <summary>
/// Represents a pricing rule for a product
/// </summary>
/// <remarks>
/// Initializes a new instance of the PricingRule class
/// </remarks>
/// <param name="sku">The SKU of the product</param>
/// <param name="unitPrice">The unit price of the product</param>
public class PricingRule(string sku, int unitPrice) : IPricingRule
{

    /// <summary>
    /// Initializes a new instance of the PricingRule class with a special offer
    /// </summary>
    /// <param name="sku">The SKU of the product</param>
    /// <param name="unitPrice">The unit price of the product</param>
    /// <param name="specialQuantity">The quantity required for the special offer</param>
    /// <param name="specialPrice">The special offer price</param>
    /// <remarks>
/// This constructor creates a pricing rule with both a unit price and a special offer.
/// For example, to create "3 items for 130" offer, use specialQuantity=3 and specialPrice=130.
/// </remarks>
    public PricingRule(string sku, int unitPrice, int specialQuantity, int specialPrice) 
        : this(sku, unitPrice)
    {
        SpecialOffer = new SpecialOffer
        {
            Quantity = specialQuantity,
            OfferPrice = specialPrice
        };
    }

    /// <summary>
    /// The Stock Keeping Unit (SKU) identifier for the product
    /// </summary>
    public string SKU { get; } = sku;

    /// <summary>
    /// The unit price of the product
    /// </summary>
    public int UnitPrice { get; } = unitPrice;

    /// <summary>
    /// The special offer for the product, if any
    /// </summary>
    public SpecialOffer? SpecialOffer { get; }
    
    /// <summary>
    /// Calculates the price for a given quantity of items
    /// </summary>
    /// <param name="quantity">The quantity of items</param>
    /// <returns>The calculated price</returns>
    public int CalculatePrice(int quantity)
    {
        if (SpecialOffer == null || quantity < SpecialOffer.Quantity)
        {
            return UnitPrice * quantity;
        }
        
        // Calculate how many complete special offers can be applied
        int specialOfferCount = quantity / SpecialOffer.Quantity;
        
        // Calculate the remaining items that don't form a complete special offer
        int remainingItems = quantity % SpecialOffer.Quantity;
        
        // Calculate the total price
        return (specialOfferCount * SpecialOffer.OfferPrice) + (remainingItems * UnitPrice);
    }
}
