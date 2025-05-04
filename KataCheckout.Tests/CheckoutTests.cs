using Xunit;
using KataCheckout.Interfaces;
using KataCheckout.Services;
using KataCheckout.Tests.TestData;

namespace KataCheckout.Tests
{
    public class CheckoutTests
    {
        [Fact]
        public void WhenNoItemsScanned_TotalPriceIsZero()
        {
            var checkout = new Checkout();
            var totalPrice = checkout.GetTotalPrice();
            Assert.Equal(0, totalPrice);
        }

        [Fact]
        public void WhenSingleItemAScanned_TotalPriceIs50()
        {
            var checkout = Checkout.WithPricingRules(PricingRuleTestData.GetStandardPricingRules());
            checkout.Scan("A");
            var totalPrice = checkout.GetTotalPrice();
            Assert.Equal(50, totalPrice);
        }

        [Fact]
        public void WhenMultipleItemsScanned_TotalPriceIsSum()
        {
            var checkout = Checkout.WithPricingRules(PricingRuleTestData.GetStandardPricingRules());

            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("C");
            var totalPrice = checkout.GetTotalPrice();

            Assert.Equal(100, totalPrice); // 50 + 30 + 20 = 100
        }
        [Fact]
        public void WhenThreeItemsAScanned_SpecialPriceApplied()
        {
            var checkout = Checkout.WithPricingRules(PricingRuleTestData.GetStandardPricingRules());

            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            var totalPrice = checkout.GetTotalPrice();

            Assert.Equal(130, totalPrice); // Special price: 3A for 130
        }
        [Fact]
        public void WhenTwoItemsBScanned_SpecialPriceApplied()
        {
            var checkout = Checkout.WithPricingRules(PricingRuleTestData.GetStandardPricingRules());

            checkout.Scan("B");
            checkout.Scan("B");
            var totalPrice = checkout.GetTotalPrice();

            Assert.Equal(45, totalPrice); // Special price: 2B for 45
        }

        [Fact]
        public void WhenMixedItemsWithSpecialOffersScanned_CorrectTotalPriceCalculated()
        {
            var checkout = Checkout.WithPricingRules(PricingRuleTestData.GetStandardPricingRules());
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("A");
            checkout.Scan("C");
            checkout.Scan("D");
            var totalPrice = checkout.GetTotalPrice();
            Assert.Equal(210, totalPrice);
        }
    }
}