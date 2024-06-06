namespace viva.webstore.Models
{
    public class CartViewModel
    {
        public CartViewModel()
        {
            Cart = new Cart();
        }

        public Cart Cart { get; set; }

        
    }
}
