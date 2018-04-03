using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Product
    {
        //data annotation to define to display error to the user if not enterd nothing in the field of the name of the product.   
        [Required(ErrorMessage = "Required field")]
        public string namep { get; set; }
        //data annotation to define to display error to the user if not enterd nothing in the field of the price of the product.   
        [Required(ErrorMessage = "Required field")]
        //Define valid regular expression, if user entered string that no suitable to the regular expression he get error message.
        [RegularExpression("^([0-9]+([/.][0-9]+)?)$", ErrorMessage = "Price of product is Invalid")]
        public double price { get; set; }
        //data annotation to define to display error to the user if not enterd nothing in the field of the amount of the product.   
        [Required(ErrorMessage = "Required field")]
        //Define valid regular expression, if user entered string that no suitable to the regular expression he get error message.
        [RegularExpression("^([0-9]{1,9})$", ErrorMessage = "Amount is Invalid")]
        public int amount { get; set; }
        //define PID as key as defined in the Products table in the database(each PID will appears only once).
        [Key]
        //data annotation to define to display error to the user if not enterd nothing in the field of the PID of the product.   
        [Required(ErrorMessage = "Required field")]
        //Define valid regular expression, if user entered string that no suitable to the regular expression he get error message.
        [RegularExpression("^([A-Z][0-9][0-9][0-9])$", ErrorMessage = "PID must be contain first uppercase letter and 3 digits after")]
        public string PID { get; set; }
        //This field used to define the status of order in the store( deleted:false,exist:true).

        public bool mode  { get; set; }



    }
    }