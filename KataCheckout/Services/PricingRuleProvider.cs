using KataCheckout.Interfaces;

namespace KataCheckout.Services;

public class PricingRuleProvider(IEnumerable<IPricingRule> pricingRules) : IPricingRuleProvider
    {
        // Primary constructor initializes these fields
        private readonly Dictionary<string, IPricingRule> _pricingRules = pricingRules?.ToDictionary(rule => rule.SKU) ?? [];

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