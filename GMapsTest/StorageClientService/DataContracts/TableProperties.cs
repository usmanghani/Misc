using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorageClientService.DataContracts
{
    using System.Runtime.Serialization;
    [DataContract(Namespace="StorageClientService.DataContracts")]
    public class TableProperties
    {
        [DataMember]
        public string Name { get; set; }
    }
}
