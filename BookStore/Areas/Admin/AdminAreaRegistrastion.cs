using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Areas.Admin
{
    public class AdminAreaRegistrastion : AreaRegistration
    {
        public override string AreaName {
            get { return "Admin"; } 
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AdminDefault",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Supplier", action = "Index", id = UrlParameter.Optional });
        }
    }
}