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
            ICheckout checkout = new Checkout();
            var totalPrice = checkout.GetTotalPrice();
            Assert.Equal(0, totalPrice);
        }

        [Fact]
public void WhenSingleItemAScanned_TotalPriceIs50()
{
    var pricingRules = new Dictionary<string, int> { { "A", 50 } };
    ICheckout checkout = new Checkout(pricingRules);
    checkout.Scan("A");
    var totalPrice = checkout.GetTotalPrice();
    Assert.Equal(50, totalPrice);
}
    }
}