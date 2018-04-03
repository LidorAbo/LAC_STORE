using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.ModelBinders
{
    public class AdminBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpContextBase obj = controllerContext.HttpContext;
            User admin = new User()
            {
               
            };
            return admin;
        }
    }
}