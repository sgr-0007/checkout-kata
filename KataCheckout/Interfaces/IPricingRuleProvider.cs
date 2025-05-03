namespace KataCheckout.Interfaces;

public interface IPricingRuleProvider
{
    IPricingRule? GetPricingRule(string sku);
}
