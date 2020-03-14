using System;
using System.Data;
using AltezaWebApp.Models;
using Microsoft.Extensions.Configuration;

namespace AltezaWebApp.DAL
{
    public interface MenuIDAL
    {
        DataTable GetWebMenus(IConfiguration configuration, MenuModel menuModel);
    }
}