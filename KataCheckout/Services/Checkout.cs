using System;
using KataCheckout.Interfaces;
using KataCheckout.Models;

namespace KataCheckout.Services;

    public class Checkout(IPricingRuleProvider? pricingRuleProvider = null) : ICheckout
    {
        // Primary constructor initializes these fields
        private readonly IPricingRuleProvider _pricingRuleProvider = pricingRuleProvider ?? new PricingRuleProvider([]);
        private readonly Dictionary<string, int> _scannedItems = [];
        
        /// <summary>
        /// Constructor that accepts pricing rules directly
        /// </summary>
        /// <param name="pricingRules">Collection of pricing rules</param>
        public Checkout(IEnumerable<IPricingRule> pricingRules) 
            : this(new PricingRuleProvider(pricingRules))
        {
            // The primary constructor handles field initialization
        }
        
        /// <summary>
        /// Constructor that accepts a dictionary of SKUs and unit prices
        /// </summary>
        /// <param name="pricingRules">Dictionary with SKUs as keys and unit prices as values</param>
        public Checkout(Dictionary<string, int> pricingRules) 
            : this(CreateUnitPricingRules(pricingRules))
        {
            // The primary constructor handles field initialization
        }
        
        /// <summary>
        /// Helper method to convert a dictionary of SKUs and prices to pricing rule objects
        /// </summary>
        private static IEnumerable<IPricingRule> CreateUnitPricingRules(Dictionary<string, int> pricingRules)
        {
            var rules = new List<IPricingRule>();
            
            if (pricingRules != null)
            {
                foreach (var rule in pricingRules)
                {
                    rules.Add(new PricingRule(rule.Key, rule.Value));
                }
            }
            
            return rules;
        }
        
        /// <summary>
        /// Factory method to create a checkout with the default special offers
        /// </summary>
        public static Checkout CreateWithSpecialOffers()
        {
            var rules = new List<IPricingRule>
            {
                new PricingRule("A", 50, 3, 130),
                new PricingRule("B", 30, 2, 45),
                new PricingRule("C", 20),
                new PricingRule("D", 15)
            };
            
            return new Checkout(rules);
        }

        public void Scan(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
                return;
                
            if (!_scannedItems.TryGetValue(item, out int value))
            {
                value = 0;
                _scannedItems[item] = value;
            }
            
            _scannedItems[item] = ++value;
        }

        public int GetTotalPrice()
        {
            int totalPrice = 0;
            
            foreach (var item in _scannedItems)
            {
                var rule = _pricingRuleProvider.GetPricingRule(item.Key);
                if (rule != null)
                {
                    totalPrice += rule.CalculatePrice(item.Value);
                }
            }
            
            return totalPrice;
        }
    }
