
using ERPWeb.Filters;

using System.Web.Mvc;

namespace ERPWeb.Controllers
{
    [CustomAuthorize]
    public class UserController : BaseController
    {
        // GET: User

        public UserController ()
        {
            
        }

        //public ActionResult GetRequesition(long assemblyId)
        //{
        //    return _requsitionInfoBusiness.GetRequsitionInfosByQuery(null, assemblyId, null, null, null, null, null, null, null, null, null, null, null, null,null);
        //}
        public ActionResult Index(string flag)
        {
            
    
                return View("Index");
    
        }

        
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}