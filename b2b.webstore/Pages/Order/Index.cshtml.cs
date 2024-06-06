using Data.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System;
using viva.webstore.Models;
using System.Collections.Generic;
using AutoMapper;
using Data.Models;
using System.Linq;

namespace viva.webstore.Pages.Order
{
    public class IndexModel : PageModel
    {
        private readonly IArtikliService _artikliService;
        private readonly ICartService _cartService;
        private readonly IEmailSender _mailService;
        
        private readonly IMapper _mapper;
        [BindProperty] public Shipping VM { get; set; }

        [BindProperty]
        public string Telefon { get; set; }
        public IndexModel(IArtikliService artikliService, ICartService cartService, IMapper mapper, IEmailSender mailService)
        {
            _artikliService = artikliService;
            _cartService = cartService;
            _mapper = mapper;
            _mailService = mailService;
        }


        public async Task OnGet(Shipping input_model)
        {
            CheckCartCookie();
            double total_price = 0;
            string cookie_token = Request.Cookies["cart"];
            var cart_items= new List<CartItem>();
            double isporuka = 0;

            if (String.IsNullOrEmpty(cookie_token))
            {
                Cookie_Set("cart", Guid.NewGuid().ToString(), 1296000000);
            }
            var cart = await _cartService.Get_By_Token(cookie_token);
            if (cart != null)
            {

                cart_items = await _cartService.Get_Cart_Items(cart.Id);
                //VM.Cart.token = cart.Token;
                //double total_price = 0;
                foreach (var item in cart_items)
                {
                   
                    total_price += item.Cena * item.Kolicina;
                };
                //VM.Cart.item_count = VM.Cart.items.Count;
                //VM.Cart.total_price = total_price;
                if (total_price >= 8000)
                {
                    isporuka = 0;
           

                }
                else
                {
                    if (input_model.Nacin_Isporuke == 2)
                    {
                        isporuka = 500;
                      
                    }
                    else
                    {
                        isporuka = 0;
                 
                    }

                }

            }
            Models.Order order = new Models.Order();
            List<Models.OrderItem> oItems = new List<Models.OrderItem>();
            if (cart_items.Count > 0)
            {
                Models.Order nOrder = new Models.Order()
                {
                    UserId = 0,
                    Datum = DateTime.Now,
                    Placanje = input_model.Nacin_Placanja,
                    Status = "F",
                    Napomena = input_model.Napomena,
                    Ukupno = Convert.ToDecimal(total_price) + Convert.ToDecimal(isporuka),
                    Isporuka= Convert.ToDecimal(isporuka),
                    Iznos = Convert.ToDecimal(total_price),
                    Referenca = RandomString(12)
                };
                var dbOrder = _artikliService.Order_Kreiraj(_mapper.Map<Data.Models.Order>(nOrder));
                order = _mapper.Map<Models.Order>(dbOrder);
                foreach (var item in cart_items)
                {
                    var artikal = await _artikliService.Artikal(item.ArtikalId);
                    Models.OrderItem oi = new Models.OrderItem() { };
                    oi.OrderId = dbOrder.Id;
                    oi.ArtikalId = Convert.ToInt32(item.ArtikalId);
                    oi.Kolicina = item.Kolicina;
                    oi.Cena = item.Cena;
                    oi.CartItemId = Convert.ToInt32(item.CartId);
                    oi.ArtikalName = artikal.Naziv;
                    _artikliService.Order_Item_Kreiraj(_mapper.Map<Data.Models.OrderItem>(oi));
                    oItems.Add(oi);
                }

                _mailService.SendORderToCustomer(dbOrder.Id,_mapper.Map<Data.Model.Models.Shipping>(input_model));
                await _cartService.Clean_Cart_By_Token(cookie_token);
            }

        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
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
