using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Service
{
    public interface IEmailSender
    {
        public bool SendOrderToAdmin(int oId);
        public bool SendORderToCustomer(int odId,Data.Model.Models.Shipping input);
        public bool SendContactMessage(Kontakt model);
    }
}
