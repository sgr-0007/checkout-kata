using Xunit;
using KataCheckout.Interfaces;
using KataCheckout.Services;


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
            var pricingRules = new Dictionary<string, int> { { "A", 50 } };
            var checkout = new Checkout(pricingRules);
            checkout.Scan("A");
            var totalPrice = checkout.GetTotalPrice();
            Assert.Equal(50, totalPrice);
        }

        [Fact]
        public void WhenMultipleItemsScanned_TotalPriceIsSum()
        {
            var pricingRules = new Dictionary<string, int>
    {
            { "A", 50 },
            { "B", 30 },
            { "C", 20 },
            { "D", 15 }
    };
            var checkout = new Checkout(pricingRules);

            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("C");
            var totalPrice = checkout.GetTotalPrice();

            Assert.Equal(100, totalPrice); // 50 + 30 + 20 = 100
        }
        [Fact]
        public void WhenThreeItemsAScanned_SpecialPriceApplied()
        {
            var checkout = Checkout.CreateWithSpecialOffers();

            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            var totalPrice = checkout.GetTotalPrice();

            Assert.Equal(130, totalPrice); // Special price: 3A for 130
        }
    }
}