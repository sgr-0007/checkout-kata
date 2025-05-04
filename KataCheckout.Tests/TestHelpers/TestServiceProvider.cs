using KataCheckout.Extensions;
using KataCheckout.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace KataCheckout.Tests.TestHelpers;

/// <summary>
/// Helper class for setting up dependency injection in tests
/// </summary>
public static class TestServiceProvider
{
    /// <summary>
    /// Creates a service provider with the specified pricing rules
    /// </summary>
    /// <param name="pricingRules">The pricing rules to use</param>
    /// <returns>A configured service provider</returns>
    public static ServiceProvider CreateProvider(IEnumerable<IPricingRule> pricingRules)
    {
        var services = new ServiceCollection();
        
        // Use the extension method to register services with specific rules
        services.AddCheckoutWithRules(pricingRules);
        
        return services.BuildServiceProvider();
    }
    
    /// <summary>
    /// Creates a service provider with an empty pricing rule provider
    /// </summary>
    /// <returns>A configured service provider</returns>
    public static ServiceProvider CreateEmptyProvider()
    {
        var services = new ServiceCollection();

        // Use the extension method to register services with empty rules
        services.AddCheckoutWithRules([]);
        
        return services.BuildServiceProvider();
    }
}
