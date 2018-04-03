using System.Web.Mvc;
namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        //redirect to the homepage of the store.
        public ActionResult ShowHomePage()
        {
            //return view of default home page when enter to the site.
            return View();
        }
    }
}