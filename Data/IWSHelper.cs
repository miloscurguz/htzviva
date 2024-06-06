using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IWSHelper
    {
        Task<object> CallWebService(string source,string restRequest, Dictionary<string, string> par, object obj, bool IsString, bool Api = false, bool tekuca = true);
        Task<object> PromoSolutions_CallWebService(string source, string restRequest, Dictionary<string, string> par, object obj, bool IsString, bool isToken = false, string token = "");
        Task<object> LACUNA_CallWebService(string source, string restRequest, Dictionary<string, string> par, object obj, bool IsString, bool isToken = false, string token = "");
    }
}
