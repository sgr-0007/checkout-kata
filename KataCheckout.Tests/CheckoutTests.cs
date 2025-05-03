using Xunit;
using KataCheckout.Interfaces;

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
    }
}