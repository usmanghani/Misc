using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace StorageClientService.DataContracts
{
    [DataContract(Namespace="StorageClientService.DataContracts")]
    public class BlobProperties
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Uri { get; set; }
        [DataMember]
        public DateTime LastModifiedTime { get; set; }
        [DataMember]
        public string ETag { get; set; }
        [DataMember]
        public string ContentType { get; set; }
        [DataMember]
        public long ContentLength { get; set; }
        [DataMember]
        public string ContentEncoding { get; set; }
        [DataMember]
        public string ContentLanguage { get; set; }
        [DataMember]
        public Dictionary<string, string> Metadata { get; set; }
    }
}
