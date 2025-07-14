using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopManagementService
{
    public interface IEmailSender { 
        bool SendEmail(string email, string subject, string mess); 
        string GetOTP(); 
    }
}
