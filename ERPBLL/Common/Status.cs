using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPBLL.Common
{
    public class RequisitionStatus
    {
        // Never change this fields values //
        public static readonly string Pending = "Pending";
        public static readonly string Rejected = "Rejected";
        public static readonly string Rechecked = "Rechecked";
        public static readonly string Approved = "Approved";
        public static readonly string Accepted = "Accepted";
        public static readonly string Canceled = "Canceled";
        public static readonly string Checked = "Checked";
        public static readonly string HandOver = "HandOver";
        public static readonly string Waiting = "Waiting";
        public static readonly string Current = "Current";
        public static readonly string Void = "Void";
        public static readonly string Return = "Return";
    }
    public class JobSource
    {
        public static readonly string DOA = "DOA";
        public static readonly string Service = "Service";
    }
    public class StockType
    {
        public static readonly string FreshStock = "FreshStock";
        public static readonly string FaultyStock = "FaultyStock";
    }
    public class CustomerSupport
    {
        public static readonly string Handset = "Handset";
        public static readonly string Other = "Other";
    }
    public class ChequeType
    {
        public static readonly string Payment = "Payment";
        public static readonly string Receipt = "Receipt";
    }
    public class QCStatus
    {
        public static readonly string QCPass = "QC-Pass";
        public static readonly string QCFail = "QC-Fail";
    }
    public class JournalType
    {
        public static readonly string Dr = "Dr";
        public static readonly string Cr = "Cr";
    }
    public class AccountsType
    {
        public static readonly string BalanceSheed = "Balance Sheet";
        public static readonly string PNL = "PNL";
    }
    public class JobOrderTransferStatus
    {
        public static readonly string Received = "Received";
        public static readonly string Pending = "Pending";
    }
    public class CallCenterApproval
    {
        public static readonly string Approved = "Approved";
        public static readonly string DisApproved = "DisApproved";
        public static readonly string Pending = "Pending";
    }
    public class InvoiceTypeStatus
    {
        public static readonly string JobOrder = "JobOrder";
        public static readonly string Sells = "Sells";
    }
    public class JobOrderTransferCondition
    {
        public static readonly string Transfer = "Transfer";
        public static readonly string Return = "Return";
    }
    public class ReceiveCondition
    {
        public static readonly string ReceiveJob = "ReceiveJob";
        public static readonly string ReceiveReturnJob = "Receive ReturnJob";
    }
    public class StockStatus
    {
        public static readonly string StockIn = "Stock-In";
        public static readonly string StockOut = "Stock-Out";
        public static readonly string StockReturn = "Stock-Return";
        public static readonly string Transfer = "Transfer";
        public static readonly string PendingEdit = "Pending Edit";
    }
    public class ReturnType
    {
        public static readonly string RepairFaultyReturn = "Repair Faulty Return";
        public static readonly string RepairGoodsReturn = "Repair Goods Return";
        public static readonly string ProductionGoodsReturn = "Production Goods Return";
        public static readonly string ProductionFaultyReturn = "Production Faulty Return";
    }
    public class FaultyCase
    {
        public static readonly string ManMade = "Man Made";
        public static readonly string ChinaMade = "China Made";
    }
    public class ProductionTypes
    {
        public static readonly string CKD = "CKD";
        public static readonly string SKD = "SKD";
        public static readonly string HandSet = "HandSet";
    }
    public class StockOutReason
    {
        public static readonly string StockOutByProductionForProduceGoods = "StockOutByProductionForProduceGoods";
        public static readonly string StockOutByTransferToAssembly = "StockOutByTransferToAssembly";
        public static readonly string StockOutByTransferToQC = "StockOutByTransferToQC";
    }
    public class FinishGoodsSendStatus
    {
        public static readonly string Send = "Send";
        public static readonly string Received = "Received";
    }
    public class UserType
    {
        public static readonly string SystemAdmin = "System Admin";
        public static readonly string Admin = "Admin";
    }
    public class RequisitionType
    {
        public static readonly string CKD = "CKD";
        public static readonly string SKD = "SKD";
    }
    public class Flag
    {
        public static readonly string View = "View";
        public static readonly string Search = "Search";
        public static readonly string Info = "Info";
        public static readonly string Detail = "Detail";
        public static readonly string Report = "Report";
        public static readonly string Delete = "Delete";
        public static readonly string Direct = "Direct";
        public static readonly string Indirect = "Indirect";
    }
    public class RequisitionExecuationType
    {
        public static readonly string Single = "Single";
        public static readonly string Bundle = "Bundle";
    }
    public class JobOrderStatus
    {
        public static readonly string PendingJobOrder = "Pending-JobOrder";
        public static readonly string JobInitiated = "Job-Initiated";
        public static readonly string CustomerDisapproved = "Customer-Disapproved";
        public static readonly string AssignToTS = "TS-Assigned";
        public static readonly string RepairDone = "Repair-Done";
        public static readonly string DeliveryDone = "Delivery-Done";
        public static readonly string HandSetChange = "HandSetChange";
        public static readonly string QCAssigned = "QC-Assigned";
    }
    public class JobOrderTypes
    {
        public static readonly string Warrenty = "Warrenty";
        public static readonly string Billing = "Billing";
        //public static readonly string WarrentyBounce = "Warrenty Bounce";
        //public static readonly string BillingBounce = "Billing Bounce";
    }
    public class PhoneTypes
    {
        public static readonly string Smartphone = "Smart phone";
        public static readonly string Featurephone = "Feature phone";
        public static readonly string Accessories = "Accessories";
        public static readonly string SpareParts = "Spare Parts";
    }
    public class ModelColors
    {
        public static readonly string Red = "Red";
        public static readonly string Blue = "Blue";
        public static readonly string Black = "Black";
        public static readonly string White = "White";
    }

    public class ApplicationType
    {
        public static readonly string ERP = "ERP";
        public static readonly string Service = "Service";
        public static readonly string Accounts = "Accounts";

    }

    public class RepairReason
    {
        public static readonly string SoftwareProblem = "Software Problem";
        public static readonly string HardwareProblem = "Hardware Problem";
        public static readonly string Both = "Both Software & Hardware Problem";
    }
    public class GeneratCode
    {
        public static readonly string EntityCode = DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");
    }
    public class ItemPreparationType
    {
        public static readonly string Production = "Production";
        public static readonly string Packaging = "Packaging";
    }

    public class QRCodeStatus
    {
        public static readonly string Assembly = "Assembly";
        public static readonly string AssemblyRepair = "Assembly-Repair";
        public static readonly string MiniStock = "MiniStock";
        public static readonly string Packaging = "PackagingLine";
        public static readonly string PackagingRepair = "Packaging-Repair";
        public static readonly string Finish = "Finish";
        public static readonly string Carton = "Carton";
        public static readonly string LotIn = "Lot-In";
        public static readonly string Weight = "Weight-Checked";
        public static readonly string Bettery = "Battery-Write";
        public static readonly string IMEIPass = "IMEI-Pass";
    }
    public class CustomerType
    {
        public static readonly string WalkInCustomer = "Walk In Customer";
        public static readonly string Dealer = "Dealer";
    }

    public class StockRetunFlag
    {
        public static readonly string AssemblyLine = "AssemblyLine";
        public static readonly string AssemblyRepair = "Assembly-Repair";
        public static readonly string AssemblyRepairFaulty = "Assembly-Repair-Faulty";
        public static readonly string PackagingLine = "PackagingLine";
        public static readonly string PackagingRepair = "Packaging-Repair";
        public static readonly string PackagingRepairFaulty = "Packaging-Repair-Faulty";
    }
}
