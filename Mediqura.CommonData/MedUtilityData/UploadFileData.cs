using Mediqura.CommonData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.MedUtilityData
{
    public class UploadFileData : BaseData
    {
        [DataMember]
        public Int64 fileID { get; set; }
        [DataMember]
        public string IPNo { get; set; }
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Tittle { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string ContentType { get; set; }
        [DataMember]
        public string FilePath { get; set; }
        [DataMember]
        public byte PdfDocument { get; set; }
        [DataMember]
        public DateTime DateFrom { get; set; }
        [DataMember]
        public DateTime DateTo { get; set; }
        [DataMember]
        public string Remarks { get; set; }


    }
}
