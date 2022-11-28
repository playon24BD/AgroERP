
using ERPBLL.Agriculture.Interface;
using ERPBO.Agriculture.DTOModels;
using ERPWeb.Filters;
using LinqToExcel;
using System.Collections.Generic;
using System.Linq;
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

            //productStockgraph
            var FGProductNameList = _rMStockDashboardGrap.GetMainStockFGProductName(User.OrgId);
            List<string> product = new List<string>();
            List<string> stock = new List<string>();
            foreach(var item in FGProductNameList)
            {
                product.Add(item.FinishGoodProductName.ToString());
            }
            var FGCurrsentQtyList = _rMStockDashboardGrap.GetMainStockFGProductCurrentStock(User.OrgId);
            foreach(var item in FGCurrsentQtyList)
            {
                stock.Add(item.CurrentStock.ToString());
            }
            TempData["FG_NAME"] = string.Join(",", product);
            TempData["CurrentStockF"] = string.Join(",", stock);


            var data = _rMStockDashboardGrap.Last30DaysSellsChart(string.Empty, string.Empty, User.OrgId);
            var days = data.Select(s => s.EntryDate.ToString("dd-MMM-yyyy")).ToArray();
            var Sells = data.Select(s => s.Sells).ToArray();
            TempData["days"] = string.Join(",", days);
            TempData["Sells"] = string.Join(",", Sells);


            //payment
            var data1 = _rMStockDashboardGrap.Last30DaysPaymentChart(string.Empty, string.Empty, User.OrgId);
            var days1 = data1.Select(p => p.PaymentDate.ToString("dd-MMM-yyyy")).ToArray();
            var Sells1 = data1.Select(t => t.Sells).ToArray();
            TempData["days1"] = string.Join(",", days1);
            TempData["Sells1"] = string.Join(",", Sells1);

            return View();


    
        }

       
        
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}