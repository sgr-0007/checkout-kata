using System;
using KataCheckout.Interfaces;

namespace KataCheckout.Services
{
    public class Checkout : ICheckout
    {
        private readonly Dictionary<string, int> _pricingRules;
        private readonly Dictionary<string, int> _scannedItems;
        
        // Default constructor for when no items are scanne
        public Checkout()
        {
            _pricingRules = new Dictionary<string, int>();
            _scannedItems = new Dictionary<string, int>();
        }
        
        public void Scan(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
                return;
                
            if (!_scannedItems.ContainsKey(item))
            {
                _scannedItems[item] = 0;
            }
            
            _scannedItems[item]++;
        }

        public int GetTotalPrice()
        {
            return 0;   
        }
    }
}
