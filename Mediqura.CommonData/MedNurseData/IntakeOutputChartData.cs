using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedNurseData
{
    public class IntakeOutputChartData : BaseData
    {
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string UHIDs { get; set; }
        [DataMember]
        public Int64 UHID { get; set; }
        [DataMember]
        public string WardBedNo { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public double fluids { get; set; }
        [DataMember]
        public DateTime oralstart { get; set; }
        [DataMember]
        public DateTime oralend { get; set; }
        [DataMember]
        public double oral { get; set; }
        [DataMember]
        public DateTime urinestart { get; set; }
        [DataMember]
        public DateTime urineend { get; set; }
        [DataMember]
        public double urine { get; set; }
        [DataMember]
        public double others { get; set; }
        [DataMember]
        public double totalfluids { get; set; }
        [DataMember]
        public double totaloral { get; set; }        
        [DataMember]
        public double totalurine { get; set; }
        [DataMember]
        public double totalothers { get; set; }
        [DataMember]
        public double totalintakechart { get; set; }
        [DataMember]
        public double totaloutputchart { get; set; }
        [DataMember]
        public double totalbalancechart { get; set; }
        [DataMember]
        public string remarks { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public string Sex { get; set; }
        [DataMember]
        public DateTime DOA { get; set; }
        [DataMember]
        public DateTime today { get; set; }
        [DataMember]
        public string Doctor { get; set; }
        [DataMember]
        public Int32 RowNo { get; set; }
        [DataMember]
        public DateTime IntakeOutputDate { get; set; }
        [DataMember]
        public DateTime fluidsstart { get; set; }
        [DataMember]
        public DateTime fluidsend { get; set; }
        [DataMember]
        public DateTime date { get; set; }
        [DataMember]
        public Int32 searchby { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }

    }

}
