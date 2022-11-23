using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedAnalytics
{
    public class SalesAnalyticsData : BaseData
    {
        [DataMember]
        public Int32 Month { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
    }
    public class SaleAnalyticsGetData
    {
        [DataMember]
        public Int64 ItemQty { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public decimal SaleAmount { get; set; }
    }
    public class CommonanalyticData
    {
        [DataMember]
        public Int32 TotalCount { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public Int32 DailyTotalCount { get; set; }
        [DataMember]
        public Int32 TotalMale { get; set; }
        [DataMember]
        public Int32 TotalFemale { get; set; }
    }
    public class MaindashboardData
    {
        [DataMember]
        public Int32 TotalpatienttillDate { get; set; }
        [DataMember]
        public Int32 TotaltodayPatient { get; set; }
        [DataMember]
        public Int32 TotalinvoicettillDate { get; set; }
        [DataMember]
        public Int32 Totaltodayinvoice { get; set; }
        [DataMember]
        public Int32 TotaltestttillDate { get; set; }
        [DataMember]
        public Int32 Totaltodaytest { get; set; }
        [DataMember]
        public Int32 TotalReportGenerated { get; set; }
        [DataMember]
        public Int32 TotalPendeingReport { get; set; }
        [DataMember]
        public Int32 Totalreportnotverified { get; set; }
        [DataMember]
        public Int32 TotalCentre { get; set; }
        [DataMember]
        public Int32 DailyRejected { get; set; }
        [DataMember]
        public Int32 TotalRejected { get; set; }
        [DataMember]
        public Int32 TotalReportPending { get; set; }
        [DataMember]
        public decimal TotalBillAmount { get; set; }
        [DataMember]
        public decimal TotalDiscountAmount { get; set; }
        [DataMember]
        public decimal TotalPaidAmount { get; set; }
        [DataMember]
        public decimal TotalDueAmount { get; set; }
        [DataMember]
        public decimal TodayPaidAmount { get; set; }
        [DataMember]
        public decimal TodayDueAmount { get; set; }
        [DataMember]
        public Int32 TodayReportGenerated { get; set; }
        [DataMember]
        public Int32 TodayReportPending { get; set; }
        [DataMember]
        public Int32 TotalDelivered { get; set; }
        [DataMember]
        public Int32 TotalDeliverypending { get; set; }
        [DataMember]
        public Int32 TodayDeliverypending { get; set; }

    }
    public class InvoiceData
    {
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string BillNo { get; set; }
        [DataMember]
        public string InvoiceDate { get; set; }
        [DataMember]
        public Int32 TestCount { get; set; }
        [DataMember]
        public string TestName { get; set; }
        [DataMember]
        public Int32 TotalInvoice { get; set; }
        [DataMember]
        public Int64 NetAmount { get; set; }
        [DataMember]
        public Int64 TotalTestCount { get; set; }
        [DataMember]
        public Int64 TotalPatient { get; set; }
        [DataMember]
        public string TestCenterName { get; set; }
        [DataMember]
        public decimal PaidAmount { get; set; }
        [DataMember]
        public decimal TotalAmount { get; set; }

    }
}
