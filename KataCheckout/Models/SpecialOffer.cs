namespace KataCheckout.Models;

/// <summary>
/// Represents a special offer for a product
/// </summary>
public class SpecialOffer
{
    /// <summary>
    /// The quantity of items needed to qualify for the special offer
    /// </summary>
    public int Quantity { get; set; }
    
    /// <summary>
    /// The special price for the specified quantity
    /// </summary>
    public int OfferPrice { get; set; }
}
