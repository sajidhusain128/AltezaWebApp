using AltezaWebApp.Common;
using AltezaWebApp.DAL;
using AltezaWebApp.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AltezaWebApp.BAL
{
    public class UserBAL
    {
        private UserIDAL GetUserIDAL()
        {
            DALFactories obDALFactory = null;
            UserIDAL obUserIDAL = null;
            try
            {
                obDALFactory = new DALFactories();
                obUserIDAL = obDALFactory.GetUserDAL();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obUserIDAL;
        }

        public LoginModel GetValidateUser(IConfiguration configuration,LoginModel loginModel)
        {
            DataTable dt = null;
            LoginModel loginModel2 = null;
            UserIDAL obUserIDAL = null;
            try
            {
                obUserIDAL = GetUserIDAL();
                dt = obUserIDAL.GetValidateUser(configuration,loginModel);
                loginModel2 = CommonFunction.ToModel<LoginModel>(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return loginModel2;
        }
    }
}
