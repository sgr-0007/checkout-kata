using KataCheckout.Interfaces;
using KataCheckout.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KataCheckout.Extensions;

/// <summary>
/// Extension methods for registering checkout services with dependency injection
/// </summary>
public static class CheckoutServiceExtensions
{

    /// <summary>
    /// Registers the checkout services with specific pricing rules
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="pricingRules">The pricing rules to use</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddCheckoutWithRules(this IServiceCollection services, 
        IEnumerable<IPricingRule> pricingRules)
    {
        // Register the pricing rule provider with specific rules
        services.AddTransient<IPricingRuleProvider>(sp => 
            new PricingRuleProvider(pricingRules));
        
        // Register the checkout
        services.AddTransient<ICheckout, Checkout>();
        
        return services;
    }
}
