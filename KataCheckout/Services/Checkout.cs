using KataCheckout.Interfaces;

namespace KataCheckout.Services;

/// <summary>
/// Implements the checkout process with pricing rules
/// </summary>
/// <remarks>
/// Initializes a new instance of the Checkout class with the specified pricing rule provider
/// </remarks>
/// <param name="pricingRuleProvider">The pricing rule provider</param>
public class Checkout(IPricingRuleProvider pricingRuleProvider) : ICheckout
{
    private readonly IPricingRuleProvider _pricingRuleProvider = pricingRuleProvider ?? throw new ArgumentNullException(nameof(pricingRuleProvider));
    private readonly Dictionary<string, int> _scannedItems = [];

    /// <summary>
    /// Scans an item and updates the total price
    /// </summary>
    /// <param name="item">The item to scan</param>
    public void Scan(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
                return;
                
            if (!_scannedItems.TryGetValue(item, out int value))
            {
                value = 0;
                _scannedItems[item] = value;
            }
            
            _scannedItems[item] = ++value;
        }

        /// <summary>
        /// Gets the total price of all scanned items
        /// </summary>
        /// <returns>The total price</returns>
        public int GetTotalPrice()
        {
            int totalPrice = 0;
            
            foreach (var item in _scannedItems)
            {
                var rule = _pricingRuleProvider.GetPricingRule(item.Key);
                if (rule != null)
                {
                    totalPrice += rule.CalculatePrice(item.Value);
                }
            }
            
            return totalPrice;
        }
    }
