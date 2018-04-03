using FinalProject.Dal;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FinalProject.Controllers;
namespace FinalProject.Controllers
{
    
    public class BuyerController : Controller
    {
        //Data access layer that connects the data in the Product model to the  Products table that available in the database.
        ProductsDal dalp = new ProductsDal();
        //Data access layer that connects the data in the Order model to the Orders table that available in the database.
        OrdersDal dalo = new OrdersDal();
        //Deafult redirect to redirect as url: Controller/view
        public ActionResult Index()
        {
            //Return default view(default html).
            return View();
        }
        //This action redirects the user to the buying products page.
        public ActionResult Buy()
        {
            //If have products in the store(products in disabled and enabled mode)
            if (dalp.Products.ToList().Count != 0)
            {
                if(dalp.Products.ToList().Any(p=>p.mode==true))
                //Return view of buying page.
                return View();
                else
                {
                    //Saves the message that will displayed to the user in the view(in the case of not have products in the store.)
                    TempData["msg"] = "No products in the stock";
                    //Redirects the user to the home page by default route.
                    return RedirectToRoute("");
                }
            }
            //If no have product in the store.
            else
            {
                //Saves the message that will displayed to the user in the view(in the case of not have products in the store.)
                TempData["msg"] = "No products in the stock";
                //Redirects the user to the home page by default route.
                return RedirectToRoute("");
            }
        }
 
        //This action returns json of products(use ing angular scope in the form).
        public ActionResult GetProductByJson()
        {
            //Gets list of all the products from the Products table in the database.
            List<Product> products = dalp.Products.ToList();
            //Convert the list to json and return json.
            return Json(products, JsonRequestBehavior.AllowGet);

        }
        //Represents HttpPost action that get the data(details of order) that client send.
        [HttpPost]
        //This action Add order to the Orders table 
        public ActionResult AddOrderToDatabase(Order order)
        {
            //Checks if the data that user send to the server are valid: amount(whole number) and name of the customer(valid by regular expression).
            if(ModelState.IsValidField("namec")&& ModelState.IsValidField("amount"))
            {
                //Gets list of all the product from the Orders Table
                List<Product> products = dalp.Products.ToList();
                foreach(Product product in products)
                {
                    //Check if PID that user entered same to the PID of available in the Orders table in the database
                    if (product.PID == order.PID)
                    {
                        //Checks if have enough items accorsding to the request of the user.
                        if (product.amount - order.amount < 0)
                        {
                            //Saves the message that will displayed to the user in the view(in the case of not have enought items from the specific product that user request in the store.)
                            TempData["msg"] = "There are not enough products in the stock";
                            //Returns view of buying products in the store.
                            return View("Buy");
                        }
                        //Updates the amount in the store(updates the amount of specific product in the store).
                        product.amount = product.amount - order.amount;
                        //Updates the current date of purchase the product by the user.
                            order.date = DateTime.Today;
                        //Marks the order as active in the store.
                            order.mode = true;
                            break;
                        
                    }
                }
               
                //Add order with details of the user to the Orders table
                dalo.Orders.Add(order);
                //Save update of the amount of specific product that user ordered.
                dalp.SaveChanges();
                //Save the adding order according the requst of the user to the Orders table.
                dalo.SaveChanges();
                //Saves the message that will displayed to the user in the view(in the case of  purchase product performed successfully).
                TempData["msg"] = "Buying is successful";
                //Returns view of buying products.
                return View("Buy");

            }
            //If the validation is field( in the case of server validation,for clients that cannot run java script in their webrowser )
            return View("Buy");
        }
        //This action redirects to the adding order to the store.
      public ActionResult CustomerFormBuy(string PID)
        {
            //Initializes new order
            Order order = new Order();
            //Gets list of all the orders in the store
            List<Product> products = dalp.Products.ToList();
            //This loops find the specific product that user interested to purchase.
            foreach(Product product in products)
            {
                //Checks if the PID of the product that user interseted to purchase is the product in the Products table in the database.
                if (product.PID == PID)
                {
                    //Updates the PID of the order to the PID of the product that user intersted to purchase.
                    order.PID = PID;
                    //Updates the name of the product in the order to the name of the product that user intersted to purchase.
                    order.namep = product.namep;
                    //Exit from loop.
                    break;
                }
            }
            //Returns view of adding order in the store with the order with update details of the ordrer without name of the customer and amount from product that user intersted it.
            return View(order);
        }
        ////Represents HttpPost action that get the data(details of search:name of product) that client send.
        [HttpPost]
        //This action returns jsons according to the results of the specific request of the user.
        public ActionResult ShowSearchByName(Product product)
        {
            //Gets list of product that name of products contain the string that user send in request.
            List<Product> products = (from p in dalp.Products where p.namep.Contains(product.namep) select p).ToList();
            //Converts the list of results product to json and return it.
            return Json(products, JsonRequestBehavior.AllowGet);
        }
        ////Represents HttpPost action that get the data(details of search:price range) that client send.
        [HttpPost]
        //This action return json according to the results of the specific request of the user.
        public ActionResult ShowSearchByPrice(ProductInputModel product)
        {
            //Gets list of product that price  of products available in the price range that user send in request.
            List<Product> products = (from p in dalp.Products where p.price>=product.price1&&p.price<=product.price2 select p).ToList();
            //This action return json according to the results of the specific request of the user.
            return Json(products, JsonRequestBehavior.AllowGet);
        }


    }
}