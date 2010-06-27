using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using StorageClientService;
using StorageClientService.ServiceContracts;
using StorageClientService.DataContracts;
using System.ServiceModel.Web;

namespace StorageClientService
{
    class StorageClientServiceHost
    {
        static void Main(string[] args)
        {
            using (ServiceHost serviceHost = new ServiceHost(typeof(StorageClientService), new Uri[] { new Uri("http://localhost:23000") }))
            {
                WebHttpBinding binding = new WebHttpBinding(WebHttpSecurityMode.None);
                serviceHost.AddServiceEndpoint(typeof(IStorageClientService), binding, "http://localhost:23000");
                serviceHost.Open();
                Console.WriteLine("Listening on port 23000...");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                serviceHost.Close();
            }
        }
    }
}
