namespace KataCheckout.Interfaces;

/// <summary>
/// Defines a pricing rule for a product
/// </summary>
public interface IPricingRule
{
    /// <summary>
    /// The Stock Keeping Unit (SKU) identifier for the product
    /// </summary>
    string SKU { get; }
    
    /// <summary>
    /// The unit price of the product
    /// </summary>
    int UnitPrice { get; }
    
    /// <summary>
    /// Calculates the price for a given quantity of items
    /// </summary>
    /// <param name="quantity">The quantity of items</param>
    /// <returns>The calculated price</returns>
    int CalculatePrice(int quantity);
}
