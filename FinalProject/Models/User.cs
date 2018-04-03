using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class User
    {
       
        //Define valid regular expression, if user entered string that no suitable to the regular expression he get error message.
        [RegularExpression("^([a-zA-Z]+[0-9]*[a-zA-Z]*)$", ErrorMessage = "Invalid UserName")]
        //data annotation to define to display error to the user if not enterd nothing in the field of the username of the product.   
        [Required(ErrorMessage = "Required field")]
        //data annotation to define to display error to the user if  entered string with string over more 10 in the field of the username of the product.   
        [StringLength(10,ErrorMessage = "Maximum length is 10")]
        [Key]
        public string UserName { get; set; }
        //data annotation to define to display error to the user if not enterd nothing in the field of the username of the product.   
        [Required(ErrorMessage = "Required field")]
        public string Password { get; set; }
        //This field used to define the status of user in the store( deleted:false,exist:true).
        public bool mode { get; set; }
    }
}