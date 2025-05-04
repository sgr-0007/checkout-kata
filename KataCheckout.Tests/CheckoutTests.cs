using Xunit;
using KataCheckout.Interfaces;
using KataCheckout.Services;
using KataCheckout.Tests.TestData;
using KataCheckout.Tests.TestHelpers;
using Microsoft.Extensions.DependencyInjection;

namespace KataCheckout.Tests
{
    public class CheckoutTests
    {
        [Fact]
        public void WhenNoItemsScanned_TotalPriceIsZero()
        {
            // Arrange - Get checkout from DI container with empty rules
            var serviceProvider = TestServiceProvider.CreateEmptyProvider();
            var checkout = serviceProvider.GetRequiredService<ICheckout>();
            
            // Act
            var totalPrice = checkout.GetTotalPrice();
            
            // Assert
            Assert.Equal(0, totalPrice);
        }

        [Fact]
        public void WhenSingleItemAScanned_TotalPriceIs50()
        {
            // Arrange - Get checkout from DI container with standard pricing rules
            var serviceProvider = TestServiceProvider.CreateProvider(PricingRuleTestData.GetStandardPricingRules());
            var checkout = serviceProvider.GetRequiredService<ICheckout>();
            
            // Act
            checkout.Scan("A");
            var totalPrice = checkout.GetTotalPrice();
            
            // Assert
            Assert.Equal(50, totalPrice);
        }

        [Fact]
        public void WhenMultipleItemsScanned_TotalPriceIsSum()
        {
            // Arrange - Get checkout from DI container with standard pricing rules
            var serviceProvider = TestServiceProvider.CreateProvider(PricingRuleTestData.GetStandardPricingRules());
            var checkout = serviceProvider.GetRequiredService<ICheckout>();

            // Act
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("C");
            var totalPrice = checkout.GetTotalPrice();

            // Assert
            Assert.Equal(100, totalPrice); // 50 + 30 + 20 = 100
        }
        [Fact]
        public void WhenThreeItemsAScanned_SpecialPriceApplied()
        {
            // Arrange - Get checkout from DI container with standard pricing rules
            var serviceProvider = TestServiceProvider.CreateProvider(PricingRuleTestData.GetStandardPricingRules());
            var checkout = serviceProvider.GetRequiredService<ICheckout>();

            // Act
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            var totalPrice = checkout.GetTotalPrice();

            // Assert
            Assert.Equal(130, totalPrice); // Special price: 3A for 130
        }
        [Fact]
        public void WhenTwoItemsBScanned_SpecialPriceApplied()
        {
            // Arrange - Get checkout from DI container with standard pricing rules
            var serviceProvider = TestServiceProvider.CreateProvider(PricingRuleTestData.GetStandardPricingRules());
            var checkout = serviceProvider.GetRequiredService<ICheckout>();

            // Act
            checkout.Scan("B");
            checkout.Scan("B");
            var totalPrice = checkout.GetTotalPrice();

            // Assert
            Assert.Equal(45, totalPrice); // Special price: 2B for 45
        }

        [Fact]
        public void WhenMixedItemsWithSpecialOffersScanned_CorrectTotalPriceCalculated()
        {
            // Arrange - Get checkout from DI container with standard pricing rules
            var serviceProvider = TestServiceProvider.CreateProvider(PricingRuleTestData.GetStandardPricingRules());
            var checkout = serviceProvider.GetRequiredService<ICheckout>();
            
            // Act
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("A");
            checkout.Scan("C");
            checkout.Scan("D");
            var totalPrice = checkout.GetTotalPrice();
            
            // Assert
            Assert.Equal(210, totalPrice); // 130 (3A) + 45 (2B) + 20 (C) + 15 (D) = 210
        }
    }
}