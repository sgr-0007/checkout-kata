namespace KataCheckout.Interfaces

{
    public interface ICheckout
    {

        void Scan(string item);
        int GetTotalPrice();

    }
}