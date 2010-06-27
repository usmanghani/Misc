using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using StorageClientService;
using StorageClientService.ServiceContracts;
using StorageClientService.DataContracts;

using System.ServiceModel;
using System.ServiceModel.Web;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebServiceHost serviceHost = new WebServiceHost(typeof(StorageClientService.StorageClientService)))
            {
                //serviceHost.AddServiceEndpoint(typeof(IStorageClientService), new WebHttpBinding(), "");
                serviceHost.Open();
                Console.WriteLine("Listening on port 23000...");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                serviceHost.Close();
            }
        }
    }
}
