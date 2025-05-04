using Xunit;
using KataCheckout.Services;
using KataCheckout.Tests.TestData;

namespace KataCheckout.Tests.TestData
{
    public class CheckoutTestsWithTestData
    {
        [Fact]
        public void WhenUsingTestData_SpecialPricesAreAppliedCorrectly()
        {
            // Get the standard pricing rules from test data
            var pricingRules = PricingRuleTestData.GetStandardPricingRules();
            
            // Create checkout with the standard pricing rules
            var checkout = new Checkout(pricingRules);
            
            // Scan items
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A"); // Should apply special offer: 3A for 130
            checkout.Scan("B");
            checkout.Scan("B"); // Should apply special offer: 2B for 45
            checkout.Scan("C");
            checkout.Scan("D");
            
            var totalPrice = checkout.GetTotalPrice();
            
            // Expected: 130 (3A) + 45 (2B) + 20 (C) + 15 (D) = 210
            Assert.Equal(210, totalPrice);
        }
        
        [Fact]
        public void WhenUsingTestData_PartialSpecialOffersAreCalculatedCorrectly()
        {
            // Get the standard pricing rules from test data
            var pricingRules = PricingRuleTestData.GetStandardPricingRules();
            
            // Create checkout with the standard pricing rules
            var checkout = new Checkout(pricingRules);
            
            // Scan items
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A"); // 3A special + 1A regular
            checkout.Scan("B"); // 1B regular
            
            var totalPrice = checkout.GetTotalPrice();
            
            // Expected: 130 (3A) + 50 (1A) + 30 (1B) = 210
            Assert.Equal(210, totalPrice);
        }
    }
}
