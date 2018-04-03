using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace FinalProject
{
    public class RouteConfig
    {
       
        
        public static void RegisterRoutes(RouteCollection routes)
        {

            
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //Route config to the action ShowHomePage that available in Home controller.
           routes.MapRoute(
               name: "DefaultHome",
               url: "",
              defaults: new { controller = "Home", action = "ShowHomePage", id = UrlParameter.Optional }

           );
            //Route config to the Add Product  Action that available in Admin controller.
              routes.MapRoute(
              name: "AddProduct",
              url: "AddProduct",
              defaults: new { controller = "Admin", action = "AddProduct", id = UrlParameter.Optional }

             );
            //Route config to the Management Action that available in Admin controller.
            routes.MapRoute(
             name: "ManagementStore",
             url: "Management",
             defaults: new { controller = "Admin", action = "Management", id = UrlParameter.Optional }

             );

            //Route config to the Login Action that available in Admin controller.
           routes.MapRoute(
                name: "AdminPanel",
                url: "Login",
                defaults: new { controller = "Admin", action = "Login", id = UrlParameter.Optional }

                );
            //Route config to the ViewOrders Action that available in Admin controller.
            routes.MapRoute(
                name: "ViewOrders",
                url: "ViewOrders",
                defaults: new { controller = "Admin", action = "ViewOrders", id = UrlParameter.Optional }

                );
            //Route config to the ValidateAsServerLevel Action that available in Admin controller.
            routes.MapRoute(
            name: "ValidateAsServerLevel",
            url: "ValidateAsServerLevel",
            defaults: new { controller = "Admin", action = "ValidateAsServerLevel", id = UrlParameter.Optional }

           );
            //Route config to the AddOrder  Action that available in Buyer controller.
            routes.MapRoute(
            name: "AddOrder",
            url: "AddOrder",
            defaults: new { controller = "Buyer", action = "AddOrder", id = UrlParameter.Optional }

           );
            //Route config to the Buy Action that available in Buyer controller.
            routes.MapRoute(
            name: "BuyProduct",
            url: "Buy",
            defaults: new { controller = "Buyer", action = "Buy", id = UrlParameter.Optional }

           );
            //Route config to the Update Action that available in Admin controller.
          routes.MapRoute(
          name: "Update",
          url: "Update",
          defaults: new { controller = "Admin", action = "Update", id = UrlParameter.Optional }

         );
            //Route config to the Delete Action that available in Admin controller.
            routes.MapRoute(
           name: "Delete",
           url: "Delete",
           defaults: new { controller = "Admin", action = "Delete", id = UrlParameter.Optional }

          );
            //Route config to the GetProductByJson Action that available in Buyer controller.
            routes.MapRoute(
          name: "GetProductByJsone",
          url: "GetProductByJson",
          defaults: new { controller = "Buyer", action = "GetProductByJson", id = UrlParameter.Optional }

         );
        

            //Default route of MVC(to enable url: controller/action
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }

            );

        }
    }
}
