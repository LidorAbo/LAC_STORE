using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    //This class used to send json as object to the action of serach product by price range.
    public class ProductInputModel
    {
        //Defines price 1 of the range in the search
        public double price1 { get; set; }
        //Defines price 2 of the range in the search. 
        public double price2 { get; set; }

    }
}