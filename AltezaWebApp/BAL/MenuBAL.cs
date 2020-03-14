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
    public class MenuBAL
    {
        private MenuIDAL GetMenuIDAL()
        {
            DALFactories obDALFactory = null;
            MenuIDAL obMenuIDAL = null;
            try
            {
                obDALFactory = new DALFactories();
                obMenuIDAL = obDALFactory.GetMenuDAL();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obMenuIDAL;
        }

        public IEnumerable<MenuModel> GetWebMenus(IConfiguration configuration, MenuModel menuModel)
        {
            DataTable dt = null;
            IEnumerable<MenuModel> MenuModel2 = null;
            MenuIDAL obMenuIDAL = null;
            try
            {
                obMenuIDAL = GetMenuIDAL();
                dt = obMenuIDAL.GetWebMenus(configuration, menuModel);
                MenuModel2 = CommonFunction.ToModelList<MenuModel>(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return MenuModel2;
        }
    }
}
