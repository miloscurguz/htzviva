using Data.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using viva.webstore.Models;

namespace viva.webstore.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly IArtikliService _artikliService;
        private readonly ICartService _cartService;
        [BindProperty] public Models.Cart VM { get; set; }
        public IndexModel(IArtikliService artikliService, ICartService cartService)
        {
            _artikliService = artikliService;
            _cartService = cartService;
        }


        public async Task OnGet()
        {
            CheckCartCookie();
            VM = new Models.Cart();
            string cookie_token = Request.Cookies["cart"];

            if (String.IsNullOrEmpty(cookie_token))
            {
                Cookie_Set("cart", Guid.NewGuid().ToString(), 1296000000);
            }
            var cart = await _cartService.Get_By_Token(cookie_token);
            if (cart != null)
            {

                var cart_items = await _cartService.Get_Cart_Items(cart.Id);
                VM.token = cart.Token;
                double total_price = 0;
                foreach (var item in cart_items)
                {
                    var artikal = await _artikliService.Artikal(item.ArtikalId);
                    VM.items.Add(new Cart_Item { 
                        product_title = artikal.Naziv, 
                        quantity = Convert.ToInt32(item.Kolicina),
                        price = Convert.ToDouble(item.Cena),
                        id = item.Id,
                        product_id = artikal.Id,
                        image = artikal.Slika, 
                        product_color=artikal.Color,
                        product_size=artikal.Size,
                        final_price = item.Kolicina * item.Cena });
                    total_price += item.Cena * item.Kolicina;
                };
                VM.item_count = VM.items.Count;
                VM.total_price = total_price;

            }

        }
        public async Task<IActionResult> OnPostChange(string quantity, string line)
        {
           
            if (int.Parse(quantity) <= 0)
            {
                bool remove_success = false;
                string cookie_token = Request.Cookies["cart"];
                var vm = new Models.Cart();
               
                var cart = await _cartService.Get_By_Token(cookie_token);
                if (cart == null)
                {
                    cart = await _cartService.Add(cookie_token, DateTime.Now.AddDays(30));
                }

                remove_success = await _cartService.Remove_CartItem(Convert.ToInt32(line));
                if (remove_success)
                {
                    var cart_items = await _cartService.Get_Cart_Items(cart.Id);
                    vm.token = cart.Token;
                    double total_price = 0;
                    foreach (var item in cart_items)
                    {

                        var artikal = await _artikliService.Artikal(item.ArtikalId);
                        vm.items.Add(new Cart_Item
                        {
                            title = artikal.Naziv,
                            product_title= artikal.Naziv,
                            quantity = Convert.ToInt32(item.Kolicina),
                            price = Convert.ToDouble(item.Cena),
                            id = item.Id,
                            product_id = artikal.Id,
                            image = artikal.Slika,
                            url = "/Product?id=" + artikal.Id + "&amp;model=false"
                        });
                        total_price += item.Cena * item.Kolicina;
                    }
                    vm.item_count = vm.items.Count;
                    vm.total_price = total_price;
                    return new JsonResult(vm);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                bool update_success = false;
                string cookie_token = Request.Cookies["cart"];
                var vm = new Models.Cart();
              
                var cart = await _cartService.Get_By_Token(cookie_token);
                if (cart == null)
                {
                    cart = await _cartService.Add(cookie_token, DateTime.Now.AddDays(30));
                }

                update_success = await _cartService.Update_Cart_Item(Convert.ToInt32(line), int.Parse(quantity));
                if (update_success)
                {
                    var cart_items = await _cartService.Get_Cart_Items(cart.Id);
                    vm.token = cart.Token;
                    double total_price = 0;
                    foreach (var item in cart_items)
                    {
                        if(item.Id== Convert.ToInt32(line))
                        {
                            vm.current_total_price = item.Kolicina * item.Cena;
                            vm.current_item_id = item.Id;
                        }
                        var artikal = await _artikliService.Artikal(item.ArtikalId);
                        vm.items.Add(new Cart_Item { title = artikal.Naziv, quantity = Convert.ToInt32(item.Kolicina), price = Convert.ToDouble(item.Cena), id = item.Id, product_id = artikal.Id, image = artikal.Slika, final_price = item.Kolicina * item.Cena,
                            product_color = artikal.Color,
                            product_size = artikal.Size
                        });
                        total_price += item.Cena * item.Kolicina;
                    }
                    vm.item_count = vm.items.Count;
                    vm.total_price = total_price;
                    return new JsonResult(vm);
                }
                else
                {
                    return null;
                }
            }



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
