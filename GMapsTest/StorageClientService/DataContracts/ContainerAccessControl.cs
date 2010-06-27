using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace StorageClientService.DataContracts
{
    [DataContract(Namespace = "StorageClientService.DataContracts")]
    public enum ContainerAccessControl
    {
        [EnumMember]
        Private,
        [EnumMember]
        Public
    }
}
