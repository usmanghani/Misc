using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorageClientService
{
    using Storage = Microsoft.Samples.ServiceHosting.StorageClient;

    internal enum StorageType
    {
        Blob, Queue, Table
    }

    internal class StorageClientHelper
    {
        internal static string BlobUri
        {
            get
            {
                return "http://blob.core.windows.net";
            }
        }
        internal static string QueueUri
        {
            get
            {
                return "http://queue.core.windows.net";
            }
        }
        internal static string TableUri
        {
            get
            {
                return "http://table.core.windows.net";
            }
        }

        internal static Storage.StorageAccountInfo GetAccountInfo(StorageType storageType, string accountName, string privateKey)
        {
            Uri baseUri = null;

            switch (storageType)
            {
                case StorageType.Blob:
                    baseUri = new Uri(BlobUri);
                    break;
                case StorageType.Table:
                    baseUri = new Uri(TableUri);
                    break;
                case StorageType.Queue:
                    baseUri = new Uri(QueueUri);
                    break;
            }

            Storage.StorageAccountInfo accountInfo = new Storage.StorageAccountInfo(baseUri, null, accountName, privateKey);
            return accountInfo;
        }

        internal static List<DataContracts.BlobContainer> ListBlobContainers(Storage.StorageAccountInfo accountInfo)
        {
            Storage.BlobStorage storage = Storage.BlobStorage.Create(accountInfo);
            List<DataContracts.BlobContainer> result = new List<global::StorageClientService.DataContracts.BlobContainer>();
            foreach (var bc in storage.ListBlobContainers())
            {
                Storage.ContainerProperties props = bc.GetContainerProperties();
                Storage.ContainerAccessControl ac = bc.GetContainerAccessControl();
                DataContracts.BlobContainer container = new DataContracts.BlobContainer()
                {
                    Name = bc.ContainerName,
                    Uri = bc.ContainerUri.ToString(),
                    LastModifiedTime = bc.LastModifiedTime,
                    ETag = props.ETag,
                    AccessControl = (DataContracts.ContainerAccessControl)Enum.Parse(typeof(DataContracts.ContainerAccessControl), ac.ToString(), true)
                };
                container.Metadata = new Dictionary<string, string>();
                if (props.Metadata != null)
                {
                    foreach (string key in props.Metadata.Keys)
                    {
                        container.Metadata.Add(key, props.Metadata[key]);
                    }
                }
                result.Add(container);
            }
            return result;
        }

        internal static List<DataContracts.BlobProperties> ListBlobs(Storage.StorageAccountInfo accountInfo, string containerName)
        {
            Storage.BlobStorage storage = Storage.BlobStorage.Create(accountInfo);
            List<DataContracts.BlobProperties> result = new List<global::StorageClientService.DataContracts.BlobProperties>();
            Storage.BlobContainer container = storage.GetBlobContainer(containerName);
            bool doesContainerExist = container.DoesContainerExist();
            if (!doesContainerExist)
            {
                return result;
            }

            var query = from b in container.ListBlobs(string.Empty, false)
                        select b as Storage.BlobProperties;

            foreach (var b in query)
            {
                DataContracts.BlobProperties bp = new global::StorageClientService.DataContracts.BlobProperties()
                {
                    ContentEncoding = b.ContentEncoding,
                    ContentLanguage = b.ContentLanguage,
                    ContentLength = b.ContentLength,
                    ContentType = b.ContentType,
                    ETag = b.ETag,
                    LastModifiedTime = b.LastModifiedTime,
                    Name = b.Name,
                    Uri = b.Uri.ToString()
                };
                bp.Metadata = new Dictionary<string, string>();
                if (b.Metadata != null)
                {
                    foreach (string key in b.Metadata.Keys)
                    {
                        bp.Metadata.Add(key, b.Metadata[key]);
                    }
                }
                result.Add(bp);
            }

            return result;
        }

        internal static List<DataContracts.QueueProperties> ListQueues(Storage.StorageAccountInfo accountInfo)
        {
            Storage.QueueStorage storage = Storage.QueueStorage.Create(accountInfo);
            List<DataContracts.QueueProperties> result = new List<global::StorageClientService.DataContracts.QueueProperties>();

            foreach (var mq in storage.ListQueues())
            {
                Storage.QueueProperties qp = mq.GetProperties();
                DataContracts.QueueProperties q = new global::StorageClientService.DataContracts.QueueProperties()
                {
                    ApproximateMessageCount = qp.ApproximateMessageCount,
                    Name = mq.Name,
                    Uri = mq.QueueUri.ToString()
                };

                if (qp.Metadata != null)
                {
                    foreach (string key in qp.Metadata.Keys)
                    {
                        q.Metadata.Add(key, qp.Metadata[key]);
                    }
                }

                result.Add(q);
            }

            return result;
        }

        internal static List<DataContracts.TableProperties> ListTables(Storage.StorageAccountInfo accountInfo)
        {
            Storage.TableStorage storage = Storage.TableStorage.Create(accountInfo);
            List<DataContracts.TableProperties> result = new List<global::StorageClientService.DataContracts.TableProperties>();
            foreach (var t in storage.ListTables())
            {
                DataContracts.TableProperties tp = new global::StorageClientService.DataContracts.TableProperties()
                {
                    Name = t
                };
                result.Add(tp);
            }
            return result;
        }

        internal static DataContracts.BlobContents GetBlobContents(Storage.StorageAccountInfo accountInfo, string containerName, string blobName)
        {
            Storage.BlobStorage storage = Storage.BlobStorage.Create(accountInfo);
            Storage.BlobContainer container = storage.GetBlobContainer(containerName);
            if (!container.DoesContainerExist()) return null;
            if (!container.DoesBlobExist(blobName)) return null;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            Storage.BlobContents contents = new Microsoft.Samples.ServiceHosting.StorageClient.BlobContents(ms);
            container.GetBlob(blobName, contents, true);
            DataContracts.BlobContents result = new global::StorageClientService.DataContracts.BlobContents();
            result.Contents = contents.AsBytes();
            return result;
        }

        internal static List<DataContracts.QueueMessage> GetQueueContents(Storage.StorageAccountInfo accountInfo, string queueName, int maxMessages)
        {
            Storage.QueueStorage storage = Storage.QueueStorage.Create(accountInfo);
            List<DataContracts.QueueMessage> result = new List<global::StorageClientService.DataContracts.QueueMessage>();
            Storage.MessageQueue q = storage.GetQueue(queueName);
            if (!q.DoesQueueExist()) return result;
            foreach (var m in q.PeekMessages(maxMessages))
            {
                DataContracts.QueueMessage msg = new global::StorageClientService.DataContracts.QueueMessage()
                {
                    Id = m.Id,
                    InsertionTime = m.InsertionTime,
                    ExpirationTime = m.ExpirationTime,
                    TimeNextVisible = m.TimeNextVisible,
                    PopReceipt = m.PopReceipt,
                    Contents = m.ContentAsBytes()
                };
            }
            return result;
        }
    }
}
