using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace OcaWCF
{
    [DataContract]
    public class UserData
    {
       
        string nickname;


        [DataMember]
        public int idUser { get; set; }
        [DataMember]
        public String Nickname { get; set; }

        public OperationContext operationContext { get; internal set; }

    }
}
