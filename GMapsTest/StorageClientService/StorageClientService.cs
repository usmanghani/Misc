using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace StorageClientService
{
    // NOTE: If you change the class name "StorageClientService" here, you must also update the reference to "StorageClientService" in App.config.
    public class StorageClientService : ServiceContracts.IStorageClientService
    {

        #region ServiceContracts.IStorageClientService Members

        List<DataContracts.BlobContainer> ServiceContracts.IStorageClientService.ListBlobContainers(string accountName, string privateKey)
        {
            return StorageClientHelper.ListBlobContainers(StorageClientHelper.GetAccountInfo(StorageType.Blob, accountName, privateKey));
        }

        List<DataContracts.BlobProperties> ServiceContracts.IStorageClientService.ListBlobs(string accountName, string privateKey, string container)
        {
            return StorageClientHelper.ListBlobs(
                StorageClientHelper.GetAccountInfo(StorageType.Blob, accountName, privateKey),
                container);
        }

        DataContracts.BlobContents ServiceContracts.IStorageClientService.GetBlobContents(string accountName, string privateKey, string container, string blob)
        {
            return StorageClientHelper.GetBlobContents(
                StorageClientHelper.GetAccountInfo(StorageType.Blob, accountName, privateKey),
                container, blob);
        }

        List<DataContracts.TableProperties> ServiceContracts.IStorageClientService.ListTables(string accountName, string privateKey)
        {
            return StorageClientHelper.ListTables(StorageClientHelper.GetAccountInfo(StorageType.Table, accountName, privateKey));
        }

        List<DataContracts.QueueProperties> ServiceContracts.IStorageClientService.ListQueues(string accountName, string privateKey)
        {
            return StorageClientHelper.ListQueues(StorageClientHelper.GetAccountInfo(StorageType.Queue, accountName, privateKey));
        }

        List<DataContracts.QueueMessage> ServiceContracts.IStorageClientService.GetQueueContents(string accountName, string privateKey, string queue, string maxNum)
        {
            try
            {
                return StorageClientHelper.GetQueueContents(
                    StorageClientHelper.GetAccountInfo(StorageType.Queue, accountName, privateKey),
                    queue, int.Parse(maxNum));
            }
            catch (FormatException)
            {
                return null;
            }
        }

        void ServiceContracts.IStorageClientService.GetTableContents(string accountName, string privateKey, string table)
        {
            throw new NotImplementedException();
        }

        string ServiceContracts.IStorageClientService.HelloWorld()
        {
            return "Hello World!";
        }

        string ServiceContracts.IStorageClientService.Echo(string echoString)
        {
            return string.Format("You said: {0}", echoString);
        }

        #endregion
    }
}
