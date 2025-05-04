using System;
using KataCheckout.Interfaces;
using KataCheckout.Models;

namespace KataCheckout.Services;

/// <summary>
/// Implements the checkout process with pricing rules
/// </summary>
public class Checkout : ICheckout
{
    private readonly IPricingRuleProvider _pricingRuleProvider;
    private readonly Dictionary<string, int> _scannedItems = [];
    
    /// <summary>
    /// Initializes a new instance of the Checkout class with the specified pricing rule provider
    /// </summary>
    /// <param name="pricingRuleProvider">The pricing rule provider (optional)</param>
    public Checkout(IPricingRuleProvider? pricingRuleProvider = null)
    {
        _pricingRuleProvider = pricingRuleProvider ?? new PricingRuleProvider(Enumerable.Empty<IPricingRule>());
    }
    
    /// <summary>
    /// Creates a checkout with the specified pricing rules
    /// </summary>
    /// <param name="pricingRules">Collection of pricing rules</param>
    /// <returns>A new checkout instance</returns>
    public static Checkout WithPricingRules(IEnumerable<IPricingRule> pricingRules)
    {
        return new Checkout(new PricingRuleProvider(pricingRules));
    }
    

    
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
