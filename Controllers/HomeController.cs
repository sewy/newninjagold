using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace newninjaGold.Controllers
{
    public class HomeController : Controller
    {
        
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("gold") == null)
            {
                HttpContext.Session.SetInt32("gold", 0);
            }
            if(HttpContext.Session.GetString("activties") == null)
            {
               HttpContext.Session.SetString("activties", ""); 
            }
            ViewBag.gold = HttpContext.Session.GetInt32("gold");
            ViewBag.activities = HttpContext.Session.GetString("activties");
            return View();
        }

        [HttpPost]
        [Route("/process_money")]
        public IActionResult money(string building)
        {
            int gold = (int)HttpContext.Session.GetInt32("gold");
            string act = (string)HttpContext.Session.GetString("activties");
            if(building == "farm")
            {
                int RandNum = new Random().Next(10,21);
                gold+= RandNum;
                HttpContext.Session.SetInt32("gold", gold);
                act += "You worked the farm and earned" + RandNum + " gold.";       
            }
            if(building == "cave")
            {
                int RandNum = new Random().Next(5,11);
                gold+= RandNum;
                HttpContext.Session.SetInt32("gold", gold); 
            }
            if(building == "house")
            {
                int RandNum = new Random().Next(2,5);
                gold+= RandNum;
                HttpContext.Session.SetInt32("gold", gold); 
            }
            if(building == "casino")
            {
                int Chance = new Random().Next(0,11);                
                int RandNum = new Random().Next(0,51);
                if(Chance > 6)
                {
                    gold+= RandNum;
                }
                else
                {
                    gold-= RandNum;
                }
                HttpContext.Session.SetInt32("gold", gold); 
            }
            HttpContext.Session.SetString("activties", act);
            return RedirectToAction("Index");
        }

    }
}
