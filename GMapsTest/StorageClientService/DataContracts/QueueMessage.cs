using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorageClientService.DataContracts
{
    using System.Runtime.Serialization;
    [DataContract(Namespace="StorageClientService.DataContracts")]
    public class QueueMessage
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public DateTime InsertionTime { get; set; }
        [DataMember]
        public DateTime ExpirationTime { get; set; }
        [DataMember]
        public DateTime TimeNextVisible { get; set; }
        [DataMember]
        public string PopReceipt { get; set; }
        [DataMember]
        public byte[] Contents { get; set; }

        public System.IO.Stream ContentsAsStream
        {
            get { return new System.IO.MemoryStream(this.Contents); }
        }

    }
}
