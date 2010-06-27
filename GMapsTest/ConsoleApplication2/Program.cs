using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StorageClientService;
using StorageClientService.ServiceContracts;
using StorageClientService.DataContracts;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            WebHttpBinding webHttpBinding = new WebHttpBinding();
            webHttpBinding.SendTimeout = TimeSpan.FromHours(1.0);
            WebChannelFactory<IStorageClientService> channelFactory = new WebChannelFactory<IStorageClientService>(webHttpBinding, new Uri("http://localhost:23000/"));
            Console.WriteLine("Press Enter when the service has come up...");
            Console.ReadLine();
            IStorageClientService channel = channelFactory.CreateChannel();
            string accountName = "dotfermion";
            string privateKey = "KCqIfQyvQbtje9IoNgXp6GXxHwTGVW+NjlkaqncqDUJOcqpLP9YYo0RAM+WExI+v8XwTiICKXx1tx0o+H1p5ZA==";
            Console.WriteLine("BLOB CONTAINERS");
            List<BlobContainer> containerList = channel.ListBlobContainers(accountName, privateKey);
            foreach (var c in containerList)
            {
                Console.WriteLine("=={0}==", c.Name);
                Console.WriteLine("Last Modified: {0}", c.LastModifiedTime);
                Console.WriteLine("Uri: {0}", c.Uri);
                Console.WriteLine("Access Control: {0}", c.AccessControl);
                Console.WriteLine("ETag: {0}", c.ETag);
                Console.WriteLine("Metadata: ");
                foreach (var m in c.Metadata)
                {
                    Console.WriteLine(m.Key + " => " + m.Value);
                }
                Console.WriteLine("BLOBS in {0}", c.Name);
                var blobs = channel.ListBlobs(accountName, privateKey, c.Name);
                foreach (var b in blobs)
                {
                    Console.WriteLine("--{0}--", b.Name);
                    Console.WriteLine("Last Modified: {0}", b.LastModifiedTime);
                    Console.WriteLine("Uri: {0}", b.Uri);
                    Console.WriteLine("Content Encoding: {0}", b.ContentEncoding);
                    Console.WriteLine("Content Language: {0}", b.ContentLanguage);
                    Console.WriteLine("Content Type: {0}", b.ContentType);
                    Console.WriteLine("Content Length: {0}", b.ContentLength);
                    Console.WriteLine("ETag: {0}", b.ETag);
                    Console.WriteLine("Metadata: ");
                    foreach (var m in b.Metadata)
                    {
                        Console.WriteLine(m.Key + " => " + m.Value);
                    }
                    Console.WriteLine("BLOB CONTENTS");
                    BlobContents contents = channel.GetBlobContents(accountName, privateKey, c.Name, b.Name);
                    System.IO.StreamReader reader = new System.IO.StreamReader(contents.AsStream);
                    Console.WriteLine(reader.ReadToEnd());
                }
            }

            Console.WriteLine("QUEUES");
            var queues = channel.ListQueues(accountName, privateKey);
            foreach (var q in queues)
            {
                Console.WriteLine("=={0}==", q.Name);
                Console.WriteLine("Uri: {0}", q.Uri);
                Console.WriteLine("Approximate Message Count: {0}", q.ApproximateMessageCount);
                Console.WriteLine("Metadata: ");
                foreach (var m in q.Metadata)
                {
                    Console.WriteLine(m.Key + " => " + m.Value);
                }
                List<QueueMessage> msgs = channel.GetQueueContents(accountName, privateKey, q.Name, q.ApproximateMessageCount.ToString());
                foreach (var m in msgs)
                {
                    System.IO.StreamReader reader = new System.IO.StreamReader(m.ContentsAsStream);
                    Console.WriteLine(reader.ReadToEnd());
                }
            }

            Console.WriteLine("TABLES");
            var tables = channel.ListQueues(accountName, privateKey);
            foreach (var t in tables)
            {
                Console.WriteLine("=={0}==", t.Name);
            }

            Console.WriteLine(channel.Echo("blah"));
            Console.WriteLine(channel.HelloWorld());
            Console.ReadLine();
        }
    }
}
