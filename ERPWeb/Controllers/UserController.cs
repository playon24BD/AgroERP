
using ERPBLL.Agriculture.Interface;
using ERPBO.Agriculture.DTOModels;
using ERPWeb.Filters;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ERPWeb.Controllers
{
    [CustomAuthorize]
    public class UserController : BaseController
    {
        // GET: User
        private readonly IRMStockDashboardGrap _rMStockDashboardGrap;

        public UserController (IRMStockDashboardGrap rMStockDashboardGrap)
        {
            this._rMStockDashboardGrap = rMStockDashboardGrap;
        }

        //public ActionResult GetRequesition(long assemblyId)
        //{
        //    return _requsitionInfoBusiness.GetRequsitionInfosByQuery(null, assemblyId, null, null, null, null, null, null, null, null, null, null, null, null,null);
        //}
        public ActionResult Index(string flag)
        {
           // TestEntities db = new TestEntities();
            //var platform_family = (from b in db.EQMlists
            //                       where b.inspection_status == "PASS"
            //                       orderby b.platform_family ascending
            //                       group b by b.platform_family into g
            //                       select new { platform_family = g.Key }).ToList();

            //decimal total = db.EQMlists.Count();
            var RMNameList = _rMStockDashboardGrap.GetMainStockRMName(User.OrgId);

            List<string> platformfamily = new List<string>();
            List<string> CurrentStock = new List<string>();
            foreach (var item in RMNameList)
            {
                

                platformfamily.Add(item.RawMaterialName.ToString());
                //percentages.Add(percentage.ToString("0.00"));
            }
            var RMCurrentStockList = _rMStockDashboardGrap.GetMainStockRMCurrentStock(User.OrgId);
            foreach (var item in RMCurrentStockList)
            {


                //platformfamily.Add(item.RawMaterialName.ToString());
                CurrentStock.Add(item.CurrentStock.ToString());
            }

            TempData["platform_family"] = string.Join(",", platformfamily);
            TempData["CurrentStock"] = string.Join(",", CurrentStock);
            return View();

            //IEnumerable<RawMaterialTrackDTO> RMNameList = _rMStockDashboardGrap.GetMainStockRMName(User.OrgId);
            //ViewBag.RMName = RMNameList;
            //IEnumerable<RawMaterialTrackDTO> RMCurrentStockList = _rMStockDashboardGrap.GetMainStockRMCurrentStock(User.OrgId);
            //ViewBag.RMCurrentStock = RMCurrentStockList;

            //TempData["platform_family"] = string.Join(",", RMNameList);
            //TempData["percentage"] = string.Join(",", RMCurrentStockList);

            //return View();
    
        }

        
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}