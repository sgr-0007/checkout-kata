using System;
using KataCheckout.Interfaces;

namespace KataCheckout.Services
{
    public class Checkout : ICheckout
    {
        public void Scan(string item)
        {
            throw new NotImplementedException();
        }

        public int GetTotalPrice()
        {
            return 0;
        }
    }
}
