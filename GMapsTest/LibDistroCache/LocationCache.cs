using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotFermion.LibCache
{
    public class LocationCache
    {
        CacheStore cache = null;
        public LocationCache()
        {
            System.Collections.Specialized.NameValueCollection appSettings = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationManager.GetSection("appSettings");
            string accountName = appSettings["AccountName"];
            string accountSharedKey = appSettings["AccountSharedKey"];
            string storageEndPoint = appSettings["BlobStorageEndpoint"];
            cache = CacheStore.Create(accountName, accountSharedKey, storageEndPoint, null, StorageType.Blob);
            cache.OpenOrCreate("locationcache", "public");
        }
        public void Write(string key, LocationCacheRecord lcr)
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            formatter.Serialize(ms, lcr);
            cache.Write(key, ms.ToArray(), true);
        }
        public LocationCacheRecord Read(string key)
        {
            byte[] buffer = cache.Read(key);
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            LocationCacheRecord lcr = (LocationCacheRecord)formatter.Deserialize(new System.IO.MemoryStream(buffer));
            return lcr;
        }
        public bool Contains(string key)
        {
            return cache.Contains(key);
        }

        public IEnumerable<KeyValuePair<string, LocationCacheRecord>> Items
        {
            get
            {
                foreach (var kvp in cache.Items)
                {
                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(kvp.Value);
                    LocationCacheRecord lcr = null;
                    try
                    {
                        lcr = (LocationCacheRecord)formatter.Deserialize(ms);
                    }
                    catch (InvalidCastException ex)
                    {
                        continue;
                    }
                    yield return new KeyValuePair<string, LocationCacheRecord>(kvp.Key, lcr);
                }

            }
        }

    }
}
