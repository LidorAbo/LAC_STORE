using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.ModelView
{
    //This class used to represent the form that contain table of products and input form(delete product, update product).
    public class ProductViewModel
    {
        //Defines new Product object to input form.
        public Product product { get; set; }
        //Defined new list of products to view table of products.
        public List<Product> products { get; set; }
       
    }
}