using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace StorageClientService.ServiceContracts
{
    [ServiceKnownType(typeof(DataContracts.ContainerAccessControl))]
    [ServiceKnownType(typeof(DataContracts.BlobContainer))]
    [ServiceKnownType(typeof(DataContracts.BlobProperties))]
    [ServiceKnownType(typeof(DataContracts.BlobContents))]
    [ServiceKnownType(typeof(DataContracts.QueueProperties))]
    [ServiceKnownType(typeof(DataContracts.QueueMessage))]
    [ServiceKnownType(typeof(DataContracts.TableProperties))]
    [ServiceContract(Namespace="StorageClientService.ServiceContracts")]
    public interface IStorageClientService
    {
        [OperationContract]
        [WebGet(UriTemplate="/{AccountName}/{PrivateKey}/Containers")]
        List<DataContracts.BlobContainer> ListBlobContainers(string accountName, string privateKey);
        [OperationContract]
        [WebGet(UriTemplate="/{AccountName}/{PrivateKey}/{Container}/Blobs")]
        List<DataContracts.BlobProperties> ListBlobs(string accountName, string privateKey, string container);
        [OperationContract]
        [WebGet(UriTemplate="/{AccountName}/{PrivateKey}/Queues")]
        List<DataContracts.QueueProperties> ListQueues(string accountName, string privateKey);
        [OperationContract]
        [WebGet(UriTemplate = "/{AccountName}/{PrivateKey}/Tables")]
        List<DataContracts.TableProperties> ListTables(string accountName, string privateKey);
        [OperationContract]
        [WebGet(UriTemplate = "/{AccountName}/{PrivateKey}/{Container}/{Blob}/Contents")]
        DataContracts.BlobContents GetBlobContents(string accountName, string privateKey, string container, string blob);
        [OperationContract]
        [WebGet(UriTemplate="/{AccountName}/{PrivateKey}/{Queue}/Messages/{maxNum}")]
        List<DataContracts.QueueMessage> GetQueueContents(string accountName, string privateKey, string queue, string maxNum);
        [OperationContract]
        [WebGet(UriTemplate="/{AccountName}/{PrivateKey}/{Table}/Rows")]
        void GetTableContents(string accountName, string privateKey, string table);
        [OperationContract]
        [WebGet(UriTemplate = "/Echo/{echoString}")]
        string Echo(string echoString);
        [OperationContract]
        [WebGet(UriTemplate = "/HelloWorld")]
        string HelloWorld();
    }
}
