﻿using System.Web;
using System.Web.Mvc;

namespace NevenIvanisic_RWA._21
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
