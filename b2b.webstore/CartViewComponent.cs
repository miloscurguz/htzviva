using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using viva.webstore.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Data.Service;

namespace viva.webstore
{
    
    public class CartViewComponent: ViewComponent
    {

        public readonly IHttpContextAccessor _httpContext;
        private readonly IArtikliService _artikliService;
        private readonly ICartService _cartService;

        public CartViewComponent(IHttpContextAccessor httpContext, IArtikliService artikliService, ICartService cartService)
        {
            _httpContext = httpContext;
            _artikliService = artikliService;
            _cartService = cartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            CartViewModel mvm = new CartViewModel();

            CheckCartCookie();
            var VM = new Models.Cart();
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
                    VM.items.Add(new Cart_Item { product_title = artikal.Naziv, quantity = Convert.ToInt32(item.Kolicina), price = Convert.ToDouble(item.Cena), id = item.Id, product_id = artikal.Id, image = artikal.Slika, final_price = item.Kolicina * item.Cena });
                    total_price += item.Cena * item.Kolicina;
                };
                VM.item_count = VM.items.Count;
                VM.total_price = total_price;

            }
            CartViewModel CVM = new CartViewModel();
            CVM.Cart = VM;
            return await Task.FromResult((IViewComponentResult)View(CVM));
        }
        public void Cookie_Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);

            _httpContext.HttpContext.Response.Cookies.Append(key, value, option);
        }

        public void CheckCartCookie()
        {
            string cookie_token = _httpContext.HttpContext.Request.Cookies["cart"];
            if (cookie_token == null)
            {
                CookieOptions option = new CookieOptions() { Path = "/", HttpOnly = true, IsEssential = true, SameSite = SameSiteMode.Strict };
                option.Expires = DateTime.Now.AddMilliseconds(1296000000);

                try
                {
                    _httpContext.HttpContext.Response.Cookies.Append("cart", Guid.NewGuid().ToString(), option);
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }
            }
        }
    }

 
}
