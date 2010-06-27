using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Samples.ServiceHosting.StorageClient;
using DotFermion.Qiblah;

namespace TableStorageTest
{
    public class LocationCacheRecord : TableStorageEntity
    {
        private string query = string.Empty;
        public string Query
        {
            get
            {
                return query;
            }
            set 
            { 
                SetDataServiceKey(value); 
                this.query = value; 
            }
        }
        public List<Placemark> Placemarks { get; set; }
        private void SetDataServiceKey(string query)
        {            
            this.PartitionKey = query[0].ToString();
            this.RowKey = query;
            this.Timestamp = DateTime.UtcNow;
        }
    }
}
