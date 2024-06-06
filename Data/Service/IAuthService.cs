
using Data.Model.Models.Promosolutions;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Service
{
    public interface  IAuthService
    {
        Task<User> Authenticate(User model);
        Task<bool> Register();
        Task<PromosolutionsToken> Promosolutions_Token_Get();
        Task<PromosolutionsToken> Promosolutions_Token_Set(TokenResponse model);
    }
}
