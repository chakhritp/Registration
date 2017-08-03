using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
using System.Web;
using System.Web.Mvc;

namespace WebAPIRegistration.Controllers
{
    public class MemberController : Controller
    {
        //
        // GET: /Member/

        public ActionResult Member()
        {
            return View("Member");
        }
    }
}
