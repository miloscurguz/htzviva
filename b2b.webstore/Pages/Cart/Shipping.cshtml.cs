using Data.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using viva.webstore.Models;

namespace viva.webstore.Pages.Cart
{
    public class ShippingModel : PageModel
    {
        private readonly IArtikliService _artikliService;
        private readonly ICartService _cartService;
        [BindProperty] public Models.Shipping VM { get; set; }
        public ShippingModel(IArtikliService artikliService, ICartService cartService)
        {
            _artikliService = artikliService;
            _cartService = cartService;
        }
        public async Task OnGet()
        {
            CheckCartCookie();
            VM = new Models.Shipping();
            string cookie_token = Request.Cookies["cart"];

            if (String.IsNullOrEmpty(cookie_token))
            {
                Cookie_Set("cart", Guid.NewGuid().ToString(), 1296000000);
            }
            var cart = await _cartService.Get_By_Token(cookie_token);
            if (cart != null)
            {

                var cart_items = await _cartService.Get_Cart_Items(cart.Id);
                VM.Cart.token = cart.Token;
                double total_price = 0;
                foreach (var item in cart_items)
                {
                    var artikal = await _artikliService.Artikal(item.ArtikalId);
                    VM.Cart.items.Add(new Cart_Item { 
                        product_title = artikal.Naziv,
                        quantity = Convert.ToInt32(item.Kolicina),
                        price = Convert.ToDouble(item.Cena),
                        id = item.Id,
                        product_id = artikal.Id,
                        image = artikal.Slika,
                        final_price = item.Kolicina * item.Cena,
                        product_color = artikal.Color,
                        product_size = artikal.Size
                    });
                    total_price += item.Cena * item.Kolicina;
                };
                VM.Cart.item_count = VM.Cart.items.Count;
                VM.Cart.total_price = total_price;
                if (total_price >= 8000)
                {
                    VM.Isporuka = 0;
                    VM.Ukupno = total_price;
        
                }
                else
                {
                    if (VM.Isporuka == 2)
                    {
                        VM.Isporuka = 500;
                        VM.Ukupno = total_price + 500;
                    }
                    else
                    {
                        VM.Isporuka = 0;
                        VM.Ukupno = total_price + 0;
                    }
                    
                }
               
                

            }
        }

        public IActionResult OnPost(Shipping VM)
        {
            return RedirectToPage("/Cart/Confirmation", VM);
        }

        public void Cookie_Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);

            Response.Cookies.Append(key, value, option);
        }
        public void CheckCartCookie()
        {
            string cookie_token = Request.Cookies["cart"];
            if (cookie_token == null)
            {
                CookieOptions option = new CookieOptions() { Path = "/", HttpOnly = true, IsEssential = true, SameSite = SameSiteMode.Strict };
                option.Expires = DateTime.Now.AddMilliseconds(1296000000);

                try
                {
                    Response.Cookies.Append("cart", Guid.NewGuid().ToString(), option);
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }
            }
        }
    }
}
