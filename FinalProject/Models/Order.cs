using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Order
    {
        //define orderid as key as defined in the Orders table in the database(each order id will appears only once).
        [Key]
        public int OrderId
        {
            get; set;
        }

         //data annotation to define to display error to the user if not enterd nothing in the field of the name of the product.   
        [Required(ErrorMessage = "Required field")]
        public string namep { get; set; }
        //Define valid regular expression, if user entered string that no suitable to the regular expression he get error message.
        [RegularExpression("^([0-9]{1,9})$", ErrorMessage = "Amount is Invalid")]
        //data annotation to define to display error to the user if not enterd nothing in the field of the amount of the product.   
        [Required(ErrorMessage = "Required field")]
        public int amount { get; set; }
        //This field used to define the status of order in the store( deleted:false,exist:true).
        public bool mode { get; set; }
        //Define the time that order performed.
        public DateTime date
        {
            get; set;
        }
        //data annotation to define to display error to the user if not enterd nothing in the field of the name of the customer.   
        [Required(ErrorMessage = "Required field")]
        public string namec { get; set; }
        //data annotation to define to display error to the user if not enterd nothing in the field of the PID of the product.   
        [Required(ErrorMessage = "Required field")]
        //Define valid regular expression, if user entered string that no suitable to the regular expression he get error message.
        [RegularExpression("^([A-Z][0-9][0-9][0-9])$", ErrorMessage = "PID must be contain first uppercase letter and 3 digits after")]
        public string PID { get; set; }
    }
}