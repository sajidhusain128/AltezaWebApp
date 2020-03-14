using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltezaWebApp.DAL
{
    public class DALFactories
    {
        public UserIDAL GetUserDAL()
        {
            UserIDAL obUserIDAL = null;
            try
            {
                obUserIDAL = new UserDAL();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obUserIDAL;
        }

        public MenuIDAL GetMenuDAL()
        {
            MenuIDAL obMenuIDAL = null;
            try
            {
                obMenuIDAL = new MenuDAL();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obMenuIDAL;
        }
    }
}
