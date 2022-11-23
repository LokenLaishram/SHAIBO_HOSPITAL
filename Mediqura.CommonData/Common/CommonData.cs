using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.CommonData.Common
{
    [DataContract(Name = "Enumaction")]
    public enum Enumaction
    {
        [EnumMember]
        Select, //0
        [EnumMember]
        Insert, //1
        [EnumMember]
        Update, //2
        [EnumMember]
        Delete, //3    
		[EnumMember]
		Forward, //4   

    }
}
