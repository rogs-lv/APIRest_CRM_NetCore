using CRM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Service.Interface
{
    public interface IAuthenticateService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest request);
    }
}
