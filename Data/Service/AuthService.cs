
using Data.Model.Models.Promosolutions;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Service
{
    public class AuthService : IAuthService
    {
        e003186Context dbContext = new e003186Context();

        public async Task<User> Authenticate(User model)
        {
            //var user = await dbContext.User.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefaultAsync();
            //return user;

            return null;
        }

        public async Task<PromosolutionsToken> Promosolutions_Token_Get()
        {
            var token = await dbContext.PromosolutionsToken.FirstOrDefaultAsync();
            return token;
        }

        public async Task<PromosolutionsToken> Promosolutions_Token_Set(TokenResponse model)
        {
            var tokenInput = new PromosolutionsToken()
            {
                AccessToken = model.AccessToken,
                TokenType = model.TokenType,
                Issued = Convert.ToDateTime(model.Issued),
                Expires = Convert.ToDateTime(model.Expires)
            };
            var token = await dbContext.PromosolutionsToken.FirstOrDefaultAsync();
            if (token != null)
            {
                dbContext.Remove(token);
            }
            try
            {
                dbContext.Add(tokenInput);
                dbContext.SaveChangesAsync();
                return tokenInput;
            }
            catch(Exception ex)
            {

                return null; 
            }
           
        }

        public async Task<bool> Register()
        {

            return false;
        }
    }
}
