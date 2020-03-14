using AltezaWebApp.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AltezaWebApp.DAL
{
    public interface UserIDAL
    {
        DataTable GetValidateUser(IConfiguration configuration,LoginModel loginModel);
    }
}
