using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        //yapmıs oldugumuz MVC routing sekli sunun gibi olacaktır.  Admintags controller
        //Add de action tarafı
        //http://localhost:154/AdminTags/Add
        public IActionResult Add()
        {
            return View();
        }
    }
}
