using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StartProcessByWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            TestProcess();
            return View();
        }

        void TestProcess()
        {
            Process.Start("notepad.exe");
        }

    }
}
