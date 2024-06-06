using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Service
{
    public interface IUserService
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int id);
        Task<User> AddUser(User model);
        Task<bool> UpdateUser(int id,User model);
        Task<bool> AddAddress(Adresa model);
        Task<Adresa> Adresa_Get(int uId);


    }
}
