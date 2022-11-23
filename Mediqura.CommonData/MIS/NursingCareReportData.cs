﻿using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MIS
{
    public class NursingCareReportData : BaseData
    {
        [DataMember]
        public int ServiceID { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public int IsSubHeading { get; set; }
        [DataMember]
        public DateTime AddedDTM { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public Decimal amount { get; set; }
        [DataMember]
        public Decimal discount { get; set; }
        [DataMember]
        public Decimal TotalAmount { get; set; }
        [DataMember]
        public Decimal TotalDiscount { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string PatientType { get; set; }
        [DataMember]
        public Decimal NetAmount { get; set; }
        [DataMember]
        public string Doctor { get; set; }
        [DataMember]
        public int PatientTypeID { get; set; }
        [DataMember]
        public Decimal ServiceCharge { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public Decimal NetServiceCharge { get; set; }
        [DataMember]
        public Decimal Share { get; set; }
    

    }
    public class NursingCareReportDataTOeXCEL
    {
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public String PatientName { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public string PatientType { get; set; }
        [DataMember]
        public DateTime AddedDTM { get; set; }
        [DataMember]
        public string Doctor { get; set; }
        [DataMember]
        public Decimal NetAmount { get; set; }
    }
}