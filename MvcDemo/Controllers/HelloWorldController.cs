using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDemo.Controllers
{
    public class HelloWorldController : Controller
    {
        // 
        // GET: /HelloWorld/ 
        // I am also adding the code from master branch directly from github let see how git fetch and git pull works
        // Adding one more commit master
        // I am working on feature and check master commit with git fetch
        // continue with amother commit as well..
        //adding here
        //second commit
        //add lightweighted tag
        public ActionResult Index()
        {
            return View();
        }

        // 
        // GET: /HelloWorld/Welcome/ 

        public ActionResult Welcome(string name = "default" ,int ID = 1)
        {
            //return "This is the Welcome action method...";
            ViewBag.name = name;
            ViewBag.ID = ID;
            return View();
        }
    }
}
