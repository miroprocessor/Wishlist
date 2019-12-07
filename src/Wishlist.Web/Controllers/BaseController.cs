using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wishlist.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected string LoggedUserId
        {
            get
            {
                return Session["user"]?.ToString() ?? "";
            }
        }
    }
}