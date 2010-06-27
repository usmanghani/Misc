using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotFermion.Qiblah;
using Microsoft.Samples.ServiceHosting.StorageClient;

namespace TableStorageTest
{
    public class LocationCacheDataServiceContext : TableStorageDataServiceContext
    {
        public LocationCacheDataServiceContext(StorageAccountInfo info)
            : base(info)
        {
        }
        public IQueryable<LocationCacheRecord> Cache
        {
            get
            {
                return this.CreateQuery<LocationCacheRecord>("LocationCache");
            }
        }

        public void AddLocation(string query, List<Placemark> placemarks)
        {
            this.AddObject("LocationCache", new LocationCacheRecord { Query = query, Placemarks = placemarks });
            this.SaveChanges();
        }
    }
}
