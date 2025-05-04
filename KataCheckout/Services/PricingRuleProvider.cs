using KataCheckout.Interfaces;

namespace KataCheckout.Services;

/// <summary>
/// Provides access to pricing rules by SKU
/// </summary>
public class PricingRuleProvider : IPricingRuleProvider
{
    private readonly Dictionary<string, IPricingRule> _pricingRules;
    
    /// <summary>
    /// Initializes a new instance of the PricingRuleProvider class
    /// </summary>
    /// <param name="pricingRules">Collection of pricing rules</param>
    public PricingRuleProvider(IEnumerable<IPricingRule> pricingRules)
    {
        _pricingRules = pricingRules?.ToDictionary(rule => rule.SKU) ?? [];
    }
    
    /// <summary>
    /// Gets the pricing rule for a specific SKU
    /// </summary>
    /// <param name="sku">The SKU to look up</param>
    /// <returns>The pricing rule for the given SKU, or null if not found</returns>
    public IPricingRule? GetPricingRule(string sku)
    {
        return _pricingRules.TryGetValue(sku, out var rule) ? rule : null;
    }
}