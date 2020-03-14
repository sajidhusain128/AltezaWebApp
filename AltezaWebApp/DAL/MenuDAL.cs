using AltezaWebApp.BAL;
using AltezaWebApp.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AltezaWebApp.DAL
{
    internal class MenuDAL : MenuIDAL
    {
        public DataTable GetWebMenus(IConfiguration configuration, MenuModel menuModel)
        {
            ConnectDB connectDB = new ConnectDB(configuration);
            DataTable dt = null;
            try
            {
                string sp_name = "usp_GetWebMenuItems";
                SqlParameter[] Params = new SqlParameter[]
                {
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