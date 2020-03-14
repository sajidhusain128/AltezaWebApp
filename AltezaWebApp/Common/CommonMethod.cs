using AltezaWebApp.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AltezaWebApp.BAL;
using Microsoft.Extensions.Configuration;

namespace AltezaWebApp.Common
{
    public static class CommonFunction
    {
        public static T ToModel<T>(this DataTable dt)
        {
            T data = default(T);
            data = GetItem<T>(dt.Rows[0]);
            return data;
        }
        public static IEnumerable<T> ToModelList<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName && dr[column.ColumnName] != DBNull.Value)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        public static IEnumerable<T> GetMenuListIfNull<T>(IConfiguration configuration,ITempDataDictionary TempData)
        {
            IEnumerable<MenuModel> menuModelList = TempDataHelper.Get<IEnumerable<MenuModel>>(TempData, "WebMenuItems");
            IEnumerable<T> TList = null;
            try
            {
                if (menuModelList == null)
                {
                    MenuBAL menuBAL = new MenuBAL();
                    menuModelList = menuBAL.GetWebMenus(configuration, new MenuModel());
                    TempDataHelper.Put<IEnumerable<MenuModel>>(TempData, "WebMenuItems", menuModelList);
                    TList = (IEnumerable<T>)menuModelList;
                }
                else
                {
                    TList = (IEnumerable<T>)menuModelList;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return TList;
        }
    }

    public static class TempDataHelper
    {
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o;
            tempData.Keep(key);
            tempData.TryGetValue(key, out o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }
    }
}
