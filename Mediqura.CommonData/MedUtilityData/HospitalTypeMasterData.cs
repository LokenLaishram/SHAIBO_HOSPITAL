using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class HospitalTypeMasterData : BaseData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string HospitalTypeCode { get; set; }
        [DataMember]
        public string HospitalName { get; set; }
        [DataMember]
        public string HospitalAddress { get; set; }
        //[DataMember]
        //public byte[] Logoimage { get; set; }   
        //[DataMember]
        //public string LogoLocationimage { get; set; }
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public int StateID { get; set; }
        [DataMember]
        public string StateName { get; set; }
        [DataMember]
        public int DistrictID { get; set; }
        [DataMember]
        public string DistrictName { get; set; }
        [DataMember]
        public int PinNo { get; set; }
        [DataMember]
        public string Website { get; set; }
        [DataMember]
        public string EmailID { get; set; }
        [DataMember]
        public string RecognitionNo { get; set; }
        [DataMember]
        public string PhoneNo { get; set; }
        [DataMember]
        public string MobileNo { get; set; }
        [DataMember]
        public string FaxNo { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string EmpName { get; set; }
    }
}
