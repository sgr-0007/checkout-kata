using KataCheckout.Interfaces;
using KataCheckout.Models;

namespace KataCheckout.Tests.TestData
{
    /// <summary>
    /// Provides test data for pricing rules
    /// </summary>
    public static class PricingRuleTestData
    {
        /// <summary>
        /// Gets the standard set of pricing rules used in tests
        /// </summary>
        public static List<IPricingRule> GetStandardPricingRules()
        {
            return
            [
                new PricingRule("A", 50, 3, 130),
                new PricingRule("B", 30, 2, 45),
                new PricingRule("C", 20),
                new PricingRule("D", 15)
            ];
        }
    }
}
