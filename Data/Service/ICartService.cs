using Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Service
{
    public interface ICartService
    {
        Task<Cart> Add(string token, DateTime date_exp);
        Task<Cart> Get(int userId);
        Task<List<CartItem>> Get_Cart_Items(int cId);
        Task<Cart> Clean_Cart_By_Token(string token);
        Task<int> GetCartItemsNumber(int cId);
        Task<bool> Add_Cart_Item(int id, int aId, int kolicina, double cena);
        Task<bool> Remove_CartItem(int cId);
        Task<bool> Update_Cart_Item(int cId, float kolicina);
        Task<Cart> Get_By_Token(string token);
    }
}
