using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.MedEmergencyData;
using Mediqura.CommonData.MedBillData;
using Mediqura.CommonData.MedHrData;
using Mediqura.CommonData.MedStore;
using Mediqura.CommonData.OTData;
using Mediqura.CommonData.MedUtilityData;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedLab;
using Mediqura.CommonData.PatientData;
using Mediqura.CommonData.MSBData;
using Mediqura.CommonData.MedAccount;
using Mediqura.CommonData.MedNurseData;
using Mediqura.CommonData.MedHrData;
using Mediqura.CommonData.MedPharData;
using Mediqura.CommonData.LoginData;

namespace Mediqura.Web.MedCommon
{
    public class XmlConvertor
    {
        public static StringBuilder DepositDatatoXML(List<DepositData> ItemList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (DepositData obj in ItemList)
            {
                str.Append("<DepositDetails DepositParticulars= \"" + obj.DepositParticulars.ToString() + "\" ");
                str.Append(" Amount = \"" + obj.Amount.ToString() + "\" ");
                str.Append(" SerialID = \"" + obj.SerialID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder StockTransferRecordDatatoXML(List<StockGRNData> ItemList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (StockGRNData obj in ItemList)
            {
                str.Append("<StocktransferDetails SubStockID= \"" + obj.SubStockID.ToString() + "\" ");
                str.Append(" TransferQty = \"" + obj.TotalQuantity.ToString() + "\" ");
                str.Append(" StockNo = \"" + obj.StockNo.ToString() + "\" ");
                str.Append(" IssueNo = \"" + obj.IssueNo.ToString() + "\" ");
                str.Append(" IndentNo = \"" + obj.IndentNo.ToString() + "\" ");
                str.Append(" MRP = \"" + obj.MRP.ToString() + "\" ");
                str.Append(" CP = \"" + obj.CP.ToString() + "\" ");

                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder AppUtilityRecordDatatoXML(List<ApplicationData> ItemList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (ApplicationData obj in ItemList)
            {
                str.Append("<ApplicationUtilityDetails AppID= \"" + obj.AppID.ToString() + "\" ");
                str.Append(" Status = \"" + obj.Status.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder OTBreakUpRecordDatatoXML(List<OTRegnData> OTBreakUp)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (OTRegnData obj in OTBreakUp)
            {
                str.Append("<OTBreakUp ID = \"" + obj.ID.ToString() + "\" ");
                str.Append(" Amount = \"" + obj.Amount.ToString() + "\" ");
                str.Append(" OTNo = \"" + obj.OTNo.ToString() + "\" ");
                str.Append(" OTemployeeID = \"" + obj.OTemployeeID.ToString() + "\" ");
                str.Append(" RoleID = \"" + obj.RoleID.ToString() + "\" ");
                str.Append(" CaseID = \"" + obj.CaseID.ToString() + "\" ");
                str.Append(" IPNo = \"" + obj.IPNo.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder OpdBillDatatoXML(List<OPDbillingData> ServiceList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (OPDbillingData obj in ServiceList)
            {
                str.Append("<OPDBill ServiceID= \"" + obj.ServiceID.ToString() + "\" ");
                str.Append(" ServiceCharge = \"" + obj.ServiceCharge.ToString() + "\" ");
                str.Append(" SerialID = \"" + obj.SerialID.ToString() + "\" ");
                str.Append(" ServiceTypeID = \"" + obj.ServiceTypeID.ToString() + "\" ");
                str.Append(" SubGroupID = \"" + obj.SubGroupID.ToString() + "\" ");
                str.Append(" DoctorTypeID = \"" + obj.DoctorTypeID.ToString() + "\" ");
                str.Append(" DeptID = \"" + obj.DeptID.ToString() + "\" ");
                str.Append(" DocID = \"" + obj.DocID.ToString() + "\" ");
                str.Append(" Remarks = \"" + obj.Remarks.ToString() + "\" ");
                str.Append(" Quantity = \"" + obj.Quantity.ToString() + "\" ");
                str.Append(" NetServiceCharge = \"" + obj.NetServiceCharge.ToString() + "\" ");
                str.Append(" isDis = \"" + obj.isDis.ToString() + "\" ");
                str.Append(" DisType = \"" + obj.DisType.ToString() + "\" ");
                str.Append(" DisAmount = \"" + obj.DisAmount.ToString() + "\" ");
                str.Append(" DisValue = \"" + obj.DisValue.ToString() + "\" ");
                str.Append(" IsMSBPatient = \"" + obj.isMsbPatient.ToString() + "\" ");
                str.Append(" IsMSBDoctor = \"" + obj.isMsbDoctor.ToString() + "\" ");
                str.Append(" MSBpc = \"" + obj.MsbPc.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder IPdiscountdetailstoXML(List<DiscoutData> ServiceList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (DiscoutData obj in ServiceList)
            {
                str.Append("<Discount Discount= \"" + obj.Discount.ToString() + "\" ");
                str.Append(" DiscountByID = \"" + obj.DiscountByID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder PageDatatoXML(List<RolesData> List)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (RolesData obj in List)
            {
                str.Append("<Page ID= \"" + obj.PageID.ToString() + "\" ");
                str.Append(" PageStatus = \"" + obj.PageStatus.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder DiscountAdjustedlist(List<DoctorPayoutData> ServiceList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (DoctorPayoutData obj in ServiceList)
            {
                str.Append("<Discount DoctorID= \"" + obj.DoctorID.ToString() + "\" ");
                str.Append(" ServiceID = \"" + obj.ServiceID.ToString() + "\" ");
                str.Append(" BillID = \"" + obj.BillID.ToString() + "\" ");
                str.Append(" AdjustementType = \"" + obj.AdjustementType.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder PayableservicetoXML(List<DoctorPayoutData> ServiceList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (DoctorPayoutData obj in ServiceList)
            {
                str.Append("<RefPercentage ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" UHID = \"" + obj.UHID.ToString() + "\" ");
                str.Append(" AddedDate = \"" + obj.AddedDate.ToString() + "\" ");
                str.Append(" BillID = \"" + obj.BillID.ToString() + "\" ");
                str.Append(" BillNo = \"" + obj.BillNo.ToString() + "\" ");
                str.Append(" TotalAmount = \"" + obj.TotalAmount.ToString() + "\" ");
                str.Append(" DiscountAmnt = \"" + obj.DiscountAmnt.ToString() + "\" ");
                str.Append(" NetAmount = \"" + obj.NetAmount.ToString() + "\" ");
                str.Append(" ReferralPC = \"" + obj.ReferralPC.ToString() + "\" ");
                str.Append(" RunnerPC = \"" + obj.RunnerPC.ToString() + "\" ");
                str.Append(" ReferralPayable = \"" + obj.ReferralPayable.ToString() + "\" ");
                str.Append(" RunnerPayable = \"" + obj.RunnerPayable.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder EMRGdiscountdetailstoXML(List<DiscoutData> EmrgDisList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (DiscoutData obj in EmrgDisList)
            {
                str.Append("<Discount Discount= \"" + obj.Discount.ToString() + "\" ");
                str.Append(" DiscountByID = \"" + obj.DiscountByID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder StockDetailsDatatoXML(List<GENStrData> IndentList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (GENStrData obj in IndentList)
            {
                str.Append("<StockItemList ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" SerialID = \"" + obj.SerialID.ToString() + "\" ");
                str.Append(" ReceiptNo = \"" + obj.ReceiptNo.ToString() + "\" ");
                str.Append(" BatchNo = \"" + obj.BatchNo.ToString() + "\" ");
                str.Append(" PONo = \"" + obj.PONo.ToString() + "\" ");
                str.Append(" ReceivedDate = \"" + obj.ReceivedDate.ToString() + "\" ");
                str.Append(" MfgDate = \"" + obj.MfgDate.ToString() + "\" ");
                str.Append(" ExpDate = \"" + obj.ExpDate.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" CompanyID = \"" + obj.CompanyID.ToString() + "\" ");
                str.Append(" SupplierID = \"" + obj.SupplierID.ToString() + "\" ");
                str.Append(" NoOfUnit = \"" + obj.NoOfUnit.ToString() + "\" ");
                str.Append(" QtyperUnit = \"" + obj.QtyperUnit.ToString() + "\" ");
                str.Append(" CPperunit = \"" + obj.CPperunit.ToString() + "\" ");
                str.Append(" TotalCPbeforeTax = \"" + obj.TotalCPbeforeTax.ToString() + "\" ");
                str.Append(" CP = \"" + obj.CP.ToString() + "\" ");
                str.Append(" MRPperunit = \"" + obj.MRPperunit.ToString() + "\" ");
                str.Append(" MRP = \"" + obj.MRP.ToString() + "\" ");
                str.Append(" Tax = \"" + obj.Tax.ToString() + "\" ");
                str.Append(" FreeQuantity = \"" + obj.FreeQuantity.ToString() + "\" ");
                str.Append(" RecvQty = \"" + obj.RecvQty.ToString() + "\" ");
                str.Append(" TotalRecdQty = \"" + obj.TotalRecdQty.ToString() + "\" ");
                str.Append(" RackGroup = \"" + obj.RackGroup.ToString() + "\" ");
                str.Append(" RackSubGroup = \"" + obj.RackSubGroup.ToString() + "\" ");
                str.Append(" RatePerQty = \"" + obj.RatePerQty.ToString() + "\" ");
                str.Append(" DiscountPerQty = \"" + obj.DiscountPerQty.ToString() + "\" ");
                str.Append(" DiscountType = \"" + obj.DiscountType.ToString() + "\" ");
                str.Append(" TotalCPBeforeDisc = \"" + obj.TotalCPBeforeDisc.ToString() + "\" ");
                str.Append(" Temperature = \"" + obj.Temperature.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder StockDetailsDatatoXML(List<StockGRNData> IndentList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (StockGRNData obj in IndentList)
            {
                str.Append("<StockItemList ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" SerialID = \"" + obj.SerialID.ToString() + "\" ");
                str.Append(" ReceiptNo = \"" + obj.ReceiptNo.ToString() + "\" ");
                str.Append(" HSNCode = \"" + obj.HSNCode.ToString() + "\" ");
                str.Append(" BatchNo = \"" + obj.BatchNo.ToString() + "\" ");
                str.Append(" PONo = \"" + obj.PONo.ToString() + "\" ");
                str.Append(" ReceivedDate = \"" + obj.ReceivedDate.ToString("yyyy-MM-dd") + "\" ");
                //str.Append(" MfgDate = \"" + obj.MfgDate.ToString() + "\" ");
                str.Append(" ExpDate = \"" + obj.ExpDate.ToString("yyyy-MM-dd") + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" CompanyID = \"" + obj.CompanyID.ToString() + "\" ");
                str.Append(" SupplierID = \"" + obj.SupplierID.ToString() + "\" ");
                str.Append(" NoOfUnit = \"" + obj.NoOfUnit.ToString() + "\" ");
                str.Append(" QtyperUnit = \"" + obj.QtyperUnit.ToString() + "\" ");
                str.Append(" CPperunit = \"" + obj.CPperunit.ToString() + "\" ");
                str.Append(" CP = \"" + obj.CP.ToString() + "\" ");
                str.Append(" MRPperunit = \"" + obj.MRPperunit.ToString() + "\" ");
                str.Append(" MRP = \"" + obj.MRP.ToString() + "\" ");
                str.Append(" FreeQuantity = \"" + obj.FreeQuantity.ToString() + "\" ");
                str.Append(" RecvQty = \"" + obj.RecvQty.ToString() + "\" ");
                str.Append(" TotalRecdQty = \"" + obj.TotalRecdQty.ToString() + "\" ");
                str.Append(" TaxableAmount = \"" + obj.TaxableAmount.ToString() + "\" ");
                str.Append(" CGST = \"" + obj.CGST.ToString() + "\" ");
                str.Append(" IGST = \"" + obj.IGST.ToString() + "\" ");
                str.Append(" SGST = \"" + obj.SGST.ToString() + "\" ");
                str.Append(" CPafterTax = \"" + obj.CPafterTax.ToString() + "\" ");
                str.Append(" Temperature = \"" + obj.Temperature.ToString() + "\" ");
                str.Append(" Discount = \"" + obj.Discount.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }

        public static StringBuilder ReturnStockDetailsDatatoXML(List<StockGRNData> IndentList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (StockGRNData obj in IndentList)
            {
                str.Append("<StockReturnList StockID= \"" + obj.StockID.ToString() + "\" ");
                str.Append(" SerialID = \"" + obj.SerialID.ToString() + "\" ");
                str.Append(" ReceiptNo = \"" + obj.ReceiptNo.ToString() + "\" ");
                str.Append(" BatchNo = \"" + obj.BatchNo.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" CompanyID = \"" + obj.CompanyID.ToString() + "\" ");
                str.Append(" SupplierID = \"" + obj.SupplierID.ToString() + "\" ");
                str.Append(" ReturnQty = \"" + obj.ReturnQty.ToString() + "\" ");
                str.Append(" TaxableAmount = \"" + obj.TaxableAmount.ToString() + "\" ");
                str.Append(" CGST = \"" + obj.CGST.ToString() + "\" ");
                str.Append(" IGST = \"" + obj.IGST.ToString() + "\" ");
                str.Append(" SGST = \"" + obj.SGST.ToString() + "\" ");
                str.Append(" CPafterTax = \"" + obj.CPafterTax.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }

        public static StringBuilder IndentDetailsDatatoXML(List<IndentToMainData> IndentList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (IndentToMainData obj in IndentList)
            {
                str.Append("<IndentList ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" SerialID = \"" + obj.SerialID.ToString() + "\" ");
                str.Append(" ItemId = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" StockIID = \"" + obj.StockID.ToString() + "\" ");
                str.Append(" BatchNo = \"" + obj.BatchNo.ToString() + "\" ");
                str.Append(" Balstock = \"" + obj.BalStock.ToString() + "\" ");
                str.Append(" ReqdQty = \"" + obj.ReqdQty.ToString() + "\" ");
                str.Append(" NoUnit = \"" + obj.NoOfUnit.ToString() + "\" ");
                str.Append(" SubStockID = \"" + obj.SubStockID.ToString() + "\" ");
                str.Append(" IndentRequestID = \"" + obj.IndentRequestID.ToString() + "\" ");
                str.Append(" IndentRaiseDate = \"" + obj.IndentRaiseDate.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder GEN_IndentDetailsDatatoXML(List<GenIndentData> IndentList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (GenIndentData obj in IndentList)
            {
                str.Append("<IndentList ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" SerialID = \"" + obj.SerialID.ToString() + "\" ");
                str.Append(" ItemId = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" ReqdQty = \"" + obj.ReqdQty.ToString() + "\" ");
                str.Append(" GenStockID = \"" + obj.GenStockID.ToString() + "\" ");
                str.Append(" IndentRequestID = \"" + obj.IndentRequestID.ToString() + "\" ");
                str.Append(" IndentRaiseDate = \"" + obj.IndentRaiseDate.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder Med_IndentDetailsDatatoXML(List<MedIndentData> IndentList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (MedIndentData obj in IndentList)
            {
                str.Append("<IndentList ID= \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" SerialID = \"" + obj.SerialID.ToString() + "\" ");
                str.Append(" ItemId = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" ReqdQty = \"" + obj.ReqdQty.ToString() + "\" ");
                str.Append(" MedSubStockID = \"" + obj.MedSubStockID.ToString() + "\" ");
                str.Append(" IndentRequestID = \"" + obj.IndentRequestID.ToString() + "\" ");
                str.Append(" BatchNo = \"" + obj.BatchNo.ToString() + "\" ");
                str.Append(" SupplierID = \"" + obj.SupplierID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder Med_ShorttDetailsDatatoXML(List<MedIndentData> IndentList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (MedIndentData obj in IndentList)
            {
                str.Append("<ShortList ID= \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" SerialID = \"" + obj.SerialID.ToString() + "\" ");
                str.Append(" ItemId = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" ReqdQty = \"" + obj.ReqdQty.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder IndentDataPhrDatatoXML(List<IPDrugIndentData> IndentList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (IPDrugIndentData obj in IndentList)
            {
                str.Append("<IndentList ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" SerialID = \"" + obj.SerialID.ToString() + "\" ");
                str.Append(" ItemId = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" StockID = \"" + obj.StockID.ToString() + "\" ");
                str.Append(" BatchNo = \"" + obj.BatchNo.ToString() + "\" ");
                str.Append(" Balstock = \"" + obj.BalStock.ToString() + "\" ");
                str.Append(" ReqdQty = \"" + obj.ReqdQty.ToString() + "\" ");
                str.Append(" SubStockID = \"" + obj.SubStockID.ToString() + "\" ");
                str.Append(" IndentRequestID = \"" + obj.IndentRequestID.ToString() + "\" ");
                //str.Append(" IndentRaiseDate = \"" + obj.IndentRaiseDate.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder IndentRecordDatatoXML(List<IndentRecvHandOverData> Indent)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (IndentRecvHandOverData obj in Indent)
            {
                str.Append("<Indent ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" IndentNo = \"" + obj.IndentNo.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" IdentID = \"" + obj.IndentID.ToString() + "\" ");
                str.Append(" StockID = \"" + obj.StockID.ToString() + "\" ");
                str.Append(" BalStock = \"" + obj.BalStock.ToString() + "\" ");
                str.Append(" reqdQty = \"" + obj.ReqdQty.ToString() + "\" ");
                str.Append(" ApprvQty = \"" + obj.apprvQty.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder PHR_IndentRecordDatatoXML(List<IPDrugIndentData> IndentPHR)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (IPDrugIndentData obj in IndentPHR)
            {
                str.Append("<IndentPHR ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" IndentNo = \"" + obj.IndentNo.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" IdentID = \"" + obj.IndentID.ToString() + "\" ");
                str.Append(" StockID = \"" + obj.StockID.ToString() + "\" ");
                str.Append(" BalStock = \"" + obj.BalStock.ToString() + "\" ");
                str.Append(" reqdQty = \"" + obj.ReqdQty.ToString() + "\" ");
                str.Append(" ApprvQty = \"" + obj.apprvQty.ToString() + "\" ");
                str.Append(" IPNo = \"" + obj.IPNo.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder HandoverRecordDatatoXML(List<IndentRecvHandOverData> Handover)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (IndentRecvHandOverData obj in Handover)
            {
                str.Append("<Handover ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" IndentNo = \"" + obj.IndentNo.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" IdentID = \"" + obj.IndentID.ToString() + "\" ");
                str.Append(" StockID = \"" + obj.StockID.ToString() + "\" ");
                str.Append(" BalStock = \"" + obj.BalStock.ToString() + "\" ");
                str.Append(" ReqdQty = \"" + obj.ReqdQty.ToString() + "\" ");
                str.Append(" apprvQty = \"" + obj.apprvQty.ToString() + "\" ");
                str.Append(" HndQty = \"" + obj.HndQty.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder ReceivedRecordDatatoXML(List<IndentRecvHandOverData> Received)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (IndentRecvHandOverData obj in Received)
            {
                str.Append("<Received ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" IndentNo = \"" + obj.IndentNo.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" IdentID = \"" + obj.IndentID.ToString() + "\" ");
                str.Append(" StockID = \"" + obj.StockID.ToString() + "\" ");
                str.Append(" BalStock = \"" + obj.BalStock.ToString() + "\" ");
                str.Append(" ReqdQty = \"" + obj.ReqdQty.ToString() + "\" ");
                str.Append(" apprvQty = \"" + obj.apprvQty.ToString() + "\" ");
                str.Append(" HndQty = \"" + obj.HndQty.ToString() + "\" ");
                str.Append(" RecvQty = \"" + obj.RecvQty.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder IPReceivedRecordDatatoXML(List<IPDrugIndentData> Received)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (IPDrugIndentData obj in Received)
            {
                str.Append("<Received ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" IndentNo = \"" + obj.IndentNo.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" IdentID = \"" + obj.IndentID.ToString() + "\" ");
                str.Append(" StockID = \"" + obj.StockID.ToString() + "\" ");
                str.Append(" BalStock = \"" + obj.BalStock.ToString() + "\" ");
                str.Append(" ReqdQty = \"" + obj.ReqdQty.ToString() + "\" ");
                str.Append(" apprvQty = \"" + obj.apprvQty.ToString() + "\" ");
                str.Append(" HndQty = \"" + obj.HndQty.ToString() + "\" ");
                str.Append(" RecvQty = \"" + obj.RecvQty.ToString() + "\" ");
                str.Append(" IPNo = \"" + obj.IPNo.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder BedDatatoXML(List<AdmissionData> ServiceList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (AdmissionData obj in ServiceList)
            {
                str.Append("<Bedtrancefer BedID= \"" + obj.BedID.ToString() + "\" ");
                str.Append(" Room = \"" + obj.Room.ToString() + "\" ");
                str.Append(" BedNo = \"" + obj.BedNo.ToString() + "\" ");
                str.Append(" Charges = \"" + obj.Charges.ToString() + "\" ");
                str.Append(" LastBedCharge = \"" + obj.LastBedCharges.ToString() + "\" ");
                str.Append(" LastEntryDate = \"" + obj.AssignedDate.ToString() + "\" ");
                str.Append(" LastWardId = \"" + obj.WardID.ToString() + "\" ");
                str.Append(" LastBedId = \"" + obj.LastBedId.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder NurseStationRecordDatatoXML(List<NursingStationData> nursingList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (NursingStationData obj in nursingList)
            {
                str.Append("<NurseAssignWardDetails NurseID= \"" + obj.NurseID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder DesignationRecordDatatoXML(List<GradeDesgnData> nursingList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (GradeDesgnData obj in nursingList)
            {
                str.Append("<DesignationDetails DesignID= \"" + obj.DesignID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder Gen_IndentApprovedQtyRecordDatatoXML(List<GenIndentData> Indent)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (GenIndentData obj in Indent)
            {
                str.Append("<Indent ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" IndentNo = \"" + obj.IndentNo.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" StockNo = \"" + obj.StockNo.ToString() + "\" ");
                str.Append(" BalStock = \"" + obj.BalStock.ToString() + "\" ");
                str.Append(" ApprovedQty = \"" + obj.ApprovedQty.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder GEN_DeptStockDatatoXML(List<GenIndentData> DeptStock)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (GenIndentData obj in DeptStock)
            {
                str.Append("<DeptStock SubStockID = \"" + obj.SubStockID.ToString() + "\" ");
                str.Append(" StockNo = \"" + obj.StockNo.ToString() + "\" ");
                str.Append(" CondemnQty = \"" + obj.CondemnQty.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder MedEmployeetoXML(List<MedStockEmployeeData> MedStock)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (MedStockEmployeeData obj in MedStock)
            {
                str.Append("<MedEmployee EmployeeID = \"" + obj.EmployeeID.ToString() + "\" ");
                str.Append(" MedSubStockID = \"" + obj.MedSubStockID.ToString() + "\" ");
                str.Append(" MedItemRequestEnable = \"" + obj.MedItemRequestEnable.ToString() + "\" ");
                str.Append(" MedItemVerifyEnable = \"" + obj.MedItemVerifyEnable.ToString() + "\" ");
                str.Append(" MedItemApproveEnable = \"" + obj.MedItemApproveEnable.ToString() + "\" ");
                str.Append(" MedItemHandoverEnable = \"" + obj.MedItemHandoverEnable.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder GEN_IndentDetailsDeptWiseDatatoXML(List<GenDeptWiseUsedItemData> IndentList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (GenDeptWiseUsedItemData obj in IndentList)
            {
                str.Append("<GenDeptWiseList ID= \"" + obj.SubStockID.ToString() + "\" ");
                str.Append(" ItemId = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" QtyUsed = \"" + obj.QtyUsed.ToString() + "\" ");
                str.Append(" Remarks = \"" + obj.Remarks.ToString() + "\" ");
                str.Append(" PatientName = \"" + obj.PatientName.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder ServiceMappingRecordDatatoXML(List<ServiceMappingMasterData> serviceList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (ServiceMappingMasterData obj in serviceList)
            {
                str.Append("<ServiceMappingDetails ServiceID= \"" + obj.ServiceID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder AdmBedDatatoXML(List<AdmissionData> ServiceList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (AdmissionData obj in ServiceList)
            {
                str.Append("<BedAdmission BedID= \"" + obj.BedID.ToString() + "\" ");
                str.Append(" Room = \"" + obj.Room.ToString() + "\" ");
                str.Append(" BedNo = \"" + obj.BedNo.ToString() + "\" ");
                str.Append(" Charges = \"" + obj.Charges.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder IPCreditDatatoXML(List<IPCreditMasterData> ServiceList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (IPCreditMasterData obj in ServiceList)
            {
                str.Append("<IPCreditLimit IPNo= \"" + obj.IPNo.ToString() + "\" ");
                str.Append(" PatientName = \"" + obj.PatientName.ToString() + "\" ");
                str.Append(" PatientAddress = \"" + obj.PatientAddress.ToString() + "\" ");
                str.Append(" DepositAmount = \"" + obj.DepositAmount.ToString() + "\" ");
                str.Append(" TotalOutstandingBill = \"" + obj.TotalOutstandingBill.ToString() + "\" ");
                str.Append(" CreditLimit = \"" + obj.CreditLimit.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder EmployeetypetoXML(List<EmployeeTypeData> Employee)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (EmployeeTypeData obj in Employee)
            {
                str.Append("<EmployeeType EmplyeetypeID= \"" + obj.EmplyeetypeID.ToString() + "\" ");
                str.Append(" IsMSB = \"" + obj.IsMSB.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder OTSchedulingRecordDatatoXML(List<OTSchedulingData> SchedulingList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (OTSchedulingData obj in SchedulingList)
            {
                str.Append("<OTSchedulingList UHID= \"" + obj.UHID.ToString() + "\" ");
                str.Append(" OTNo = \"" + obj.OTNo.ToString() + "\" ");
                str.Append(" OTDate = \"" + obj.Date.ToString() + "\" ");
                str.Append(" OTStartTime = \"" + obj.OTStartTime.ToString() + "\" ");
                str.Append(" OTEndTime = \"" + obj.OTEndTime.ToString() + "\" ");
                str.Append(" PatientName = \"" + obj.PatientName.ToString() + "\" ");
                str.Append(" WardBedName = \"" + obj.WardBedName.ToString() + "\" ");
                str.Append(" OperationName = \"" + obj.OperationName.ToString() + "\" ");
                str.Append(" DoctorID = \"" + obj.DoctorID.ToString() + "\" ");
                str.Append(" Surgeon = \"" + obj.Surgeon.ToString() + "\" ");
                str.Append(" TheatreID = \"" + obj.TheatreID.ToString() + "\" ");
                str.Append(" AnaesthtistID = \"" + obj.AnaesthetistID.ToString() + "\" ");
                str.Append(" OTStatusID = \"" + obj.OTStatusID.ToString() + "\" ");
                str.Append(" Remark = \"" + obj.Remark.ToString() + "\" ");

                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder GEN_StockReturnDatatoXML(List<GenIndentData> RequestList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (GenIndentData obj in RequestList)
            {
                str.Append("<Returnlist SubStockID = \"" + obj.SubStockID.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" ReturnQty = \"" + obj.ReturnQty.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder Med_StockReturnDatatoXML(List<MedIndentData> Returnlist)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (MedIndentData obj in Returnlist)
            {
                str.Append("<Returnlist SubStockID = \"" + obj.SubStockID.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" ReturnQty = \"" + obj.ReturnQty.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder EmrgBedDatatoXML(List<EmrgAdmissionData> EmrgList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (EmrgAdmissionData obj in EmrgList)
            {
                str.Append("<BedAdmission BedID= \"" + obj.BedID.ToString() + "\" ");
                str.Append(" Room = \"" + obj.Room.ToString() + "\" ");
                str.Append(" BedNo = \"" + obj.BedNo.ToString() + "\" ");
                str.Append(" Charges = \"" + obj.Charges.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }

        public static StringBuilder GVInsuranceReceivableRecordDatatoXML(List<InsuranceReceivableData> EmrgList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (InsuranceReceivableData obj in EmrgList)
            {
                str.Append("<InsuranceReceivable ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" PatCat = \"" + obj.PatientCategoryID.ToString() + "\" ");
                str.Append(" PatSubcat = \"" + obj.SubCategoryID.ToString() + "\" ");
                str.Append(" ServiceType = \"" + obj.ServiceTypeID.ToString() + "\" ");
                str.Append(" BillNo = \"" + obj.BillNo.ToString() + "\" ");
                str.Append(" UHID = \"" + obj.UHID.ToString() + "\" ");
                str.Append(" PatName = \"" + obj.PatientName.ToString() + "\" ");
                str.Append(" DiscountAmount = \"" + obj.DiscountAmount.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder ItemLocationRecordDatatoXML(List<RackMasterData> EmrgList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (RackMasterData obj in EmrgList)
            {
                str.Append("<ItemLocation StockID= \"" + obj.StockID.ToString() + "\" ");
                str.Append(" RackID = \"" + obj.RackID.ToString() + "\" ");
                str.Append(" SubRackID = \"" + obj.SubRackID.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" ItemLocation = \"" + obj.ItemLocation.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder TransferedBedDatatoXML(List<AdmissionData> ServiceList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (AdmissionData obj in ServiceList)
            {
                str.Append("<LastBedDetails BedID= \"" + obj.BedID.ToString() + "\" ");
                str.Append(" IsReleased = \"" + obj.IsReleased.ToString() + "\" ");
                str.Append(" Patient_Active = \"" + obj.Patient_Active.ToString() + "\" ");

                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder BedAssignedDatatoXML(List<AdmissionData> ServiceList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (AdmissionData obj in ServiceList)
            {
                str.Append("<BedCurrentDetails BedID= \"" + obj.BedID.ToString() + "\" ");
                str.Append(" Room = \"" + obj.Room.ToString() + "\" ");
                str.Append(" BedNo = \"" + obj.BedNo.ToString() + "\" ");
                str.Append(" Charges = \"" + obj.Charges.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder OpdLabBillDatatoXML(List<LabBillingData> LabServiceList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (LabBillingData obj in LabServiceList)
            {
                String filtertestname = null;
                if (obj.TestName.ToString().Contains("&"))
                {
                    filtertestname = obj.TestName.ToString().Replace("&", "&amp;");
                }
                str.Append("<OPDLabBill LabServiceID= \"" + obj.LabServiceID.ToString() + "\" ");
                str.Append(" LabServiceCharge = \"" + obj.LabServiceCharge.ToString() + "\" ");
                str.Append(" SerialID = \"" + obj.SerialID.ToString() + "\" ");
                str.Append(" PackageID = \"" + obj.PackageID.ToString() + "\" ");
                str.Append(" Quantity = \"" + obj.Quantity.ToString() + "\" ");
                str.Append(" DoctorTypeID = \"" + obj.DoctorTypeID.ToString() + "\" ");
                str.Append(" DeptID = \"" + obj.DeptID.ToString() + "\" ");
                str.Append(" DocID = \"" + obj.DocID.ToString() + "\" ");
                str.Append(" TestCenterID = \"" + obj.TestCenterID.ToString() + "\" ");
                str.Append(" UrgencyID = \"" + obj.UrgencyID.ToString() + "\" ");
                str.Append(" NetLabServiceCharge = \"" + obj.NetLabServiceCharge.ToString() + "\" ");
                str.Append(" Remarks = \"" + obj.Remarks.ToString() + "\" ");
                str.Append(" isDis = \"" + obj.isDis.ToString() + "\" ");
                str.Append(" DisType = \"" + obj.DisType.ToString() + "\" ");
                str.Append(" DisAmount = \"" + obj.DisAmount.ToString() + "\" ");
                str.Append(" DisValue = \"" + obj.DisValue.ToString() + "\" ");
                //       str.Append(" TestName = \"" + obj.TestName.ToString() + "\" ");
                str.Append(" TestName = \"" + filtertestname + "\" ");
                str.Append(" IsMSBPatient = \"" + obj.isMsbPatient.ToString() + "\" ");
                str.Append(" IsMSBDoctor = \"" + obj.isMsbDoctor.ToString() + "\" ");
                str.Append(" MSBpc = \"" + obj.MsbPc.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder Dueresponsiblemployee(List<EmployeeData> emplist)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (EmployeeData obj in emplist)
            {
                str.Append("<Respemployee EmployeeID= \"" + obj.EmployeeID.ToString() + "\" ");
                str.Append(" Amount= \"" + obj.Amount.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder IPServiceRecordDatatoXML(List<IPServiceRecordData> IPServiceList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (IPServiceRecordData obj in IPServiceList)
            {
                str.Append("<IPServiceRecord ServiceID= \"" + obj.ServiceID.ToString() + "\" ");
                str.Append(" ServiceCharge = \"" + obj.ServiceCharge.ToString() + "\" ");
                str.Append(" SerialID = \"" + obj.SerialID.ToString() + "\" ");
                str.Append(" Quantity = \"" + obj.Quantity.ToString() + "\" ");
                str.Append(" NetServiceCharge = \"" + obj.NetServiceCharge.ToString() + "\" ");
                str.Append(" DoctorID = \"" + obj.DoctorID.ToString() + "\" ");
                str.Append(" ServiceCategoryID = \"" + obj.ServiceCategoryID.ToString() + "\" ");
                str.Append(" ServiceTypeID = \"" + obj.ServiceTypeID.ToString() + "\" ");
                str.Append(" SubGroupID = \"" + obj.SubGroupID.ToString() + "\" ");
                str.Append(" TestCenterID = \"" + obj.TestCenterID.ToString() + "\" ");
                str.Append(" UrgencyID = \"" + obj.UrgencyID.ToString() + "\" ");
                str.Append(" IsMSBPatient = \"" + obj.isMsbPatient.ToString() + "\" ");
                str.Append(" IsMSBDoctor = \"" + obj.isMsbDoctor.ToString() + "\" ");
                str.Append(" MSBpc = \"" + obj.MsbPc.ToString() + "\" ");
                str.Append(" Remarks = \"" + obj.Remarks.ToString() + "\" ");
                str.Append(" ServiceDate = \"" + obj.ServiceDate.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder ServiceRecorddDatatoXML(List<BillAdjustmentData> IPServiceList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (BillAdjustmentData obj in IPServiceList)
            {
                str.Append("<ServiceRecord ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" ServiceID = \"" + obj.ServiceID.ToString() + "\" ");
                str.Append(" ServiceCharge = \"" + obj.ServiceCharge.ToString() + "\" ");
                str.Append(" Quantity = \"" + obj.Quantity.ToString() + "\" ");
                str.Append(" NetServiceCharge = \"" + obj.NetServiceCharge.ToString() + "\" ");
                str.Append(" Remarks = \"" + obj.Remarks.ToString() + "\" ");
                str.Append(" ServiceStartDate = \"" + obj.ServiceStartDate.ToString() + "\" ");
                str.Append(" ServiceEndDate = \"" + obj.ServiceEndDate.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder EMRGServiceRecordDatatoXML(List<EmrgAdmissionData> EMRGServiceList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (EmrgAdmissionData obj in EMRGServiceList)
            {
                str.Append("<EMRGServiceRecord ServiceID= \"" + obj.ServiceID.ToString() + "\" ");
                str.Append(" ServiceCharge = \"" + obj.ServiceCharge.ToString() + "\" ");
                str.Append(" SerialID = \"" + obj.SerialID.ToString() + "\" ");
                str.Append(" Quantity = \"" + obj.Quantity.ToString() + "\" ");
                str.Append(" NetServiceCharge = \"" + obj.NetServiceCharge.ToString() + "\" ");
                str.Append(" DoctorID = \"" + obj.DocID.ToString() + "\" ");
                str.Append(" ServiceCategoryID = \"" + obj.ServiceCategoryID.ToString() + "\" ");
                str.Append(" ServiceTypeID = \"" + obj.ServiceTypeID.ToString() + "\" ");
                str.Append(" SubGroupID = \"" + obj.SubGroupID.ToString() + "\" ");
                str.Append(" TestCenterID = \"" + obj.TestCenterID.ToString() + "\" ");
                str.Append(" UrgencyID = \"" + obj.UrgencyID.ToString() + "\" ");
                str.Append(" IsMSBPatient = \"" + obj.isMsbPatient.ToString() + "\" ");
                str.Append(" IsMSBDoctor = \"" + obj.isMsbDoctor.ToString() + "\" ");
                str.Append(" Remarks = \"" + obj.Remarks.ToString() + "\" ");
                str.Append(" MSBpc = \"" + obj.MsbPc.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder IPIssueServiceRecordDatatoXML(List<IPServiceRecordData> IPServiceList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (IPServiceRecordData obj in IPServiceList)
            {
                str.Append("<IPServiceRecord ServiceID= \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" ServiceCharge = \"" + obj.ServiceCharge.ToString() + "\" ");
                str.Append(" SerialID = \"" + obj.SerialID.ToString() + "\" ");
                str.Append(" DoctorID = \"" + obj.DoctorID.ToString() + "\" ");
                str.Append(" Quantity = \"" + obj.Quantity.ToString() + "\" ");
                str.Append(" NetServiceCharge = \"" + obj.NetServiceCharge.ToString() + "\" ");
                str.Append(" SubStockID = \"" + obj.SubStockID.ToString() + "\" ");

                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder ControllisttoXML(List<PageControlsData> ControlList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (PageControlsData obj in ControlList)
            {
                str.Append("<ControlList SaveEnable= \"" + obj.SaveEnable.ToString() + "\" ");
                str.Append(" UpdateEnable = \"" + obj.UpdateEnable.ToString() + "\" ");
                str.Append(" SitemapID = \"" + obj.SitemapID.ToString() + "\" ");
                str.Append(" EditEnable = \"" + obj.EditEnable.ToString() + "\" ");
                str.Append(" DeleteEnable = \"" + obj.DeleteEnable.ToString() + "\" ");
                str.Append(" SearchEnable = \"" + obj.SearchEnable.ToString() + "\" ");
                str.Append(" PrintEnable = \"" + obj.PrintEnable.ToString() + "\" ");
                str.Append(" ExportEnable = \"" + obj.ExportEnable.ToString() + "\" ");
                str.Append(" AmountEnable = \"" + obj.AmountEnable.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder StockIssueDetailsDatatoXML(List<StockIssueData> IndentList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (StockIssueData obj in IndentList)
            {
                str.Append("<StockIssuedList ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" StockID = \"" + obj.StockID.ToString() + "\" ");
                str.Append(" SerialID = \"" + obj.SerialID.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" IssueQuantity = \"" + obj.IssueQuantity.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder StockReturnDetailsDatatoXML(List<StockReturnData> IndentList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (StockReturnData obj in IndentList)
            {
                str.Append("<StockReturnList StockID= \"" + obj.StockID.ToString() + "\" ");
                str.Append(" SubStockID = \"" + obj.SubStockID.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" ReturnQty = \"" + obj.Return.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder AppmntSchdXML(List<AppoinmentScheduleData> AppmntSchd)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (AppoinmentScheduleData obj in AppmntSchd)
            {

                str.Append("<Appoinment SchdID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" MorningSch = \"" + obj.Morning.ToString() + "\" ");
                str.Append(" MorningSlot = \"" + obj.MorningSlots.ToString() + "\" ");
                str.Append(" EveningSch = \"" + obj.Evening.ToString() + "\" ");
                str.Append(" EveningSlot = \"" + obj.EveningSlots.ToString() + "\" ");
                str.Append(" AfternoonSch = \"" + obj.Afternoon.ToString() + "\" ");
                str.Append(" AfternoonSlot = \"" + obj.AfternoonSlots.ToString() + "\" ");
                str.Append(" Availibility = \"" + obj.Availibility.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder FileDatatoXML(List<UploadFileData> fileUpload)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (UploadFileData obj in fileUpload)
            {

                str.Append("<Upload IPno= \"" + obj.IPNo.ToString() + "\" ");
                //str.Append("<Upload Title= \"" + obj.Tittle.ToString() + "\" ");
                str.Append(" Title = \"" + obj.Tittle.ToString() + "\" ");
                str.Append(" Filename = \"" + obj.FileName.ToString() + "\" ");
                str.Append(" path = \"" + obj.FilePath.ToString() + "\" ");
                str.Append(" Contenttype = \"" + obj.ContentType.ToString() + "\" ");
                //str.Append(" Content = \"" + obj.PdfDocument.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder RoasterDatatoXML(List<NurseRoasterData> NurRoaster)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (NurseRoasterData obj in NurRoaster)
            {

                str.Append("<Roaster tb_ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" NurID = \"" + obj.nurseID.ToString() + "\" ");
                str.Append(" Nurname = \"" + obj.NurseName.ToString() + "\" ");
                str.Append(" day1 = \"" + obj.Day_I.ToString() + "\" ");
                str.Append(" day2 = \"" + obj.Day_II.ToString() + "\" ");
                str.Append(" day3 = \"" + obj.Day_III.ToString() + "\" ");
                str.Append(" day4 = \"" + obj.Day_IV.ToString() + "\" ");
                str.Append(" day5 = \"" + obj.Day_V.ToString() + "\" ");
                str.Append(" day6 = \"" + obj.Day_VI.ToString() + "\" ");
                str.Append(" day7 = \"" + obj.Day_VII.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }

        public static StringBuilder IPLabServiceRecordDatatoXML(List<LabBillingData> IPServiceList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (LabBillingData obj in IPServiceList)
            {
                str.Append("<IPLabServiceRecord ServiceID= \"" + obj.LabServiceID.ToString() + "\" ");
                str.Append(" ServiceCharge = \"" + obj.LabServiceCharge.ToString() + "\" ");
                str.Append(" SerialID = \"" + obj.SerialID.ToString() + "\" ");
                str.Append(" Quantity = \"" + obj.Quantity.ToString() + "\" ");
                str.Append(" DocID = \"" + obj.DocID.ToString() + "\" ");
                str.Append(" NetServiceCharge = \"" + obj.NetLabServiceCharge.ToString() + "\" ");
                str.Append(" TestCenterID = \"" + obj.TestCenterID.ToString() + "\" ");
                str.Append(" UrgencyID = \"" + obj.UrgencyID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder ICDCodeRecordDatatoXML(List<ICDData> ItemList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (ICDData obj in ItemList)
            {
                str.Append("<ICDCodeList ID= \"" + obj.ICDCodeID.ToString() + "\" ");
                str.Append(" ICDCode= \"" + obj.ICDCode.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder OpdPhrBillDatatoXML(List<PHRbillingData> LabServiceList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (PHRbillingData obj in LabServiceList)
            {
                str.Append("<OPDPhrBill ItemID= \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" Charge = \"" + obj.Charge.ToString() + "\" ");
                str.Append(" SerialID = \"" + obj.SerialID.ToString() + "\" ");
                str.Append(" Quantity = \"" + obj.Quantity.ToString() + "\" ");
                str.Append(" NetCharges = \"" + obj.NetCharges.ToString() + "\" ");
                str.Append(" Tax = \"" + obj.Tax.ToString() + "\" ");
                str.Append(" SubStockID = \"" + obj.SubStockID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder StockCondemnedDatatoXML(List<StockGRNData> CondemndedList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (StockGRNData obj in CondemndedList)
            {
                str.Append("<CondemnedQty ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" TotalCondemned = \"" + obj.TotalCondemned.ToString() + "\" ");

                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder StockCondemnedDatatoXML(List<GENStrData> CondemndedList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (GENStrData obj in CondemndedList)
            {
                str.Append("<CondemnedQty ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" TotalCondemned = \"" + obj.TotalCondemned.ToString() + "\" ");

                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder OPReturnDetailsDatatoXML(List<OPReturnData> IndentList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (OPReturnData obj in IndentList)
            {
                str.Append("<OPReturnList ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" UHID = \"" + obj.UHID.ToString() + "\" ");
                str.Append(" SubStockID = \"" + obj.SubStockID.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" Charges = \"" + obj.Charges.ToString() + "\" ");
                str.Append(" ItemWiseDiscount = \"" + obj.ItemWiseDiscount.ToString() + "\" ");
                str.Append(" ReturnQty = \"" + obj.Return.ToString() + "\" ");
                str.Append(" NetCharges = \"" + obj.NetCharges.ToString() + "\" ");
                str.Append(" ReturnAmt = \"" + obj.ReturnAmt.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        //ip return item phrm
        public static StringBuilder IPReturnDetailsDatatoXML(List<IPReturnData> objlst)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (IPReturnData obj in objlst)
            {
                str.Append("<IPReturnList ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" IPDrgIssueNo = \"" + obj.IPDrgIssueNo.ToString() + "\" ");
                str.Append(" UHID = \"" + obj.UHID.ToString() + "\" ");
                str.Append(" SubStockID = \"" + obj.SubStockID.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" Unit = \"" + obj.Unit.ToString() + "\" ");
                str.Append(" Quantity = \"" + obj.Quantity.ToString() + "\" ");
                str.Append(" ReturnQty = \"" + obj.Return.ToString() + "\" ");
                str.Append(" MRPperQty = \"" + obj.MRPperQty.ToString() + "\" ");
                str.Append(" ReturnAmt = \"" + obj.ReturnAmt.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        //Emg return item
        public static StringBuilder EmergReturnDetailsDatatoXML(List<EmgReturnData> objlst)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (EmgReturnData obj in objlst)
            {
                str.Append("<EmergReturnList ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" EmergDrgIssueNo = \"" + obj.EmergDrgIssueNo.ToString() + "\" ");
                str.Append(" UHID = \"" + obj.UHID.ToString() + "\" ");
                str.Append(" SubStockID = \"" + obj.SubStockID.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" Unit = \"" + obj.Unit.ToString() + "\" ");
                str.Append(" Quantity = \"" + obj.Quantity.ToString() + "\" ");
                str.Append(" ReturnQty = \"" + obj.Return.ToString() + "\" ");
                str.Append(" MRPperQty = \"" + obj.MRPperQty.ToString() + "\" ");
                str.Append(" ReturnAmt = \"" + obj.ReturnAmt.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder SampleDatatoXML(List<SampleCollectionData> LabSampleCollection)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (SampleCollectionData obj in LabSampleCollection)
            {
                str.Append("<SampleCollection ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" TestCenterID = \"" + obj.TestCenterID.ToString() + "\" ");
                str.Append(" LabServiceID = \"" + obj.LabServiceID.ToString() + "\" ");
                str.Append(" IsOutsourcedTest = \"" + obj.IsOutsourcedTest.ToString() + "\" ");
                str.Append(" CollectionStatus = \"" + obj.CollectionStatus.ToString() + "\" ");
                str.Append(" Investigationumber = \"" + obj.Investigationumber.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder IPSampleDatatoXML(List<SampleCollectionData> LabSampleCollection)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (SampleCollectionData obj in LabSampleCollection)
            {
                str.Append("<SampleCollection SampleID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" SampleTypeID = \"" + obj.SampleTypeID.ToString() + "\" ");
                str.Append(" Status = \"" + obj.StatusID.ToString() + "\" ");
                str.Append(" LabServiceID = \"" + obj.LabServiceID.ToString() + "\" ");
                str.Append(" OrderID = \"" + obj.OrderID.ToString() + "\" ");
                str.Append(" UrgencyID = \"" + obj.UrgencyID.ToString() + "\" ");
                str.Append(" Name = \"" + obj.PatientName.ToString() + "\" ");
                str.Append(" PatientTypeID = \"" + obj.PatientTypeID.ToString() + "\" ");
                str.Append(" ReferalDoctor = \"" + obj.ReferalDoctor.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder EMRGSampleDatatoXML(List<EmrgAdmissionData> EmrgSampleCollection)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (EmrgAdmissionData obj in EmrgSampleCollection)
            {
                str.Append("<EmrgSampCollection SampleID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" SampleTypeID = \"" + obj.SampleTypeID.ToString() + "\" ");
                str.Append(" StatusID = \"" + obj.StatusID.ToString() + "\" ");
                str.Append(" LabServiceID = \"" + obj.LabServiceID.ToString() + "\" ");
                str.Append(" EmrgID = \"" + obj.EmrgID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder LabResultEntryDatatoXML(List<LabResultData> LabSampleCollection)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (LabResultData obj in LabSampleCollection)
            {
                str.Append("<ResultEntry LabServiceID= \"" + obj.LabServiceID.ToString() + "\" ");
                str.Append(" Unit = \"" + obj.UnitID.ToString() + "\" ");
                str.Append(" MethodID = \"" + obj.MethodID.ToString() + "\" ");
                str.Append(" MachineID = \"" + obj.MachineID.ToString() + "\" ");
                str.Append(" IsNormal = \"" + obj.IsNormal.ToString() + "\" ");
                str.Append(" ReagentID = \"" + obj.ReagentID.ToString() + "\" ");
                str.Append(" ParameterID = \"" + obj.ParameterID.ToString() + "\" ");
                str.Append(" LabResultValue = \"" + obj.LabResultValue.ToString() + "\" ");
                str.Append(" UHID = \"" + obj.UHID.ToString() + "\" ");
                str.Append(" Investigationumber = \"" + obj.Investigationumber.ToString() + "\" ");
                str.Append(" ParmRemarks = \"" + obj.ParmRemarks.ToString() + "\" ");
                str.Append(" Ranges = \"" + obj.Ranges.ToString() + "\" ");
                str.Append(" RowType = \"" + obj.RowType.ToString() + "\" ");
                str.Append(" OPno = \"" + obj.OPno.ToString() + "\" ");
                str.Append(" IPno = \"" + obj.IPno.ToString() + "\" ");
                str.Append(" Emergno = \"" + obj.Emergno.ToString() + "\" ");
                str.Append(" PatientTypeID = \"" + obj.PatientTypeID.ToString() + "\" ");
                str.Append(" UrgencyID = \"" + obj.UrgencyID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }

        public static StringBuilder SampleIPDDatatoXML(List<SampleCollectionData> LabSampleCollection)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (SampleCollectionData obj in LabSampleCollection)
            {
                str.Append("<SampleCollection SampleID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" UHID = \"" + obj.UHID.ToString() + "\" ");
                str.Append(" SampleType = \"" + obj.SampleTypeID.ToString() + "\" ");
                str.Append(" Status = \"" + obj.StatusID.ToString() + "\" ");
                str.Append(" LabServiceID = \"" + obj.LabServiceID.ToString() + "\" ");
                str.Append(" Name = \"" + obj.PatientName.ToString() + "\" ");
                str.Append(" PatientTypeID = \"" + obj.PatientTypeID.ToString() + "\" ");
                str.Append(" ReferalDoctor = \"" + obj.ReferalDoctor.ToString() + "\" ");
                str.Append(" OrderID = \"" + obj.OrderID.ToString() + "\" ");

                str.Append(" />");
            }
            return str;
        }

        public static StringBuilder OtregistrationtoXML(List<OTRegnData> otregd)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (OTRegnData obj in otregd)
            {
                str.Append("<OTRGD OTemployeeID= \"" + obj.OTemployeeID.ToString() + "\" ");
                str.Append(" OTemployeeTypeID = \"" + obj.OTemployeeTypeID.ToString() + "\" ");
                str.Append(" OTroleID = \"" + obj.OTroleID.ToString() + "\" ");
                str.Append(" IsMainSurgeon = \"" + obj.IsMainSurgeon.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder OtproceduretoXML(List<OTRegnData> otregd)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (OTRegnData obj in otregd)
            {
                str.Append("<procedure CaseID= \"" + obj.CaseID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder OtanaesthesiatoXML(List<OTRegnData> otregd)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (OTRegnData obj in otregd)
            {
                str.Append("<Anaesthesia AnaesthesiaType= \"" + obj.AnaesthesiaType.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder StockStatustoXML(List<StockStatusData> Stock)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (StockStatusData obj in Stock)
            {
                str.Append("<StockStatus StockID= \"" + obj.StockID.ToString() + "\" ");
                str.Append(" TotalCondemnQty = \"" + obj.TotalCondemnQty.ToString() + "\" ");
                str.Append(" SubStockID = \"" + obj.SubStockID.ToString() + "\" ");
                str.Append(" StockTypeID = \"" + obj.StockTypeID.ToString() + "\" ");
                str.Append(" ExpireDates = \"" + obj.ExpireDates.ToString() + "\" ");
                str.Append(" RecivedDates = \"" + obj.RecivedDates.ToString() + "\" ");
                str.Append(" BalStock = \"" + obj.BalStock.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder MedStockStatustoXML(List<StockStatusData> Stock)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (StockStatusData obj in Stock)
            {
                str.Append("<Stock StockID= \"" + obj.StockID.ToString() + "\" ");
                str.Append(" NoOfUnit = \"" + obj.NoOfUnit.ToString() + "\" ");
                str.Append(" FreeQty = \"" + obj.FreeQty.ToString() + "\" ");
                str.Append(" CPPerUnit = \"" + obj.CPPerUnit.ToString() + "\" ");
                str.Append(" Discount = \"" + obj.Discount.ToString() + "\" ");
                str.Append(" SGST = \"" + obj.SGST.ToString() + "\" ");
                str.Append(" CGST = \"" + obj.CGST.ToString() + "\" ");
                str.Append(" MRPperunit = \"" + obj.MRPperunit.ToString() + "\" ");
                str.Append(" TotalIsssuedQty = \"" + obj.TotalIsssuedQty.ToString() + "\" ");
                str.Append(" TotalCondmQty = \"" + obj.TotalCondmQty.ToString() + "\" ");
                str.Append(" TotalVendorReturnQty = \"" + obj.TotalVendorReturnQty.ToString() + "\" ");
                str.Append(" TotalUnitBalance = \"" + obj.TotalUnitBalance.ToString() + "\" ");
                str.Append(" RecievedDates = \"" + obj.RecivedDates.ToString() + "\" ");
                str.Append(" ExpiryDates = \"" + obj.ExpireDates.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder MedSubStockStatustoXML(List<StockStatusData> Stock)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (StockStatusData obj in Stock)
            {
                str.Append("<Stock StockID= \"" + obj.StockID.ToString() + "\" ");
                str.Append(" AvailableBalAfterUsed = \"" + obj.AvailableBalAfterUsed.ToString() + "\" ");
                str.Append(" TotalQuantity = \"" + obj.TotalQuantity.ToString() + "\" ");
                str.Append(" ExpiryDates = \"" + obj.ExpireDates.ToString() + "\" ");
                str.Append(" Numpoint = \"" + obj.Numpoint.ToString() + "\" ");
                str.Append(" Numright = \"" + obj.Numright.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder MedSaleStockDatatoXML(List<MedOPDSalesData> Stock)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (MedOPDSalesData obj in Stock)
            {
                str.Append("<Stock ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" SubStockID = \"" + obj.SubStockID.ToString() + "\" ");
                str.Append(" SerialID = \"" + obj.SerialID.ToString() + "\" ");
                str.Append(" TransactionID = \"" + obj.TransactionID.ToString() + "\" ");
                str.Append(" NoUnit = \"" + obj.NoUnit.ToString() + "\" ");
                str.Append(" Quantity = \"" + obj.Quantity.ToString() + "\" ");
                str.Append(" MRPperQty = \"" + obj.MRPperQty.ToString() + "\" ");
                str.Append(" NetCharge = \"" + obj.NetCharge.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" BatchNo = \"" + obj.BatchNo.ToString() + "\" ");
                str.Append(" ItemName = \"" + obj.ItemName.ToString() + "\" ");

                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder OTStatustoXML(List<OTStatusData> OTstate)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (OTStatusData obj in OTstate)
            {
                str.Append("<OTStatus IPNo= \"" + obj.IPNo.ToString() + "\" ");
                str.Append(" OT_status = \"" + obj.Otstatus.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder IndentStatustoXML(List<IndentToMainData> Indentstate)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (IndentToMainData obj in Indentstate)
            {
                str.Append("<IndentStatus IndentNo= \"" + obj.IndentNo.ToString() + "\" ");
                str.Append(" StatusID = \"" + obj.StatusID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder LabServiceRecordDatatoXML(List<LabBillingData> IPServiceList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (LabBillingData obj in IPServiceList)
            {
                str.Append("<ServiceRecord LabServiceID= \"" + obj.LabServiceID.ToString() + "\" ");
                str.Append(" BillNo = \"" + obj.BillNo.ToString() + "\" ");
                str.Append(" TestName = \"" + obj.TestName.ToString() + "\" ");
                str.Append(" SerialID = \"" + obj.SerialID.ToString() + "\" ");
                str.Append(" NetServiceCharge = \"" + obj.NetLabServiceCharge.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder StockistItemRecordDatatoXML(List<StockGRNData> IndentList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (StockGRNData obj in IndentList)
            {
                str.Append("<StockistReturnList ID= \"" + obj.ReturnID.ToString() + "\" ");
                str.Append(" StockID = \"" + obj.StockID.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" TotalReturnQty = \"" + obj.TotalRecdQty.ToString() + "\" ");
                str.Append(" CPPerQty = \"" + obj.CPPerQty.ToString() + "\" ");
                str.Append(" NetCPPerQty = \"" + obj.CP.ToString() + "\" ");
                str.Append(" SupplierID = \"" + obj.SupplierID.ToString() + "\" ");
                str.Append(" Tax = \"" + obj.Tax.ToString() + "\" ");
                str.Append(" PO = \"" + obj.PONo.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }

        public static StringBuilder StockistItemRecordDatatoXML(List<GENStrData> IndentList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (GENStrData obj in IndentList)
            {
                str.Append("<StockistReturnList ID= \"" + obj.ReturnID.ToString() + "\" ");
                str.Append(" StockID = \"" + obj.StockID.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" TotalReturnQty = \"" + obj.TotalRecdQty.ToString() + "\" ");
                str.Append(" CPPerQty = \"" + obj.CPPerQty.ToString() + "\" ");
                str.Append(" NetCPPerQty = \"" + obj.CP.ToString() + "\" ");
                str.Append(" SupplierID = \"" + obj.SupplierID.ToString() + "\" ");
                str.Append(" Tax = \"" + obj.Tax.ToString() + "\" ");
                str.Append(" PO = \"" + obj.PONo.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder ItemCheckListRecordDatatoXML(List<GENStrData> IndentList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (GENStrData obj in IndentList)
            {
                str.Append("<ItemCheckList ID= \"" + obj.ReturnID.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" SupplierID = \"" + obj.SupplierID.ToString() + "\" ");
                str.Append(" TotalCondemned = \"" + obj.TotalCondemned.ToString() + "\" ");
                str.Append(" BalStock = \"" + obj.BalStock.ToString() + "\" ");
                str.Append(" CPPerQty = \"" + obj.CPPerQty.ToString() + "\" ");
                str.Append(" TotalCP = \"" + obj.CP.ToString() + "\" ");
                str.Append(" Tax = \"" + obj.Tax.ToString() + "\" ");
                str.Append(" TotalQuantity = \"" + obj.TotalQuantity.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder ItemCheckListRecordDatatoXML(List<StockGRNData> IndentList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (StockGRNData obj in IndentList)
            {
                str.Append("<ItemCheckList ID= \"" + obj.ReturnID.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" SupplierID = \"" + obj.SupplierID.ToString() + "\" ");
                str.Append(" TotalCondemned = \"" + obj.TotalCondemned.ToString() + "\" ");
                str.Append(" BalStock = \"" + obj.BalStock.ToString() + "\" ");
                str.Append(" CPPerQty = \"" + obj.CPPerQty.ToString() + "\" ");
                str.Append(" TotalCP = \"" + obj.CP.ToString() + "\" ");
                str.Append(" Tax = \"" + obj.Tax.ToString() + "\" ");
                str.Append(" TotalQuantity = \"" + obj.TotalQuantity.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder POCrossCheckRecordDatatoXML(List<StockGRNData> IndentList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (StockGRNData obj in IndentList)
            {
                str.Append("<ItemCrossCheckList ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" SupplierID = \"" + obj.SupplierID.ToString() + "\" ");
                str.Append(" TotalOrderedQty = \"" + obj.OrderedQty.ToString() + "\" ");
                str.Append(" TotalRecdQty = \"" + obj.RecvQty.ToString() + "\" ");
                str.Append(" CP = \"" + obj.CP.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }

        public static StringBuilder DoctorWiseCollectionDatatoXML(List<DoctorWiseCollectionMasterData> collectionList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (DoctorWiseCollectionMasterData obj in collectionList)
            {
                str.Append("<DOCTORCOMMISSION BillID= \"" + obj.BillID.ToString() + "\" ");
                str.Append(" BillNo = \"" + obj.BillNo.ToString() + "\" ");
                str.Append(" UHID = \"" + obj.UHID.ToString() + "\" ");
                str.Append(" ServiceID = \"" + obj.ServiceID.ToString() + "\" ");
                str.Append(" Doctortype = \"" + obj.Doctortype.ToString() + "\" ");
                str.Append(" DepartmentID = \"" + obj.DepartmentID.ToString() + "\" ");
                str.Append(" DoctorID = \"" + obj.DoctorID.ToString() + "\" ");
                str.Append(" Servicetype = \"" + obj.Servicetype.ToString() + "\" ");
                str.Append(" ServiceCharge = \"" + obj.ServiceCharge.ToString() + "\" ");
                str.Append(" Commission = \"" + obj.Commission.ToString() + "\" ");
                str.Append(" Tax = \"" + obj.Tax.ToString() + "\" ");
                str.Append(" DoctorPayable = \"" + obj.DoctorPayable.ToString() + "\" ");
                str.Append(" Hospitalcharge = \"" + obj.Hospitalcharge.ToString() + "\" ");
                str.Append(" AddedBy = \"" + obj.AddedByID.ToString() + "\" ");
                str.Append(" LastVisitDate = \"" + obj.LastVisitDate.ToString() + "\" ");
                str.Append(" HospitalID = \"" + obj.HospitalID.ToString() + "\" ");
                str.Append(" FinancialYearID = \"" + obj.FinancialYearID.ToString() + "\" ");
                str.Append(" verifyBy = \"" + obj.verifyBy.ToString() + "\" ");

                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder ItemPurchaseOrderRecordDatatoXML(List<GENStrData> IndentList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (GENStrData obj in IndentList)
            {
                str.Append("<ItemPurchaseOrder ID= \"" + obj.ReturnID.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" SupplierID = \"" + obj.SupplierID.ToString() + "\" ");
                str.Append(" ApprovalListNo = \"" + obj.ApprovalListNo.ToString() + "\" ");
                str.Append(" BalStock = \"" + obj.BalStock.ToString() + "\" ");
                str.Append(" CPPerQty = \"" + obj.CPPerQty.ToString() + "\" ");
                str.Append(" TotalCP = \"" + obj.CP.ToString() + "\" ");
                str.Append(" Tax = \"" + obj.Tax.ToString() + "\" ");
                str.Append(" TotalQuantity = \"" + obj.TotalQuantity.ToString() + "\" ");
                str.Append(" ApprovedQuantity = \"" + obj.ApprovedQty.ToString() + "\" ");

                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder ItemPurchaseOrderRecordDatatoXML(List<StockGRNData> IndentList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (StockGRNData obj in IndentList)
            {
                str.Append("<ItemPurchaseOrder ID= \"" + obj.ReturnID.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" SupplierID = \"" + obj.SupplierID.ToString() + "\" ");
                str.Append(" ApprovalListNo = \"" + obj.ApprovalListNo.ToString() + "\" ");
                str.Append(" BalStock = \"" + obj.BalStock.ToString() + "\" ");
                str.Append(" CPPerQty = \"" + obj.CPPerQty.ToString() + "\" ");
                str.Append(" TotalCP = \"" + obj.CP.ToString() + "\" ");
                str.Append(" Tax = \"" + obj.Tax.ToString() + "\" ");
                str.Append(" TotalQuantity = \"" + obj.TotalQuantity.ToString() + "\" ");
                str.Append(" ApprovedQuantity = \"" + obj.ApprovedQty.ToString() + "\" ");

                str.Append(" />");
            }
            return str;
        }


        public static StringBuilder EmpFiletoXML(List<EmpFileData> fileUpload)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (EmpFileData obj in fileUpload)
            {

                str.Append("<Upload EmpID= \"" + obj.EmployeeID.ToString() + "\" ");
                //str.Append("<Upload Title= \"" + obj.Tittle.ToString() + "\" ");
                str.Append(" Title = \"" + obj.Tittle.ToString() + "\" ");
                str.Append(" Filename = \"" + obj.FileName.ToString() + "\" ");
                str.Append(" path = \"" + obj.FilePath.ToString() + "\" ");
                str.Append(" Contenttype = \"" + obj.ContentType.ToString() + "\" ");
                str.Append(" docID = \"" + obj.docID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder EmployeeDependentRecordDatatoXML(List<EmployeeData> IndentList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (EmployeeData obj in IndentList)
            {
                str.Append("<EmployeeDependent ID= \"" + obj.DependentID.ToString() + "\" ");
                str.Append(" DependentName = \"" + obj.DependentName.ToString() + "\" ");
                str.Append(" Relation = \"" + obj.Relation.ToString() + "\" ");
                str.Append(" Gender = \"" + obj.Gender.ToString() + "\" ");

                str.Append(" DOB = \"" + obj.DOB.ToString() + "\" ");
                str.Append(" Age = \"" + obj.Age.ToString() + "\" ");

                str.Append(" AppDt = \"" + obj.AppDt.ToString() + "\" ");
                str.Append(" IssueDt = \"" + obj.IssueDt.ToString() + "\" ");
                str.Append(" ValidDt = \"" + obj.ValidDt.ToString() + "\" ");
                str.Append(" SurDt = \"" + obj.SurDt.ToString() + "\" ");


                str.Append(" DiscountIPD = \"" + obj.DiscountIPD.ToString() + "\" ");
                str.Append(" DiscountOPD = \"" + obj.DiscountOPD.ToString() + "\" ");
                str.Append(" DiscountOPDLab = \"" + obj.DiscountOPDLab.ToString() + "\" ");

                str.Append(" />");
            }
            return str;
        }

        public static StringBuilder EmployeeDiscountRecordDatatoXML(List<EmployeeData> IndentList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (EmployeeData obj in IndentList)
            {
                str.Append("<EmployeeDiscount EmployeeID= \"" + obj.EmployeeID.ToString() + "\" ");
                str.Append(" DiscountIPD = \"" + obj.DiscountIPD.ToString() + "\" ");
                str.Append(" DiscountOPD = \"" + obj.DiscountOPD.ToString() + "\" ");
                str.Append(" DiscountOPDLab = \"" + obj.DiscountOPDLab.ToString() + "\" ");

                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder LabSubTestRecordDatatoXML(List<LabServiceMasterData> StockItemList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (LabServiceMasterData obj in StockItemList)
            {
                str.Append("<LabSubTest ID= \"" + obj.ID.ToString() + "\" ");
                //str.Append(" Code = \"" + obj.Code.ToString() + "\" ");
                str.Append(" OrderNo = \"" + obj.OrderNo.ToString() + "\" ");
                str.Append(" Unit = \"" + obj.UnitID.ToString() + "\" ");
                str.Append(" Sample = \"" + obj.SampleTypeID.ToString() + "\" ");
                str.Append(" Machine = \"" + obj.MachineID.ToString() + "\" ");
                str.Append(" ContainerID = \"" + obj.ContainerID.ToString() + "\" ");
                str.Append(" SubTestName = \"" + obj.SubTestName.ToString() + "\" ");
                str.Append(" AgeRangeFrom = \"" + obj.AgeRangeFrom.ToString() + "\" ");
                str.Append(" AgeRangeTo = \"" + obj.AgeRangeTo.ToString() + "\" ");
                str.Append(" NormalRangeMaleFrom = \"" + obj.NormalRangeMaleFrom.ToString() + "\" ");
                str.Append(" NormalRangeMaleTo = \"" + obj.NormalRangeMaleTo.ToString() + "\" ");
                str.Append(" NormalRangeFeMaleFrom = \"" + obj.NormalRangeFeMaleFrom.ToString() + "\" ");
                str.Append(" NormalRangeFeMaleTo = \"" + obj.NormalRangeFeMaleTo.ToString() + "\" ");
                str.Append(" NormalRangeTransFeMaleFrom = \"" + obj.NormalRangeTransFeMaleFrom.ToString() + "\" ");
                str.Append(" NormalRangeTransFeMaleTo = \"" + obj.NormalRangeTransFeMaleTo.ToString() + "\" ");
                str.Append(" NormalRangeTransMaleFrom = \"" + obj.NormalRangeTransMaleFrom.ToString() + "\" ");
                str.Append(" NormalRangeTransMaleTo = \"" + obj.NormalRangeTransMaleTo.ToString() + "\" ");
                str.Append(" RowTypeID = \"" + obj.RowTypeID.ToString() + "\" ");
                str.Append(" Reagent = \"" + obj.ReagentTypeID.ToString() + "\" ");
                str.Append(" DefaultValue = \"" + obj.defaultValue.ToString() + "\" ");
                str.Append(" Method = \"" + obj.MethodID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder OPDEmergencyRecordDatatoXML(List<OPDEmergencyPayOutData> LabSampleCollection)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (OPDEmergencyPayOutData obj in LabSampleCollection)
            {
                str.Append("<OPDEmergencyPayOut DoctorID= \"" + obj.DoctorID.ToString() + "\" ");
                str.Append(" ServiceID = \"" + obj.ServiceID.ToString() + "\" ");
                str.Append(" ServiceCharge = \"" + obj.ServiceCharge.ToString() + "\" ");
                str.Append(" ShareType = \"" + obj.ShareType.ToString() + "\" ");
                str.Append(" HospitalShare = \"" + obj.HospitalShare.ToString() + "\" ");
                str.Append(" ConsultantShare = \"" + obj.ConsultantShare.ToString() + "\" ");
                str.Append(" Activate = \"" + obj.Activate.ToString() + "\" ");
                str.Append(" GroupID = \"" + obj.GroupID.ToString() + "\" ");
                str.Append(" SubGroupID = \"" + obj.SubGroupID.ToString() + "\" ");
                str.Append(" Validfrom = \"" + obj.Validfrom.ToString() + "\" ");
                str.Append(" Validto = \"" + obj.Validto.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder RadiologyLabRecordDatatoXML(List<RadilogyLabServiceMSTData> LabSampleCollection)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (RadilogyLabServiceMSTData obj in LabSampleCollection)
            {
                str.Append("<RadiologyLabPayOut ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" ServiceID = \"" + obj.ServiceID.ToString() + "\" ");
                str.Append(" GroupID = \"" + obj.GroupID.ToString() + "\" ");
                str.Append(" SubGroupID = \"" + obj.SubGroupID.ToString() + "\" ");
                str.Append(" ServiceCharge = \"" + obj.ServiceCharge.ToString() + "\" ");
                str.Append(" ShareType = \"" + obj.ShareType.ToString() + "\" ");
                str.Append(" HospitalShare = \"" + obj.HospitalShare.ToString() + "\" ");
                str.Append(" Reportingshare = \"" + obj.Reportingshare.ToString() + "\" ");
                str.Append(" ConsultantShare = \"" + obj.ConsultantShare.ToString() + "\" ");
                str.Append(" Activate = \"" + obj.Activate.ToString() + "\" ");
                str.Append(" TestcenterID = \"" + obj.TestcenterID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder MinorOTRecordDatatoXML(List<MinorOTServicesPayOutData> LabSampleCollection)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (MinorOTServicesPayOutData obj in LabSampleCollection)
            {
                str.Append("<MinorOTPayOut ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" ServiceID = \"" + obj.ServiceID.ToString() + "\" ");
                str.Append(" ServiceCode = \"" + obj.ServiceCode.ToString() + "\" ");
                str.Append(" ServiceCharge = \"" + obj.ServiceCharge.ToString() + "\" ");
                str.Append(" ShareType = \"" + obj.ShareType.ToString() + "\" ");
                str.Append(" HospitalShare = \"" + obj.HospitalShare.ToString() + "\" ");
                str.Append(" ConsultantShare = \"" + obj.ConsultantShare.ToString() + "\" ");
                str.Append(" CorpusFund = \"" + obj.CorpusFund.ToString() + "\" ");
                str.Append(" StaffFund = \"" + obj.StaffFund.ToString() + "\" ");
                str.Append(" Activate = \"" + obj.Activate.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder MajorOTRecordDatatoXML(List<MinorOTServicesPayOutData> LabSampleCollection)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (MinorOTServicesPayOutData obj in LabSampleCollection)
            {
                str.Append("<MajorOTPayOut ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" ServiceID = \"" + obj.ServiceID.ToString() + "\" ");
                str.Append(" GroupID = \"" + obj.GroupID.ToString() + "\" ");
                str.Append(" SubGroupID = \"" + obj.SubGroupID.ToString() + "\" ");
                str.Append(" ServiceCode = \"" + obj.ServiceCode.ToString() + "\" ");
                str.Append(" ServiceCharge = \"" + obj.ServiceCharge.ToString() + "\" ");
                str.Append(" ShareType = \"" + obj.ShareType.ToString() + "\" ");
                str.Append(" ConsultantShare = \"" + obj.ConsultantShare.ToString() + "\" ");
                str.Append(" CorpusFund = \"" + obj.CorpusFund.ToString() + "\" ");
                str.Append(" StaffFund = \"" + obj.StaffFund.ToString() + "\" ");
                str.Append(" Surgeon1 = \"" + obj.Surgeon1.ToString() + "\" ");
                str.Append(" Surgeon2 = \"" + obj.Surgeon2.ToString() + "\" ");
                str.Append(" Surgeon3 = \"" + obj.Surgeon3.ToString() + "\" ");
                str.Append(" AnaesthesiaShare = \"" + obj.AnaesthesiaShare.ToString() + "\" ");
                str.Append(" OTConsumableShare = \"" + obj.OTConsumableShare.ToString() + "\" ");
                str.Append(" InstrumentUsedShare = \"" + obj.InstrumentUsedShare.ToString() + "\" ");
                str.Append(" AssistantShare = \"" + obj.AssistantShare.ToString() + "\" ");
                str.Append(" Activate = \"" + obj.Activate.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder AccountMappingCollectionDatatoXML(List<AccountMappingMasterData> collectionList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (AccountMappingMasterData obj in collectionList)
            {
                str.Append("<ACCOUNTMAPPING ServiceType= \"" + obj.ServiceType.ToString() + "\" ");
                str.Append(" ServiceID = \"" + obj.ServiceID.ToString() + "\" ");
                str.Append(" SubServiceType = \"" + obj.SubServiceType.ToString() + "\" ");
                str.Append(" MappingType = \"" + obj.MappingType.ToString() + "\" ");
                str.Append(" GroupType = \"" + obj.GroupType.ToString() + "\" ");
                str.Append(" DebitID = \"" + obj.DebitID.ToString() + "\" ");
                str.Append(" CreditID = \"" + obj.CreditID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder AccountTransactionCredittoXML(List<AccountTransactionData> collectionList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (AccountTransactionData obj in collectionList)
            {
                str.Append("<CREDITACCOUNT CreditID= \"" + obj.CreditID.ToString() + "\" ");
                str.Append(" CreditAmount = \"" + obj.CreditAmount.ToString() + "\" ");

                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder AccountTransactionDebittoXML(List<AccountTransactionData> collectionList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (AccountTransactionData obj in collectionList)
            {
                str.Append("<DEBITACCOUNT DebitID= \"" + obj.DebitID.ToString() + "\" ");
                str.Append(" DebitAmount = \"" + obj.DebitAmount.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder EmrgPhrDatatoXML(List<EmrgAdmissionData> EMRGServiceListPHR)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (EmrgAdmissionData obj in EMRGServiceListPHR)
            {
                str.Append("<EMRGPhrService ItemID= \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" ServiceCharge = \"" + obj.ServiceCharge.ToString() + "\" ");
                str.Append(" SerialID = \"" + obj.SerialID.ToString() + "\" ");
                str.Append(" Quantity = \"" + obj.Quantity.ToString() + "\" ");
                str.Append(" NetPHRServiceCharge = \"" + obj.NetPHRServiceCharge.ToString() + "\" ");
                str.Append(" Tax = \"" + obj.Tax.ToString() + "\" ");
                str.Append(" SubStockID = \"" + obj.SubStockID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder MSBBenefitToXml(List<BenefitData> collectionList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (BenefitData obj in collectionList)
            {
                str.Append("<MSBBENEFIT BenefitTypeID= \"" + obj.BeneficiaryTypeID.ToString() + "\" ");
                str.Append(" OPD = \"" + obj.opd.ToString() + "\" ");
                str.Append(" Investigation = \"" + obj.investigation.ToString() + "\" ");
                str.Append(" IPD = \"" + obj.ipd.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder MsbBedAllotationToXml(List<MsbBedAllotData> collectionList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (MsbBedAllotData obj in collectionList)
            {
                str.Append("<MSBBED EmployeeGradeID= \"" + obj.EmployeeGradeID.ToString() + "\" ");
                str.Append(" BedAllotedID = \"" + obj.BedAllotedID.ToString() + "\" ");
                str.Append(" isSelf = \"" + obj.isSelf.ToString() + "\" ");
                str.Append(" isDependent = \"" + obj.isDependent.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder DiscountRequestToXML(List<DiscountRequestServiceData> collectionList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (DiscountRequestServiceData obj in collectionList)
            {
                str.Append("<DISCOUNT serviceTypeID= \"" + obj.serviceTypeID.ToString() + "\" ");
                str.Append(" ServiceID = \"" + obj.ServiceID.ToString() + "\" ");
                str.Append(" ServiceName = \"" + obj.ServiceName.ToString() + "\" ");
                str.Append(" Quantity = \"" + obj.Quantity.ToString() + "\" ");
                str.Append(" amount = \"" + obj.amount.ToString() + "\" ");
                str.Append(" NetAmount = \"" + obj.NetAmount.ToString() + "\" ");
                str.Append(" DisType = \"" + obj.DisType.ToString() + "\" ");
                str.Append(" isDis = \"" + obj.isDis.ToString() + "\" ");
                str.Append(" DisValue = \"" + obj.DisValue.ToString() + "\" ");
                str.Append(" DisAmount = \"" + obj.DisAmount.ToString() + "\" ");
                str.Append(" DoctorID = \"" + obj.DoctorID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder DiscountApprovalToXML(List<DiscountRequestServiceData> collectionList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (DiscountRequestServiceData obj in collectionList)
            {
                str.Append("<APPROVAL serviceTypeID= \"" + obj.serviceTypeID.ToString() + "\" ");
                str.Append(" BillNo = \"" + obj.BillNo.ToString() + "\" ");
                str.Append(" UHID = \"" + obj.UHID.ToString() + "\" ");
                str.Append(" isDis = \"" + obj.isDis.ToString() + "\" ");
                str.Append(" ServiceID = \"" + obj.ServiceID.ToString() + "\" ");
                str.Append(" DisType = \"" + obj.DisType.ToString() + "\" ");
                str.Append(" DisValue = \"" + obj.DisValue.ToString() + "\" ");
                str.Append(" DisAmount = \"" + obj.DisAmount.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder DiscountApprovalListToXML(List<DiscountRequestServiceData> collectionList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (DiscountRequestServiceData obj in collectionList)
            {
                str.Append("<APPROVAL serviceTypeID= \"" + obj.serviceTypeID.ToString() + "\" ");
                str.Append(" BillNo = \"" + obj.BillNo.ToString() + "\" ");
                str.Append(" UHID = \"" + obj.UHID.ToString() + "\" ");
                str.Append(" isDis = \"" + obj.isDis.ToString() + "\" ");
                str.Append(" ServiceID = \"" + obj.ServiceID.ToString() + "\" ");
                str.Append(" DisType = \"" + obj.DisType.ToString() + "\" ");
                str.Append(" HosShare = \"" + obj.HosShare.ToString() + "\" ");
                str.Append(" DocShare = \"" + obj.DocShare.ToString() + "\" ");
                str.Append(" RefShare = \"" + obj.RefShare.ToString() + "\" ");
                str.Append(" NetAmount = \"" + obj.NetAmount.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder LabOutsourceRecordDatatoXML(List<LabOutsourceManagerData> LabOutsource)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (LabOutsourceManagerData obj in LabOutsource)
            {
                str.Append("<Outsource ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" IsSampleCollcteded = \"" + obj.IsSampleCollcteded.ToString() + "\" ");
                str.Append(" IsOutsourcedSampleSend = \"" + obj.IsOutsourcedSampleSend.ToString() + "\" ");
                str.Append(" IsOutsourcedReportReceived = \"" + obj.IsOutsourcedReportReceived.ToString() + "\" ");
                str.Append(" IsOutsourcedTest = \"" + obj.IsOutsourcedTest.ToString() + "\" ");

                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder OpdLabDiscountBillDatatoXML(List<LabBillingData> LabServiceList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (LabBillingData obj in LabServiceList)
            {
                str.Append("<OPDLabBill TestName= \"" + obj.TestName.ToString() + "\" ");
                str.Append(" LabServiceCharge = \"" + obj.LabServiceCharge.ToString() + "\" ");
                str.Append(" Quantity = \"" + obj.Quantity.ToString() + "\" ");
                str.Append(" NetLabServiceCharge = \"" + obj.NetLabServiceCharge.ToString() + "\" ");
                str.Append(" LabServiceID = \"" + obj.LabServiceID.ToString() + "\" ");
                str.Append(" DocID = \"" + obj.DocID.ToString() + "\" ");
                str.Append(" TestCenterID = \"" + obj.TestCenterID.ToString() + "\" ");
                str.Append(" UrgencyID = \"" + obj.UrgencyID.ToString() + "\" ");
                str.Append(" isDis = \"" + obj.isDis.ToString() + "\" ");
                str.Append(" DisAmount = \"" + obj.DisAmount.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder OpdDiscountBillDatatoXML(List<OPDbillingData> ServiceList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (OPDbillingData obj in ServiceList)
            {
                str.Append("<OPDBill ServiceName= \"" + obj.ServiceName.ToString() + "\" ");
                str.Append(" ServiceCharge = \"" + obj.ServiceCharge.ToString() + "\" ");
                str.Append(" Quantity = \"" + obj.Quantity.ToString() + "\" ");
                str.Append(" NetServiceCharge = \"" + obj.NetServiceCharge.ToString() + "\" ");
                str.Append(" ServiceID = \"" + obj.ServiceID.ToString() + "\" ");
                str.Append(" DocID = \"" + obj.DocID.ToString() + "\" ");
                str.Append(" isDis = \"" + obj.isDis.ToString() + "\" ");
                str.Append(" DisAmount = \"" + obj.DisAmount.ToString() + "\" ");
                str.Append(" ServiceTypeID = \"" + obj.ServiceTypeID.ToString() + "\" ");
                str.Append(" SubGroupID = \"" + obj.SubGroupID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static String LedgerDatatoXML(List<AccountLedgerMasterData> objData)
        {
            String xmlstc = "";
            xmlstc = "<ENVELOPE>\r\n";
            xmlstc = xmlstc + "<HEADER>\r\n";
            xmlstc = xmlstc + "<TALLYREQUEST>Import Data</TALLYREQUEST>\r\n";
            xmlstc = xmlstc + "</HEADER>\r\n";
            xmlstc = xmlstc + "<BODY>\r\n";
            xmlstc = xmlstc + "<IMPORTDATA>\r\n";
            xmlstc = xmlstc + "<REQUESTDESC>\r\n";
            xmlstc = xmlstc + "<REPORTNAME>All Masters</REPORTNAME>\r\n";
            xmlstc = xmlstc + "</REQUESTDESC>\r\n";
            xmlstc = xmlstc + "<REQUESTDATA>\r\n";
            xmlstc = xmlstc + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\">\r\n";
            foreach (AccountLedgerMasterData obj in objData)
            {
                xmlstc = xmlstc + "<LEDGER NAME=" + "\"" + obj.AccountName + "\" Action =" + "\"" + "Create" + "\">\r\n";
                xmlstc = xmlstc + "<NAME>" + obj.AccountName + "</NAME>\r\n";
                xmlstc = xmlstc + "<PARENT>" + obj.GroupName + "</PARENT>\r\n";
                xmlstc = xmlstc + "<OPENINGBALANCE>" + obj.Opnbal + "</OPENINGBALANCE>\r\n";
                xmlstc = xmlstc + "<ISBILLWISEON>Yes</ISBILLWISEON>\r\n";
                xmlstc = xmlstc + "</LEDGER>\r\n";
            }
            xmlstc = xmlstc + "</TALLYMESSAGE>\r\n";
            xmlstc = xmlstc + "</REQUESTDATA>\r\n";
            xmlstc = xmlstc + "</IMPORTDATA>\r\n";
            xmlstc = xmlstc + "</BODY>";
            xmlstc = xmlstc + "</ENVELOPE>";
            return xmlstc;
        }
        public static String GroupDatatoXML(List<AccountGroupMasterData> objData)
        {
            String xmlstc = "";
            xmlstc = "<ENVELOPE>\r\n";
            xmlstc = xmlstc + "<HEADER>\r\n";
            xmlstc = xmlstc + "<TALLYREQUEST>Import Data</TALLYREQUEST>\r\n";
            xmlstc = xmlstc + "</HEADER>\r\n";
            xmlstc = xmlstc + "<BODY>\r\n";
            xmlstc = xmlstc + "<IMPORTDATA>\r\n";
            xmlstc = xmlstc + "<REQUESTDESC>\r\n";
            xmlstc = xmlstc + "<REPORTNAME>All Masters</REPORTNAME>\r\n";
            xmlstc = xmlstc + "</REQUESTDESC>\r\n";
            xmlstc = xmlstc + "<REQUESTDATA>\r\n";
            xmlstc = xmlstc + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\">\r\n";
            foreach (AccountGroupMasterData obj in objData)
            {
                xmlstc = xmlstc + "<GROUP NAME=" + "\"" + obj.GroupName + "\" Action =" + "\"" + "Create" + "\">\r\n";
                xmlstc = xmlstc + "<NAME>" + obj.GroupName + "</NAME>\r\n";
                xmlstc = xmlstc + "<PARENT>" + obj.Under + "</PARENT>\r\n";
                xmlstc = xmlstc + "</GROUP>\r\n";
            }
            xmlstc = xmlstc + "</TALLYMESSAGE>\r\n";
            xmlstc = xmlstc + "</REQUESTDATA>\r\n";
            xmlstc = xmlstc + "</IMPORTDATA>\r\n";
            xmlstc = xmlstc + "</BODY>";
            xmlstc = xmlstc + "</ENVELOPE>";
            return xmlstc;
        }
        public static StringBuilder DependentDocumenttoXML(List<DocumentUploadData> fileUpload)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (DocumentUploadData obj in fileUpload)
            {

                str.Append("<Upload EmpID= \"" + obj.EmployeeID.ToString() + "\" ");
                str.Append(" Tittle = \"" + obj.Tittle.ToString() + "\" ");
                str.Append(" Filename = \"" + obj.FileName.ToString() + "\" ");
                str.Append(" path = \"" + obj.FilePath.ToString() + "\" ");
                str.Append(" Contenttype = \"" + obj.ContentType.ToString() + "\" ");
                str.Append(" UHID = \"" + obj.UHID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder EmpPhototoXML(List<EmployeeData> empPhotoList)
        {
            StringBuilder str = new StringBuilder();
            //byte[] byteArray = ASCIIEncoding.ASCII.GetBytes(str.ToString());


            foreach (EmployeeData obj in empPhotoList)
            {
                byte[] img = ASCIIEncoding.ASCII.GetBytes(obj.ImageFile.ToString());
                //foreach (byte b in byteArray)
                //{
                str.Append("<empPhoto EmployeeID= \"" + obj.EmployeeID.ToString() + "\" ");
                for (int i = 0; i < img.Length; i++)
                {
                    //str.Append(" ImageFile = \"" + img[i] + "\" ");
                    str.Append(img[i]);
                }
                str.Append(" ImageFile = \"" + img + "\" ");
                str.Append(" EmployeePhotoLocation = \"" + obj.EmployeePhotoLocation.ToString() + "\" ");
                str.Append(" />");
                //}
            }
            return str;
        }
        //public static StringBuilder EmpPhototoXML(List<EmployeeData> empPhotoList)
        //{
        //    StringBuilder str = new StringBuilder();
        //    //str.Append("<?xml version=\"1.0\"?>");
        //    //str.Append("<Root>");
        //    foreach (EmployeeData obj in empPhotoList)
        //    {
        //        str.Append("<empPhoto EmployeeID= \"" + obj.EmployeeID.ToString() + "\" ");
        //        str.Append(" ImageFile = \"" + obj.ImageFile.ToString() + "\" ");
        //        str.Append(" EmployeePhotoLocation = \"" + obj.EmployeePhotoLocation.ToString() + "\" ");
        //        str.Append(" />");
        //    }
        //    return str;
        //}
        public static StringBuilder ExtraDiscountDatatoXML(List<Discount> ServiceList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (Discount obj in ServiceList)
            {
                str.Append("<EXTRADISCOUNT PatientCatID= \"" + obj.PatientCatID.ToString() + "\" ");
                str.Append(" SubCatID = \"" + obj.SubCatID.ToString() + "\" ");
                str.Append(" DiscountAmount = \"" + obj.DiscountAmount.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static String GenerateVoucherXML(AccountVoucherData objData)
        {
            String voucher = "";
            String Body = "";
            String header = "<VOUCHER REMOTEID=\"" + objData.voucher + "\"  VCHTYPE=\"" + objData.voucherType + "\"  >"
                            + "<DATE>" + objData.TransactionDate + "</DATE>"
                            + "<GUID>" + objData.voucher + "</GUID>"
                            + "<NARRATION>" + objData.Narration + "</NARRATION>"
                            + "<VOUCHERTYPENAME>" + objData.voucherType + "</VOUCHERTYPENAME>"
                            + "<VOUCHERNUMBER>" + objData.voucher + "</VOUCHERNUMBER>"
                            + "<PARTYLEDGERNAME>" + objData.Partlyledger + "</PARTYLEDGERNAME>";

            foreach (TransactionDetailData obj in objData.TransactionDetails)
            {
                if (objData.TransType == 1)
                {
                    if (obj.LedgerType == "CR")
                    {
                        Body = Body + "<ALLLEDGERENTRIES.LIST>"
                                                 + "<LEDGERNAME>" + obj.Ledger + "</LEDGERNAME>"
                                                 + "<ISDEEMEDPOSITIVE>NO</ISDEEMEDPOSITIVE>"
                                                 + "<AMOUNT>" + obj.amount + "</AMOUNT>"
                                                 + "</ALLLEDGERENTRIES.LIST>";
                    }
                    else
                    {
                        Body = Body + "<ALLLEDGERENTRIES.LIST>"
                                                + "<LEDGERNAME>" + obj.Ledger + "</LEDGERNAME>"
                                                + "<ISDEEMEDPOSITIVE>YES</ISDEEMEDPOSITIVE>"
                                                + "<AMOUNT>-" + obj.amount + "</AMOUNT>"
                                                + "</ALLLEDGERENTRIES.LIST>";
                    }

                }
                else
                {
                    if (objData.TransType == 4)
                    {
                        if (obj.LedgerType == "DR")
                        {
                            Body = Body + "<ALLLEDGERENTRIES.LIST>"
                                                   + "<LEDGERNAME>" + obj.Ledger + "</LEDGERNAME>"
                                                   + "<ISDEEMEDPOSITIVE>YES</ISDEEMEDPOSITIVE>"
                                                   + "<AMOUNT>-" + obj.amount + "</AMOUNT>"
                                                   + "</ALLLEDGERENTRIES.LIST>";

                        }
                        else
                        {
                            Body = Body + "<ALLLEDGERENTRIES.LIST>"
                                                      + "<LEDGERNAME>" + obj.Ledger + "</LEDGERNAME>"
                                                      + "<ISDEEMEDPOSITIVE>NO</ISDEEMEDPOSITIVE>"
                                                      + "<AMOUNT>" + obj.amount + "</AMOUNT>"
                                                      + "</ALLLEDGERENTRIES.LIST>";
                        }
                    }
                    else
                    {
                        if (obj.LedgerType == "DR")
                        {
                            Body = Body + "<ALLLEDGERENTRIES.LIST>"
                                                     + "<LEDGERNAME>" + obj.Ledger + "</LEDGERNAME>"
                                                     + "<ISDEEMEDPOSITIVE>NO</ISDEEMEDPOSITIVE>"
                                                     + "<AMOUNT>" + obj.amount + "</AMOUNT>"
                                                     + "</ALLLEDGERENTRIES.LIST>";
                        }
                        else
                        {
                            Body = Body + "<ALLLEDGERENTRIES.LIST>"
                                                    + "<LEDGERNAME>" + obj.Ledger + "</LEDGERNAME>"
                                                    + "<ISDEEMEDPOSITIVE>YES</ISDEEMEDPOSITIVE>"
                                                    + "<AMOUNT>-" + obj.amount + "</AMOUNT>"
                                                    + "</ALLLEDGERENTRIES.LIST>";
                        }
                    }
                }

            }


            voucher = header + Body + "</VOUCHER>";

            return voucher;
        }
        public static String GenerateCompleteVoucherXML(String xml)
        {
            String voucher = "";
            String header = "<ENVELOPE>"
                            + "<HEADER>"
                            + "<TALLYREQUEST>Import Data</TALLYREQUEST>"
                            + "</HEADER>"
                            + "<BODY>"
                            + "<IMPORTDATA>"
                            + "<REQUESTDESC>"
                            + "<REPORTNAME>Vouchers</REPORTNAME>"
                            + "</REQUESTDESC>"
                            + "<REQUESTDATA>"
                            + "<TALLYMESSAGE xmlns:UDF=\"TallyUDF\">";

            String footer = "</TALLYMESSAGE>"
                    + "</REQUESTDATA>"
                    + "</IMPORTDATA>"
                    + "</BODY>"
                    + "</ENVELOPE>";

            voucher = header + xml + footer;

            return voucher;
        }
        public static String PatientQRDataXML(PatientQRdata objData)
        {
            String QR = "";
            QR = QR + "<PATIENTDATA>";
            QR = QR + "<UHID>" + objData.UHID + "</UHID>";
            QR = QR + "<NAME>" + objData.Name + "</NAME>";
            QR = QR + "<DOB>" + objData.DOB + "</DOB>";
            QR = QR + "<GENDER>" + objData.Gender + "</GENDER>";
            QR = QR + "<ADDRESS>" + objData.Address + "</ADDRESS>";
            QR = QR + "<CONTACTNO>" + objData.ContactNo + "</CONTACTNO>";
            if (objData.IPNo != "")
            {
                QR = QR + "<IPNO>" + objData.IPNo + "</IPNO>";
            }
            QR = QR + "</PATIENTDATA>";
            return QR;
        }
        public static StringBuilder AdmissionDiscountToXML(List<EMRDiscountListData> ServiceList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (EMRDiscountListData obj in ServiceList)
            {
                str.Append("<DISCOUNT ShareID= \"" + obj.ShareID.ToString() + "\" ");
                str.Append(" ShareAmount = \"" + obj.ShareAmount.ToString() + "\" ");
                str.Append(" DoctorName = \"" + obj.DoctorName.ToString() + "\" ");
                str.Append(" disType = \"" + obj.disType.ToString() + "\" ");
                str.Append(" isDis = \"" + (obj.disValue > 0 ? 1 : 0).ToString() + "\" ");
                str.Append(" disValue = \"" + obj.disValue.ToString() + "\" ");
                str.Append(" discountAmount = \"" + obj.discountAmount.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder IpAdmissionDiscountToXML(List<IPDiscountListData> ServiceList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (IPDiscountListData obj in ServiceList)
            {
                str.Append("<DISCOUNT ShareID= \"" + obj.ShareID.ToString() + "\" ");
                str.Append(" ShareAmount = \"" + obj.ShareAmount.ToString() + "\" ");
                str.Append(" DoctorName = \"" + obj.DoctorName.ToString() + "\" ");
                str.Append(" disType = \"" + obj.disType.ToString() + "\" ");
                str.Append(" isDis = \"" + (obj.disValue > 0 ? 1 : 0).ToString() + "\" ");
                str.Append(" disValue = \"" + obj.disValue.ToString() + "\" ");
                str.Append(" discountAmount = \"" + obj.discountAmount.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder Gen_IndentRecordDatatoXML(List<GenIndentData> Indent)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (GenIndentData obj in Indent)
            {
                str.Append("<Indent ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" IndentNo = \"" + obj.IndentNo.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" IdentID = \"" + obj.IndentID.ToString() + "\" ");
                str.Append(" StockID = \"" + obj.StockID.ToString() + "\" ");
                str.Append(" DeptID = \"" + obj.DeptID.ToString() + "\" ");
                str.Append(" BalStock = \"" + obj.BalStock.ToString() + "\" ");
                str.Append(" reqdQty = \"" + obj.ReqdQty.ToString() + "\" ");
                str.Append(" ApprvQty = \"" + obj.apprvQty.ToString() + "\" ");
                str.Append(" Remarks = \"" + obj.Remarks.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder Med_IndentRecordDatatoXML(List<MedIndentData> Indent)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (MedIndentData obj in Indent)
            {
                str.Append("<Indent ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" IndentNo = \"" + obj.IndentNo.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" IdentID = \"" + obj.IndentID.ToString() + "\" ");
                str.Append(" StockID = \"" + obj.StockID.ToString() + "\" ");
                str.Append(" StockNo = \"" + obj.StockNo.ToString() + "\" ");
                str.Append(" BalStock = \"" + obj.BalStock.ToString() + "\" ");
                str.Append(" BatchNo = \"" + obj.BatchNo.ToString() + "\" ");
                str.Append(" reqdQty = \"" + obj.ReqdQty.ToString() + "\" ");
                str.Append(" ApprvQty = \"" + obj.apprvQty.ToString() + "\" ");
                str.Append(" Remarks = \"" + obj.Remarks.ToString() + "\" ");
                str.Append(" SupplierID = \"" + obj.SupplierID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder Gen_indentverificationDatatoXML(List<GenIndentData> Indent)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (GenIndentData obj in Indent)
            {
                str.Append("<Indent IndentNo= \"" + obj.IndentNo.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" reqdQty = \"" + obj.ReqdQty.ToString() + "\" ");
                str.Append(" Remarks = \"" + obj.Remarks.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder GENindentCollectionRecordDatatoXML(List<GenIndentData> Received)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (GenIndentData obj in Received)
            {
                str.Append("<Received ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" IndentNo = \"" + obj.IndentNo.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" IdentID = \"" + obj.IndentID.ToString() + "\" ");
                str.Append(" StockID = \"" + obj.StockID.ToString() + "\" ");
                str.Append(" BalStock = \"" + obj.BalStock.ToString() + "\" ");
                str.Append(" ReqdQty = \"" + obj.ReqdQty.ToString() + "\" ");
                str.Append(" apprvQty = \"" + obj.apprvQty.ToString() + "\" ");
                //str.Append(" HndQty = \"" + obj.HndQty.ToString() + "\" ");
                str.Append(" RecvQty = \"" + obj.RecvQty.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder DeptStockAvailDatatoXML(List<GenDeptStockAvailibilityData> DeptStoctavail)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (GenDeptStockAvailibilityData obj in DeptStoctavail)
            {
                str.Append("<DeptStoctavail ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" deptID = \"" + obj.deptID.ToString() + "\" ");
                str.Append(" stockAvail = \"" + obj.stockAvail.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder CondemnStockDetailsDatatoXML(List<CondemnReqApprovedData> CondemnList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (CondemnReqApprovedData obj in CondemnList)
            {
                str.Append("<CondemnStockItemList ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" SubStockID = \"" + obj.SubStockID.ToString() + "\" ");
                str.Append(" CondemnQty = \"" + obj.CondemnQty.ToString() + "\" ");
                str.Append(" StockNo = \"" + obj.StockNo.ToString() + "\" ");
                str.Append(" CondemnRemark = \"" + obj.CondemnRemark.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder CondemnApprovedDatatoXML(List<CondemnReqApprovedData> CondemnApprovedList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (CondemnReqApprovedData obj in CondemnApprovedList)
            {
                str.Append("<CondemnApprovedList ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" SubStockID = \"" + obj.SubStockID.ToString() + "\" ");
                str.Append(" CondemnQty = \"" + obj.CondemnQty.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder InterStockTransferDatatoXML(List<GenInterStockTransferData> TransferStockList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (GenInterStockTransferData obj in TransferStockList)
            {
                str.Append("<InterStockTransferList ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" GenStockID = \"" + obj.GenStockID.ToString() + "\" ");
                str.Append(" StockNo = \"" + obj.StockNo.ToString() + "\" ");
                str.Append(" TransferQty = \"" + obj.TransferQty.ToString() + "\" ");
                str.Append(" TransferRemark = \"" + obj.TransferRemarks.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder ReturnStockDatatoXML(List<VendorStockReturnData> ReturnStockList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (VendorStockReturnData obj in ReturnStockList)
            {
                str.Append("<ReturnStockList ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" ReceiptNo = \"" + obj.ReceiptNo.ToString() + "\" ");
                str.Append(" StockNo = \"" + obj.StockNo.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" TotalRecievedQty = \"" + obj.TotalRecievedQty.ToString() + "\" ");
                str.Append(" CPPerQty = \"" + obj.CPPerQty.ToString() + "\" ");
                str.Append(" ReturnQty = \"" + obj.ReturnQty.ToString() + "\" ");
                str.Append(" ReturnAmount = \"" + obj.ReturnAmount.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        //----------------LEAVE CARRY FORWARD---------------------
        public static StringBuilder CarriedforwardleavetoXML(List<LeaveTypeData> CarriedforwardleaveList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (LeaveTypeData obj in CarriedforwardleaveList)
            {
                str.Append("<CarriedforwardleaveList LeaveID= \"" + obj.LeaveID.ToString() + "\" ");
                str.Append(" Leavecarriedforward = \"" + obj.Leavecarriedforward.ToString() + "\" ");
                str.Append(" LeaveCountID = \"" + obj.LeaveCountID.ToString() + "\" ");
                str.Append(" LeaveAvailinAdvance = \"" + obj.LeaveAvailinAdvance.ToString() + "\" ");
                str.Append(" leaveHalfday = \"" + obj.leaveHalfday.ToString() + "\" ");
                str.Append(" LeaveDocument = \"" + obj.LeaveDocument.ToString() + "\" ");
                str.Append(" LeaveEligible = \"" + obj.LeaveEligible.ToString() + "\" ");
                str.Append(" Combinedleave = \"" + obj.leavecombined.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder AdjustApproveleavetoXML(List<LeaveApplicationData> LstleaveData)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");


            int k = 0;
            int l = 0;
            for (int i = 0; i < LstleaveData.Count; i++)
            {
                str.Append("<LeaveadjustApproveList LeaveRecordID= \"" + LstleaveData[i].LeaveRecordID.ToString() + "\" ");
                str.Append(" date = \"" + LstleaveData[i].date.ToString() + "\" ");
                str.Append(" LeaveID = \"" + LstleaveData[i].LeaveID.ToString() + "\" ");
                str.Append(" LeaveCategoryID = \"" + LstleaveData[i].LeaveCategoryID.ToString() + "\" ");
                str.Append(" Status = \"" + LstleaveData[i].Status.ToString() + "\" ");
                str.Append(" LeaveEmployeeID = \"" + LstleaveData[i].LeaveEmployeeID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }

        //----------------Duty Schedular---------------------
        public static StringBuilder DutySchedulartoXML(List<DutyRosterData> EmpDutyList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (DutyRosterData obj in EmpDutyList)
            {
                str.Append("<EmpDutyList DepartmentID= \"" + obj.DepartmentID.ToString() + "\" ");
                str.Append(" EmpID = \"" + obj.EmpID.ToString() + "\" ");
                str.Append(" Year = \"" + obj.Year.ToString() + "\" ");
                str.Append(" Month = \"" + obj.Month.ToString() + "\" ");
                str.Append(" SeasonID = \"" + obj.SeasonID.ToString() + "\" ");
                str.Append(" No_Of_Days = \"" + obj.No_Of_Days.ToString() + "\" ");
                str.Append(" RosterDetails_Day1 = \"" + obj.RosterDetails_Day1.ToString() + "\" ");
                str.Append(" Date_Day1 = \"" + obj.Date_Day1.ToString() + "\" ");
                str.Append(" RosterDetails_Day2 = \"" + obj.RosterDetails_Day2.ToString() + "\" ");
                str.Append(" Date_Day2 = \"" + obj.Date_Day2.ToString() + "\" ");
                str.Append(" RosterDetails_Day3 = \"" + obj.RosterDetails_Day3.ToString() + "\" ");
                str.Append(" Date_Day3 = \"" + obj.Date_Day3.ToString() + "\" ");
                str.Append(" RosterDetails_Day4 = \"" + obj.RosterDetails_Day4.ToString() + "\" ");
                str.Append(" Date_Day4 = \"" + obj.Date_Day4.ToString() + "\" ");
                str.Append(" RosterDetails_Day5 = \"" + obj.RosterDetails_Day5.ToString() + "\" ");
                str.Append(" Date_Day5 = \"" + obj.Date_Day5.ToString() + "\" ");
                str.Append(" RosterDetails_Day6 = \"" + obj.RosterDetails_Day6.ToString() + "\" ");
                str.Append(" Date_Day6 = \"" + obj.Date_Day6.ToString() + "\" ");
                str.Append(" RosterDetails_Day7 = \"" + obj.RosterDetails_Day7.ToString() + "\" ");
                str.Append(" Date_Day7 = \"" + obj.Date_Day7.ToString() + "\" ");
                str.Append(" RosterDetails_Day8 = \"" + obj.RosterDetails_Day8.ToString() + "\" ");
                str.Append(" Date_Day8 = \"" + obj.Date_Day8.ToString() + "\" ");
                str.Append(" RosterDetails_Day9 = \"" + obj.RosterDetails_Day9.ToString() + "\" ");
                str.Append(" Date_Day9 = \"" + obj.Date_Day9.ToString() + "\" ");
                str.Append(" RosterDetails_Day10 = \"" + obj.RosterDetails_Day10.ToString() + "\" ");
                str.Append(" Date_Day10 = \"" + obj.Date_Day10.ToString() + "\" ");
                str.Append(" RosterDetails_Day11 = \"" + obj.RosterDetails_Day11.ToString() + "\" ");
                str.Append(" Date_Day11 = \"" + obj.Date_Day11.ToString() + "\" ");
                str.Append(" RosterDetails_Day12 = \"" + obj.RosterDetails_Day12.ToString() + "\" ");
                str.Append(" Date_Day12 = \"" + obj.Date_Day12.ToString() + "\" ");
                str.Append(" RosterDetails_Day13 = \"" + obj.RosterDetails_Day13.ToString() + "\" ");
                str.Append(" Date_Day13 = \"" + obj.Date_Day13.ToString() + "\" ");
                str.Append(" RosterDetails_Day14 = \"" + obj.RosterDetails_Day14.ToString() + "\" ");
                str.Append(" Date_Day14 = \"" + obj.Date_Day14.ToString() + "\" ");
                str.Append(" RosterDetails_Day15 = \"" + obj.RosterDetails_Day15.ToString() + "\" ");
                str.Append(" Date_Day15 = \"" + obj.Date_Day15.ToString() + "\" ");
                str.Append(" RosterDetails_Day16 = \"" + obj.RosterDetails_Day16.ToString() + "\" ");
                str.Append(" Date_Day16 = \"" + obj.Date_Day16.ToString() + "\" ");
                str.Append(" RosterDetails_Day17 = \"" + obj.RosterDetails_Day17.ToString() + "\" ");
                str.Append(" Date_Day17 = \"" + obj.Date_Day17.ToString() + "\" ");
                str.Append(" RosterDetails_Day18 = \"" + obj.RosterDetails_Day18.ToString() + "\" ");
                str.Append(" Date_Day18 = \"" + obj.Date_Day18.ToString() + "\" ");
                str.Append(" RosterDetails_Day19 = \"" + obj.RosterDetails_Day19.ToString() + "\" ");
                str.Append(" Date_Day19 = \"" + obj.Date_Day19.ToString() + "\" ");
                str.Append(" RosterDetails_Day20 = \"" + obj.RosterDetails_Day20.ToString() + "\" ");
                str.Append(" Date_Day20 = \"" + obj.Date_Day20.ToString() + "\" ");
                str.Append(" RosterDetails_Day21 = \"" + obj.RosterDetails_Day21.ToString() + "\" ");
                str.Append(" Date_Day21 = \"" + obj.Date_Day21.ToString() + "\" ");
                str.Append(" RosterDetails_Day22 = \"" + obj.RosterDetails_Day22.ToString() + "\" ");
                str.Append(" Date_Day22 = \"" + obj.Date_Day22.ToString() + "\" ");
                str.Append(" RosterDetails_Day23 = \"" + obj.RosterDetails_Day23.ToString() + "\" ");
                str.Append(" Date_Day23 = \"" + obj.Date_Day23.ToString() + "\" ");
                str.Append(" RosterDetails_Day24 = \"" + obj.RosterDetails_Day24.ToString() + "\" ");
                str.Append(" Date_Day24 = \"" + obj.Date_Day24.ToString() + "\" ");
                str.Append(" RosterDetails_Day25 = \"" + obj.RosterDetails_Day25.ToString() + "\" ");
                str.Append(" Date_Day25 = \"" + obj.Date_Day25.ToString() + "\" ");
                str.Append(" RosterDetails_Day26 = \"" + obj.RosterDetails_Day26.ToString() + "\" ");
                str.Append(" Date_Day26 = \"" + obj.Date_Day26.ToString() + "\" ");
                str.Append(" RosterDetails_Day27 = \"" + obj.RosterDetails_Day27.ToString() + "\" ");
                str.Append(" Date_Day27 = \"" + obj.Date_Day27.ToString() + "\" ");
                str.Append(" RosterDetails_Day28 = \"" + obj.RosterDetails_Day28.ToString() + "\" ");
                str.Append(" Date_Day28 = \"" + obj.Date_Day28.ToString() + "\" ");
                str.Append(" RosterDetails_Day29 = \"" + obj.RosterDetails_Day29.ToString() + "\" ");
                str.Append(" Date_Day29 = \"" + obj.Date_Day29.ToString() + "\" ");
                str.Append(" RosterDetails_Day30 = \"" + obj.RosterDetails_Day30.ToString() + "\" ");
                str.Append(" Date_Day30 = \"" + obj.Date_Day30.ToString() + "\" ");
                str.Append(" RosterDetails_Day31 = \"" + obj.RosterDetails_Day31.ToString() + "\" ");
                str.Append(" Date_Day31 = \"" + obj.Date_Day31.ToString() + "\" ");

                str.Append(" />");
            }
            return str;
        }

        public static object ControllisttoXML(List<HRControllManagerData> listcontrols)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (HRControllManagerData obj in listcontrols)
            {
                str.Append("<HRControllList EmployeeID= \"" + obj.EmployeeID.ToString() + "\" ");
                str.Append(" DepartmentID = \"" + obj.DepartmentID.ToString() + "\" ");
                str.Append(" DesignationID = \"" + obj.DesignationID.ToString() + "\" ");
                str.Append(" EmployeeTypeID = \"" + obj.EmployeeTypeID.ToString() + "\" ");
                str.Append(" LeaveRequestEnable = \"" + obj.LeaveRequestEnable.ToString() + "\" ");
                str.Append(" LeaveApproveEnable = \"" + obj.LeaveApproveEnable.ToString() + "\" ");
                str.Append(" RosterUpdateEnable = \"" + obj.RosterUpdateEnable.ToString() + "\" ");
                str.Append(" RosterChangeRequestEnable = \"" + obj.RosterChangeRequestEnable.ToString() + "\" ");
                str.Append(" RosterChangeApproveEnable = \"" + obj.RosterChangeApproveEnable.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        //--------------------------Purchase--------------------
        public static StringBuilder PurchaseRequisitionDataToXML(List<PurchaseRequisitionData> PurchaseRequisitionList)
        {
            StringBuilder str = new StringBuilder();
            foreach (PurchaseRequisitionData obj in PurchaseRequisitionList)
            {
                str.Append("<PurchaseRequisitionList ItemID= \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" ItemName = \"" + obj.ItemName.ToString() + "\" ");
                str.Append(" AvailableQuantity = \"" + obj.TotalAvailableQuantity.ToString() + "\" ");
                str.Append(" RequisitionQuantity = \"" + obj.PurchaseRequisitionQuantity.ToString() + "\" ");
                str.Append(" ProbableRate = \"" + obj.ProbableRate.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder PurchaseApproveDataToXML(List<PurchaseRequisitionData> PurchaseApproveList)
        {
            StringBuilder str = new StringBuilder();
            foreach (PurchaseRequisitionData obj in PurchaseApproveList)
            {
                str.Append("<PurchaseApproveList ItemID= \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" TotalApprovedQuantity = \"" + obj.TotalApprovedQuantity.ToString() + "\" ");
                str.Append(" ItemStatusID = \"" + obj.ItemStatusID.ToString() + "\" ");
                str.Append(" ItemStatusName = \"" + obj.ItemStatusName.ToString() + "\" ");
                str.Append(" Remark = \"" + obj.Remark.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder PurchaseOrderDataToXML(List<PurchaseRequisitionData> PurchaseOrderList)
        {
            StringBuilder str = new StringBuilder();
            foreach (PurchaseRequisitionData obj in PurchaseOrderList)
            {
                str.Append("<PurchaseOrderList ItemID= \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder PurchaseOrderRecievedDataToXML(List<PurchaseRequisitionData> PurchaseOrderRecievedList)
        {
            StringBuilder str = new StringBuilder();
            foreach (PurchaseRequisitionData obj in PurchaseOrderRecievedList)
            {
                str.Append("<PurchaseOrderRecievedList ItemID= \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" RecievedQuantity = \"" + obj.RecievedQuantity.ToString() + "\" ");
                str.Append(" RecievedStatus = \"" + obj.RecievedStatus.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        //--------------------------End Purchase--------------------
        //--------------------------Start PHR vendor payment--------------------
        public static StringBuilder VendorPaymentDatatoXML(List<VendorPaymentData> objlst)
        {
            StringBuilder str = new StringBuilder();
            foreach (VendorPaymentData obj in objlst)
            {
                str.Append("<VendorPaymentList InvoiceNo= \"" + obj.InvoiceNo.ToString() + "\" ");
                str.Append(" ReceiptNo = \"" + obj.ReceiptNo.ToString() + "\" ");
                str.Append(" VendorPayment_ID = \"" + obj.VendorPayment_ID.ToString() + "\" ");
                str.Append(" SupplierID = \"" + obj.SupplierID.ToString() + "\" ");
                str.Append(" ItemAddedDate = \"" + obj.ItemAddedDate.ToString("yyyy-MM-dd") + "\" ");
                str.Append(" Amount = \"" + obj.Amount.ToString() + "\" ");
                str.Append(" PaidAmount = \"" + obj.PaidAmount.ToString() + "\" ");
                str.Append(" DueAmount = \"" + obj.DueAmount.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static String PHRGroupDatatoXML(List<PHRAccountGroupMasterData> Listobjdata)
        {
            String xmlstc = "";
            xmlstc = "<ENVELOPE>\r\n";
            xmlstc = xmlstc + "<HEADER>\r\n";
            xmlstc = xmlstc + "<TALLYREQUEST>Import Data</TALLYREQUEST>\r\n";
            xmlstc = xmlstc + "</HEADER>\r\n";
            xmlstc = xmlstc + "<BODY>\r\n";
            xmlstc = xmlstc + "<IMPORTDATA>\r\n";
            xmlstc = xmlstc + "<REQUESTDESC>\r\n";
            xmlstc = xmlstc + "<REPORTNAME>All Masters</REPORTNAME>\r\n";
            xmlstc = xmlstc + "</REQUESTDESC>\r\n";
            xmlstc = xmlstc + "<REQUESTDATA>\r\n";
            xmlstc = xmlstc + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\">\r\n";
            foreach (PHRAccountGroupMasterData obj in Listobjdata)
            {
                xmlstc = xmlstc + "<GROUP NAME=" + "\"" + obj.GroupName + "\" Action =" + "\"" + "Create" + "\">\r\n";
                xmlstc = xmlstc + "<NAME>" + obj.GroupName + "</NAME>\r\n";
                xmlstc = xmlstc + "<PARENT>" + obj.Under + "</PARENT>\r\n";
                xmlstc = xmlstc + "</GROUP>\r\n";
            }
            xmlstc = xmlstc + "</TALLYMESSAGE>\r\n";
            xmlstc = xmlstc + "</REQUESTDATA>\r\n";
            xmlstc = xmlstc + "</IMPORTDATA>\r\n";
            xmlstc = xmlstc + "</BODY>";
            xmlstc = xmlstc + "</ENVELOPE>";
            return xmlstc;
        }

        public static String PHRLedgerDatatoXML(List<PHRAccountLedgerMasterData> Listobjdata)
        {
            String xmlstc = "";
            xmlstc = "<ENVELOPE>\r\n";
            xmlstc = xmlstc + "<HEADER>\r\n";
            xmlstc = xmlstc + "<TALLYREQUEST>Import Data</TALLYREQUEST>\r\n";
            xmlstc = xmlstc + "</HEADER>\r\n";
            xmlstc = xmlstc + "<BODY>\r\n";
            xmlstc = xmlstc + "<IMPORTDATA>\r\n";
            xmlstc = xmlstc + "<REQUESTDESC>\r\n";
            xmlstc = xmlstc + "<REPORTNAME>All Masters</REPORTNAME>\r\n";
            xmlstc = xmlstc + "</REQUESTDESC>\r\n";
            xmlstc = xmlstc + "<REQUESTDATA>\r\n";
            xmlstc = xmlstc + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\">\r\n";
            foreach (PHRAccountLedgerMasterData obj in Listobjdata)
            {
                xmlstc = xmlstc + "<LEDGER NAME=" + "\"" + obj.AccountName + "\" Action =" + "\"" + "Create" + "\">\r\n";
                xmlstc = xmlstc + "<NAME>" + obj.AccountName + "</NAME>\r\n";
                xmlstc = xmlstc + "<PARENT>" + obj.GroupName + "</PARENT>\r\n";
                xmlstc = xmlstc + "<OPENINGBALANCE>" + obj.Opnbal + "</OPENINGBALANCE>\r\n";
                xmlstc = xmlstc + "<ISBILLWISEON>Yes</ISBILLWISEON>\r\n";
                xmlstc = xmlstc + "</LEDGER>\r\n";
            }
            xmlstc = xmlstc + "</TALLYMESSAGE>\r\n";
            xmlstc = xmlstc + "</REQUESTDATA>\r\n";
            xmlstc = xmlstc + "</IMPORTDATA>\r\n";
            xmlstc = xmlstc + "</BODY>";
            xmlstc = xmlstc + "</ENVELOPE>";
            return xmlstc;
        }

        public static StringBuilder PHRAccountMappingCollectionDatatoXML(List<PHRAccountMappingMasterData> Listobjdata)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (PHRAccountMappingMasterData obj in Listobjdata)
            {
                str.Append("<ACCOUNTMAPPING ServiceType= \"" + obj.ServiceType.ToString() + "\" ");
                str.Append(" ServiceID = \"" + obj.ServiceID.ToString() + "\" ");
                str.Append(" SubServiceType = \"" + obj.SubServiceType.ToString() + "\" ");
                str.Append(" MappingType = \"" + obj.MappingType.ToString() + "\" ");
                str.Append(" GroupType = \"" + obj.GroupType.ToString() + "\" ");
                str.Append(" DebitID = \"" + obj.DebitID.ToString() + "\" ");
                str.Append(" CreditID = \"" + obj.CreditID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder DrugDetailsDatatoXML(List<PharIPIssueData> CondemnList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (PharIPIssueData obj in CondemnList)
            {
                str.Append("<DrugItemList ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" SubStockID = \"" + obj.SubStockID.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" DrugName = \"" + obj.DrugName.ToString() + "\" ");
                str.Append(" MRPperQty = \"" + obj.MRPperQty.ToString() + "\" ");
                str.Append(" NoUnit = \"" + obj.NoUnit.ToString() + "\" ");
                str.Append(" EquivalentQty = \"" + obj.EquivalentQty.ToString() + "\" ");
                str.Append(" NetCharge = \"" + obj.NetCharge.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder EmgDrugDetailsDatatoXML(List<EmgDrugIssueData> CondemnList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (EmgDrugIssueData obj in CondemnList)
            {
                str.Append("<DrugItemList ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" SubStockID = \"" + obj.SubStockID.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" DrugName = \"" + obj.DrugName.ToString() + "\" ");
                str.Append(" MRPperQty = \"" + obj.MRPperQty.ToString() + "\" ");
                str.Append(" NoUnit = \"" + obj.NoUnit.ToString() + "\" ");
                str.Append(" EquivalentQty = \"" + obj.EquivalentQty.ToString() + "\" ");
                str.Append(" NetCharge = \"" + obj.NetCharge.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        public static StringBuilder PHRAccountTransactionCredittoXML(List<PHRAccountTransactionData> ListobjdataCredit)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (PHRAccountTransactionData obj in ListobjdataCredit)
            {
                str.Append("<CREDITACCOUNT CreditID= \"" + obj.CreditID.ToString() + "\" ");
                str.Append(" CreditAmount = \"" + obj.CreditAmount.ToString() + "\" ");

                str.Append(" />");
            }
            return str;
        }

        public static StringBuilder PHRAccountTransactionDebittoXML(List<PHRAccountTransactionData> ListobjdataDebit)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (PHRAccountTransactionData obj in ListobjdataDebit)
            {
                str.Append("<DEBITACCOUNT DebitID= \"" + obj.DebitID.ToString() + "\" ");
                str.Append(" DebitAmount = \"" + obj.DebitAmount.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        //--------------------------END PHARMACY ACCOUNT--------------------
        public static StringBuilder PhrInterStockTransferDatatoXML(List<Phr_InterStockTransferData> TransferStockList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (Phr_InterStockTransferData obj in TransferStockList)
            {
                str.Append("<InterStockTransferList SubStockID= \"" + obj.SubStockID.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" MedSubStockID = \"" + obj.MedSubStockID.ToString() + "\" ");
                str.Append(" StockNo = \"" + obj.StockNo.ToString() + "\" ");
                str.Append(" CPperQty = \"" + obj.CPperQty.ToString() + "\" ");
                str.Append(" MRPperUnit = \"" + obj.MRPperUnit.ToString() + "\" ");
                str.Append(" MRPperQty = \"" + obj.MRPperQty.ToString() + "\" ");
                str.Append(" NoUnitBalance = \"" + obj.NoUnitBalance.ToString() + "\" ");
                str.Append(" EquivalentQtyBalance = \"" + obj.EquivalentQtyBalance.ToString() + "\" ");
                str.Append(" TransferQty = \"" + obj.TransferQty.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        //--------------------------START STOCK RETURN--------------------
        public static StringBuilder StockReturnListToXML(List<StockReturnData> StockReturnList)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (StockReturnData obj in StockReturnList)
            {
                str.Append("<StockReturnList StockID= \"" + obj.StockID.ToString() + "\" ");
                str.Append(" SerialID = \"" + obj.SerialID.ToString() + "\" ");
                str.Append(" ReceiptNo = \"" + obj.ReceiptNo.ToString() + "\" ");
                str.Append(" BatchNo = \"" + obj.BatchNo.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" CompanyID = \"" + obj.CompanyID.ToString() + "\" ");
                str.Append(" SupplierID = \"" + obj.SupplierID.ToString() + "\" ");
                str.Append(" ReturnQty = \"" + obj.ReturnQty.ToString() + "\" ");
                str.Append(" TaxableAmount = \"" + obj.TaxableAmount.ToString() + "\" ");
                str.Append(" CGST = \"" + obj.CGST.ToString() + "\" ");
                str.Append(" IGST = \"" + obj.IGST.ToString() + "\" ");
                str.Append(" SGST = \"" + obj.SGST.ToString() + "\" ");
                str.Append(" CPafterTax = \"" + obj.CPafterTax.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
        //--------------------------END STOCK RETURN--------------------

        //--------------------------START EMERG RETURN AFTER BILLING-------------------------
        public static StringBuilder EmergAfterBillingReturnDetailsDatatoXML(List<EmgReturnData> objlst)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (EmgReturnData obj in objlst)
            {
                str.Append("<EmergReturnList ID= \"" + obj.ID.ToString() + "\" ");
                str.Append(" EmergDrgIssueNo = \"" + obj.EmergDrgIssueNo.ToString() + "\" ");
                str.Append(" UHID = \"" + obj.UHID.ToString() + "\" ");
                str.Append(" SubStockID = \"" + obj.SubStockID.ToString() + "\" ");
                str.Append(" ItemID = \"" + obj.ItemID.ToString() + "\" ");
                str.Append(" Unit = \"" + obj.Unit.ToString() + "\" ");
                str.Append(" Quantity = \"" + obj.Quantity.ToString() + "\" ");
                str.Append(" ReturnQty = \"" + obj.Return.ToString() + "\" ");
                str.Append(" MRPperQty = \"" + obj.MRPperQty.ToString() + "\" ");
                str.Append(" ReturnAmt = \"" + obj.ReturnAmt.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }

        //-------------------- CULTURE REPORT -------------------------
        public static StringBuilder LabCultureDatatoXML(List<LabResultData> Result)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<?xml version=\"1.0\"?>");
            //str.Append("<Root>");
            foreach (LabResultData obj in Result)
            {
                str.Append("<ResultEntry AntibioticID= \"" + obj.AntibioticID.ToString() + "\" ");
                str.Append(" AntibioticSensitiveTypeID = \"" + obj.AntibioticSensitiveTypeID.ToString() + "\" ");
                str.Append(" />");
            }
            return str;
        }
    }

}