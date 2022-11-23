using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedDashboardData
{
   public class CompanyDashboardData
    {
        [DataMember]
        public string IncomeCurrent { get; set; }

        [DataMember]
        public string IncomePrevious { get; set; }
        [DataMember]
        public string AdvanceCurrent { get; set; }
        [DataMember]
        public string AdvancePrevious { get; set; }
        [DataMember]
        public string ExpensesCurrent { get; set; }
        [DataMember]
        public string ExpensesPrevious { get; set; }
        [DataMember]
        public string BalanceCurrent { get; set; }
        [DataMember]
        public string BalancePrevious { get; set; }
        [DataMember]
        public string Top1EarningValue { get; set; }
        [DataMember]
        public string Top1EarningName { get; set; }
        [DataMember]
        public string Top2EarningValue { get; set; }
        [DataMember]
        public string Top2EarningName { get; set; }
        [DataMember]
        public string Top3EarningValue { get; set; }
        [DataMember]
        public string Top3EarningName { get; set; }
        [DataMember]
        public string Top4EarningValue { get; set; }
        [DataMember]
        public string Top4EarningName { get; set; }
        [DataMember]
        public string TotalBed { get; set; }
        [DataMember]
        public string BedOccupied { get; set; }
        [DataMember]
        public string BedVacant { get; set; }
        [DataMember]
        public string OPD { get; set; }
        [DataMember]
        public string OT { get; set; }
        [DataMember]
        public string ER { get; set; }
        [DataMember]
        public string IPD { get; set; }
        [DataMember]
        public string NewPatient { get; set; }
        [DataMember]
        public string OldPatient { get; set; }
        [DataMember]
        public string Admitted { get; set; }
        [DataMember]
        public string Discharge { get; set; }
        [DataMember]
        public string Discount { get; set; }
    }
}
