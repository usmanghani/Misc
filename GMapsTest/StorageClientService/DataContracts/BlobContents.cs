using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorageClientService.DataContracts
{
    using System.Runtime.Serialization;
    [DataContract(Namespace="StorageClientService.DataContracts")]
    public class BlobContents
    {
        [DataMember]
        public byte[] Contents { get; set; }

        public System.IO.Stream AsStream
        {
            get { return new System.IO.MemoryStream(this.Contents); }
        }
    }
}
