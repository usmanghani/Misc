using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Samples.ServiceHosting.StorageClient;

namespace DotFermion.LibCache
{
    public enum StorageType
    {
        Blob, Queue, Table
    }

    public abstract class CacheStore
    {
        protected StorageAccountInfo storageAccountInfo = null;
        protected CacheStore(string accountName, string sharedKey, string endPointUri, bool? usePathStyleUris)
        {
            storageAccountInfo = new StorageAccountInfo(new Uri(endPointUri), usePathStyleUris, accountName, sharedKey);
        }

        public static CacheStore Create(string accountName, string sharedKey, string endpointUri, bool? usePathStyleUris, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Blob:
                    XStoreBlobCacheStore store = new XStoreBlobCacheStore(accountName, sharedKey, endpointUri, usePathStyleUris);
                    return store;
                case StorageType.Queue:
                case StorageType.Table:
                    return null;
                default:
                    return null;        
            }

        }

        public abstract bool OpenOrCreate(string cacheName, string cacheAccessControl);
        public abstract bool Write(string key, byte[] data, bool overwrite);
        public abstract byte[] Read(string key);
        public abstract bool Contains(string key);
        public abstract IEnumerable<KeyValuePair<string, byte[]>> Items { get; }
    }
}
