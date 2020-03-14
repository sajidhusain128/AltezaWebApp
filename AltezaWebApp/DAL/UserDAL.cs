using AltezaWebApp.BAL;
using AltezaWebApp.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AltezaWebApp.DAL
{
    internal class UserDAL : UserIDAL
    {
        public DataTable GetValidateUser(IConfiguration configuration,LoginModel loginModel)
        {
            ConnectDB connectDB = new ConnectDB(configuration);
            DataTable dt = null;
            try
            {
                string sp_name = "usp_ValidateUser";
                SqlParameter[] Params = new SqlParameter[]
                {
                    new SqlParameter("@UserName",loginModel.Username),
                    new SqlParameter("@Password",loginModel.Password)
                };
                dt = connectDB.GetDataBySP(sp_name, Params);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return dt;
        }
    }
}