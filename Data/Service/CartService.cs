using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Service
{
    public  class CartService:ICartService
    {

        e003186Context dbContext = new e003186Context();

        public async Task<Cart> Get(int userId)
        {
            var cart = await dbContext.Cart.Where(x => x.UserId == userId).FirstOrDefaultAsync();
            return cart;
        }

        public async Task<List<CartItem>> Get_Cart_Items(int cId)
        {
            var cartItems = await dbContext.CartItem.Where(x => x.CartId == cId).ToListAsync();
            return cartItems;
        }

        public async Task<bool> Update_Cart_Item(int cId, float kolicina)
        {
            var cartItem = await dbContext.CartItem.Where(x => x.Id == cId).FirstOrDefaultAsync();
            cartItem.Kolicina = kolicina;
            try
            {
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<bool> Remove_CartItem(int cId)
        {
            var cartItem = await dbContext.CartItem.Where(x => x.Id == cId).FirstOrDefaultAsync();

            try
            {
                dbContext.Remove(cartItem);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<Cart> Add(string token,DateTime date_exp)
        {
            var cart = new Cart();
           
            cart.Token = token;
            cart.ExpDate = date_exp;

            dbContext.Cart.Add(cart);
            try
            {
                dbContext.SaveChanges();
                return cart;
            }
            catch(Exception ex)
            {
                return null;
            }
          
           
        }

        public async Task<bool> Add_Cart_Item(int id, int aId, int kolicina, double cena)
        {
            var cart = await dbContext.Cart.Where(x => x.Id == id).FirstOrDefaultAsync();
            var item = await dbContext.CartItem.Where(x => x.CartId == cart.Id && x.ArtikalId == aId).FirstOrDefaultAsync();
            if (item != null)
            {
                item.Kolicina += kolicina;

            }
            else
            {
                var cartItem = new CartItem();
                cartItem.CartId = id;
                cartItem.ArtikalId = aId;
                cartItem.Kolicina = kolicina;
                cartItem.Cena = cena;
                await dbContext.CartItem.AddAsync(cartItem);
            }

            dbContext.SaveChanges();
            return true;
        }

        public async Task<int> GetCartItemsNumber(int cId)
        {
            var cartItems = dbContext.CartItem.Where(x => x.CartId == cId).Sum(x => x.Kolicina);
            return Convert.ToInt32(cartItems);
        }

        public async Task<Cart> Get_By_Token(string token)
        {
            var cart = await dbContext.Cart.Where(x=>x.Token== token).FirstOrDefaultAsync();
            return cart;
        
        }
        public async Task<Cart> Clean_Cart_By_Token(string token)
        {
            var cart = await dbContext.Cart.Where(x => x.Token == token).FirstOrDefaultAsync();
            var cart_items=await dbContext.CartItem.Where(x=>x.CartId== cart.Id).ToListAsync();
            dbContext.CartItem.RemoveRange(cart_items);
            dbContext.SaveChanges();
            return cart;

        }
    }
}
