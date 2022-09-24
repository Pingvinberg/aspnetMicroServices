namespace Basket.API.Entities
{
    public class ShoppingCart
    {

        public string Username { get; set; }
        public ShoppingCartItem[] Items { get; set; }

        public ShoppingCart()
        {

        }

        public ShoppingCart(string username)
        {
            Username = username;            
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                foreach(var item in Items)
                {
                    totalprice += item.Price * item.Quantity;
                }
                return totalprice;
            }
        }
    }
}
