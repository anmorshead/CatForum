using System.Diagnostics;
using CatForum.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatForum.Controllers
{
    public class HomeController : Controller
    {
        //constructor
        public HomeController()
        {

        }

        public IActionResult Index()
        {
            //entity framework: fetch all discussions

            //perform work here
            return View();
        }

        public IActionResult DisplayDiscussion(int id)
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


    }

}
