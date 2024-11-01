using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuanLyThueXe
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "list ô tô",
                url: "PublicCar/ListOto/{page}",
                defaults: new { controller = "PublicCar", action = "ListOto", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "list xe máy",
              url: "PublicCar/ListXeMay/{page}",
              defaults: new { controller = "PublicCar", action = "ListXeMay", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "PublicHome", action = "Index", id = UrlParameter.Optional }
            );
          

        }
    }
}
