using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Samples.ServiceHosting.StorageClient;

namespace TableStorageTest
{
    public class XStoreBlobCacheStore : CacheStore
    {
        private BlobStorage blobStorage = null;
        private BlobContainer blobContainer = null;

        public XStoreBlobCacheStore(string accountName, string sharedKey, string blobStorageEndPoint, bool? usePathStyleUris)
            : base(accountName, sharedKey, blobStorageEndPoint, usePathStyleUris)
        {
            blobStorage = BlobStorage.Create(storageAccountInfo);
        }

        /// <summary>
        /// Opens a container or Creates if one with the given name does not exist.
        /// </summary>
        /// <param name="containerName">The name of the container to open. If the container with the given name does not exist, it will be creted.</param>
        /// <param name="accessControl">Could be public or private.</param>
        /// <returns>Success/Failure whether open/create succeeded or not.</returns>
        public override bool OpenOrCreate(string containerName, string accessControl)
        {
            bool result = true;
            blobContainer = blobStorage.GetBlobContainer(containerName);
            if (!blobContainer.DoesContainerExist())
            {
                result = blobContainer.CreateContainer();
            }
            blobContainer.SetContainerAccessControl((ContainerAccessControl)Enum.Parse(typeof(ContainerAccessControl), accessControl, true));
            return result;

        }

        public override bool Write(string key, byte[] data, bool overwrite)
        {
            BlobProperties properties = new BlobProperties(key);
            BlobContents contents = new BlobContents(data);
            return blobContainer.CreateBlob(properties, contents, overwrite);
        }

        public override byte[] Read(string key)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            BlobContents contents = new BlobContents(ms);
            try
            {
                BlobProperties properties = (BlobProperties)blobContainer.GetBlob(key, contents, false);
                if (properties == null)
                {
                    return null;
                }
            }
            catch (StorageServerException ex)
            {
                return null;
            }

            return contents.AsBytes();
        }

        public override bool Contains(string key)
        {
            return blobContainer.DoesBlobExist(key); 
        }

        public override IEnumerable<KeyValuePair<string, byte[]>> Items
        {
            get
            {
                foreach (var b in blobContainer.ListBlobs("", false))
                {
                    BlobProperties bp = (BlobProperties)b;
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    BlobContents contents = new BlobContents(ms);
                    blobContainer.GetBlob(bp.Name, contents, false);
                    yield return new KeyValuePair<string, byte[]>(bp.Name, contents.AsBytes());
                }

            }
        }
    }
}
