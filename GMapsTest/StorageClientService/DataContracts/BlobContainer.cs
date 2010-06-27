using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace StorageClientService.DataContracts
{
    [DataContract(Namespace="StorageClientService.DataContracts")]
    public class BlobContainer
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Uri { get; set; }
        [DataMember]
        public DateTime LastModifiedTime { get; set; }
        [DataMember]
        public string ETag { get; internal set; }
        [DataMember]
        public Dictionary<string, string> Metadata { get; internal set; }
        [DataMember]
        public ContainerAccessControl AccessControl { get; internal set; }
    }

}
