using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TableStorageTest
{

    public interface ISerializable
    {
    }
    
    public interface IXmlSerializable : ISerializable
    {
    }
    [Serializable]
    public interface IBinarySerializable : ISerializable
    {
    }
}
