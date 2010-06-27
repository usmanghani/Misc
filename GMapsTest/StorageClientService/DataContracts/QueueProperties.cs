using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorageClientService.DataContracts
{
    using System.Runtime.Serialization;

    [DataContract(Namespace="StorageClientService.DataContracts")]
    public class QueueProperties
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Uri { get; set; }
        [DataMember]
        public int ApproximateMessageCount { get; set; }
        [DataMember]
        public Dictionary<string, string> Metadata { get; set; }
    }
}
