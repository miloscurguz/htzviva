using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Service
{
    public class UserService : IUserService
    {
        e003186Context dbContext = new e003186Context();

        public async Task<bool> AddAddress(Adresa model)
        {
            if (model != null)
            {
                dbContext.Adresa.Add(model);
                dbContext.SaveChanges();
            }
            return true;
        }

        public async Task<Adresa> Adresa_Get(int uId)
        {
            var adresa=dbContext.Adresa.Where(x=>x.UserId==uId).FirstOrDefault();
            return adresa;
        }

        public async Task<User> AddUser(User model)
        {
            if (model != null)
            {
                try
                {
                    dbContext.User.Add(model);
                    dbContext.SaveChanges();
                    return model;
                }
                catch
                {
                    return null;
                }
               
            }
            return null;
            
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await dbContext.User.Where(x => x.Email == email).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetUserById(int id)
        {
           
            var user = await dbContext.User.Where(x => x.Id == id).FirstOrDefaultAsync();
            return user;
        }

        public async Task<bool> UpdateUser(int id,User model)
        {
            var user = await dbContext.User.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (user != null)
            {
                user.CalculusId = model.CalculusId;
                dbContext.SaveChanges();
            }
            return true;
        }
    }
}
