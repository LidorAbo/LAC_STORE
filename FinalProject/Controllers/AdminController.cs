using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;
using FinalProject.ModelBinders;
using FinalProject.Dal;
using System.Data.SqlClient;
using System.Data;
using FinalProject.ModelView;
using System.Diagnostics;
using System.Data.Entity;

namespace FinalProject.Controllers
{
    public class AdminController : Controller
    {
        //Data access layer that connects the data in the Product model to the  Products table that available in the database.
         ProductsDal dalp = new ProductsDal();
        //Data access layer that connects the data in the User model to the Login table that available in the database.
        LoginDal dall = new LoginDal();
        //Data access layer that connects the data in the Order model to the Orders table that available in the database.
        static OrdersDal dalo = new OrdersDal();
        //class to represents screen of porducts table and input form from the user. 
        ProductViewModel pvm = new ProductViewModel();
        //This action represents the page that user enter to administration panel.
        public ActionResult Login()
        {

            //Returns page of login(to management the store).
            return View();

        }
        //This action represents the page that user enter to adding product to store.
        public ActionResult AddProduct()
        {
            //Returns page of adding product to the store.
            return View();
        }
        //Represents HttpPost action that get the data(username and password) that client send.
        [HttpPost]
        //This action check if the user has administrations permissions if so he redirected to the management page else he redirected to the home page.
        public ActionResult ValidateAsServerLevel(User user)
        {
            //Checks if the form in the view(html page) is valid
            if (ModelState.IsValid)
            {
                //Query that checks if exist user that user entered.
                List<User> users = (from u in dall.Users
                                    where
                                    user.UserName == u.UserName && user.Password == u.Password && u.mode == true
                                    select u).ToList();
                //If available user in the Login table in the database
                if (users.Count == 1)
                {
                    //Checks if  to the user has administrations permissions.
                    if (user.UserName == "Admin" && user.Password == "Admin")
                        // //Returns page of managemnt store(html page).
                        return View("Management");


                }
                //In case if the user doe's not have administartions permissions.
                TempData["msg"] = "Access is denied";
            }
            //Redirects the user to the home page if you do not have appropriate permissions.
            return RedirectToAction("ShowHomePage","Home");

        }
        //Represents HttpPost action that get the data(details of product) that client send.
        [HttpPost]
        //This action add to product to the Products table in the databse in the case it is not available in the store(disabled mode in the Products table in the database or not exist in the database, else(enabled mode in the Products table in the database or exist in this table will a message appear to the user and will redirected to the managament page.
        public ActionResult AddToDatabase(Product product)
        {
            //Checks if the form in the view(html page) is valid: required fields, product id(such as A123), price(float or int such as: 1.4,4) and amount(only integer numbers).
            if (ModelState.IsValid)
            {
                //Checks if the products exist in the store(enable mode in the pid of product that user entered).
                bool exist = dalp.Products.Any(p => p.PID == product.PID && p.mode == true);
                //If the product not exist in the Products table of database or mode of product is false.
                if (!exist.Equals(true))
                {
                    //Gets the list of products that their mode are false(deleted from the store).
                    List<Product> notactive = (from p in dalp.Products where p.PID == product.PID && p.mode == false select p).ToList();
                    //If the mode of specific product that user entered is false.
                    if (notactive.Count != 0)
                    {
                        //Gets the specific product that user entered in the form by PID(key in the Products table in the database). 
                        Product producttou = dalp.Products.Find(product.PID);
                        //Changes mode of product to enable(Product is active in the store).
                        producttou.mode = true;
                        //Changes the name of disabled product to the new name of active product(identify by PID).
                        producttou.namep = product.namep;
                        //Changes the price of disabled product to the new name of active product(identify by PID).
                        producttou.price = product.price;
                        // //Changes the amount of disabled product to the new amount of active product(identify by PID)
                        producttou.amount = product.amount;
                       
                    }
                    //If the product not available in the database add new product and mark it as enabled(active in the store).
                    else
                    {
                        //Changes the mode of product that user entered to enable(active in store)
                        product.mode = true;
                        //Adds the product to the database.
                        dalp.Products.Add(product);
                    }
                    //Saves the new Product object in the databse as enabled(active in the store).
                    dalp.SaveChanges();
                    //Saves the message that will displayed to the user in the view(in the case of the adding product is perforemed successfully).
                    TempData["msg"] = "The product added successfully";
                   //Returns page of adding product to the store.
                    return View("AddProduct");
                }
                //If the product available in the store(marked as enabled)
                else
                {
                    //Saves the message that will displayed to the user in the view(in the case of the adding product is failed).
                    TempData["msg"] = "The product already exist in the store";
                    return View("AddProduct");
                }

            }
            //If the validation is field( in the case of server validation,for clients that cannot run java script in their webrowser )
            return View("Management");

        }
        //This action represents the page that user enter to update product in the store.
        public ActionResult Update()
        {
            //Gets all the product in the store(include disabled products).
            List<Product> products = dalp.Products.ToList<Product>();
            //Initializes new object in the ProductViewModel object in order to view to get data(PID of product and amount of it).
            pvm.product = new Product();
            //Checks if have products in the Products table in the database.
            if(products.Count!=0)
            {
                //If have active orders in the store
                if (products.Any(p => p.mode == true))
                {
                    //Initizializes new list in the ProductViewModel object to display the user the table of products in order to know which product he interseted to update.
                    pvm.products = products;
                    ////Returns page of update product to the store with the ProductViewModel object that allowing it.
                    return View(pvm);
                }
                else
                {
                    //Saves the message that will displayed to the user in the view(in the case of that not products in the store).
                    TempData["msg"] = "There are no products in the store";
                    ////Returns page of Management the store.
                    return View("Management");
                }

            }
            //In the case that not have products in the store.
            else
            {
                //Saves the message that will displayed to the user in the view(in the case of that not products in the store).
                TempData["msg"] = "There are no products in the store";
                ////Returns page of Management the store.
                return View("Management");
            }

        }
        //The action represents the page that the user enter to delete product from the store.
        public ActionResult Delete()
        {
            //Gets all the product in the store(include disabled products).
            List<Product> products = dalp.Products.ToList<Product>();
            // //Initializes new object in the ProductViewModel object in order to view to get data(PID of product and amount of it).
            pvm.product = new Product();
            pvm.products = products;
            //Checks if have products in the Products table in the database.
            if (pvm.products.Count != 0)
            {
                //If have active orders in the store
                if (products.Any(p => p.mode == true))
                    ////Returns page of delete product in the store with the ProductViewModel object that allowing it.
                    return View(pvm);
                else
                {
                    //Saves the message that will displayed to the user in the view(in the case of that not products in the store).
                    TempData["msg"] = "There are no products in the store";
                    ////Returns page of Management the store.
                    return View("Management");
                }
            }

            //In the case that not have products in the store.
            else
            {
                
                //Saves the message that will displayed to the user in the view(in the case of that not products in the store).
                TempData["msg"] = "There are no products in the store";
                ////Returns page of Management the store.
                return View("Management");
            }
        }
        //Represents HttpPost action that get the data(PID of product) that client send and removes it from table products.
        [HttpPost]
        //This action deletes product from the Products table in the database. 
        public ActionResult DeleteFromDatabse()
        {
            //Initializes new Product object in order to get PID from form.
            Product product = new Product();
            //Inserts PID of the product that user entered in the form of delete product.
            product.PID = Request.Form["product.PID"];
            //Checks if the form in the view(html page) is valid: required fields, product id(such as A123).
            if (ModelState.IsValid)
            {
                //Checks if the products exist in the store(enable mode in the pid of product that user entered).
                List<Product> products = dalp.Products.Where(p => p.PID == product.PID && p.mode == true).ToList();
                //Checks if have products in the Products table in the database.
                if (products.Count != 0)
                {
                    //Gets the specific product that user entered in the form by PID(key in the Products table in the database). 
                    Product producttod = dalp.Products.Find(product.PID);
                    //Gets the orders of the product by name of the product.
                    List<Order> orderstod = (from order in dalo.Orders where order.namep == producttod.namep select order).ToList();
                    //Checks if exist orders of product that user interested to delete from the store
                    if(orderstod!=null)
                    {
                        //Deletes the orders of the specific product that user interested to delete 
                        foreach (Order order in orderstod)
                            //Change mode to disabled each order of sprecific product that user interested to delete(delete from the store). 
                            order.mode = false;
                    }
                    //Changes mode of product to disabled(delete from the store).              
                    producttod.mode = false;
                    //Saves changes of delete product in the Products table.
                    dalp.SaveChanges();
                    // //Saves changes of delete orders of product in the Orders table.
                    dalo.SaveChanges();
                    //Initializes new Product Model in ProductViewModel Object to display the user new Delete form to delete other product from the store.
                    pvm.product = new Product();

                }
                //If the user try to delete product that not exist in the store.
                else
                {
                    //Initializes Product Model with data of the user entered(invalid data) to view to user the invalid data that typed in the form.
                    pvm.product = product;
                    //Saves the message that will displayed to the user in the view(in the case of that product not available in the store).
                    TempData["msg"] = "The product not exist in the store";
                }

            }
            //If the form not passed validation( in the case of the user not run javascript in your webrowser).
            else
            {
                //Initializes Product Model with the data that user entered in the form in previous request.
                pvm.product = product;
            }
            //Initializes list of products on order to view to user the products that available in the store.
            pvm.products = dalp.Products.ToList();
            // ////Returns page of delete product from the store.
            return View("Delete", pvm);

        }
        //Represents HttpPost action that get the data(PID of product and amount of it) that client send in the request and update the amount of this product.
        [HttpPost]
        //This action updates amount  of product in the Products table in the database. 
        public ActionResult UpdateDatabase()
        {
            //Initializes new Product object in order to get PID from form.
            Product product = new Product();
            //Inserts PID of the product that user entered in the form of delete product.
            product.PID = Request.Form["product.PID"];
            //Inserts amount of the product that user entered in the form of delete product.
            product.amount = int.Parse(Request.Form["product.amount"]);
            //Checks if the form in the view(html page) is valid: required fields, product id(such as A123), amount(whole number).
            if (ModelState.IsValid)
            {
                //Checks if the products exist in the store(enable mode in the pid of product that user entered).
                List<Product> products = dalp.Products.Where(p => p.PID == product.PID && p.mode == true).ToList();
                //Checks if have products in the Products table in the database.
                if (products.Count != 0)
                {
                    //Gets the specific product that user entered in the form by PID(key in the Products table in the database). 
                    Product producttou = dalp.Products.Find(product.PID);
                    //Changes amount of products to amount that user entered.
                    producttou.amount = product.amount;
                    // //Saves changes of delete orders of product in the Orders table.
                    dalp.SaveChanges();
                    //Initializes new Product Model in ProductViewModel Object to display the user new update form to update other product from the store.
                    pvm.product = new Product();

                }
                //If the user try to update product that not exist in the store.
                else
                {
                    //Initializes Product Model with data of the user entered(invalid data) to view to user the invalid data that typed in the form.
                    pvm.product = product;
                    //Saves the message that will displayed to the user in the view(in the case of that product not available in the store).
                    TempData["msg"] = "The product not exist in the store";
                }

            }
            //If the form not passed validation( in the case of the user not run javascript in your webrowser).
            else
            {
                //Initializes Product Model with the data that user entered in the form in previous request.
                pvm.product = product;
            }
            //Initializes list of products on order to view to user the products that available in the store
            pvm.products = dalp.Products.ToList();
            //Returns page of update product in the store.
            return View("Update", pvm);


        }
        //This action redirects to the page that user can view in your orders in the store.
        public ActionResult ViewOrders()
        {
            //Gets all the product in the store(include disabled products).
            List<Order> orders = dalo.Orders.ToList();
            //If not  have orders in the store(refer to disabled and enabled orders).
            if(orders.Count==0)
            {
                //Saves the message that will displayed to the user in the view(in the case of that not available orders  in the store).
                TempData["msg"] = "There are no orders in the store";
                //Returns page of management  in the store.
                return View("Management");
            }
            //If have orders in the store(enabled and disabled orders)
            else
            {
                //If have active orders in the store
                if( orders.Any(p=>p.mode==true))
                    return View(orders);
                //If no have orders to display to the user.
                else
                {
                    //Saves the message that will displayed to the user in the view(in the case of that not available orders  in the store).
                    TempData["msg"] = "There are no orders in the store";
                    //Retur view of management in the store.
                    return View("Management");
                }

            }
                
            
        }
        //Redirects to view that disaplay to the user the orders in the store
        public ActionResult Management()
        {
            //Return view of management in the store.
            return View();
        }

    }
}
       
     


    
      
    

    
   
        
    
