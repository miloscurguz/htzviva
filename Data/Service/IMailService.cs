using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Service
{
    public interface IMailService
    {
        public bool SendWelcomeEmail(int uId);
        public bool SendOrderToAdmin(int oId);
        public bool SendORderToCustomer(int odId,Data.Model.Models.Shipping input);
    }
}
