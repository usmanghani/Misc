using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Configuration;
using Microsoft.Samples.ServiceHosting.StorageClient;
using DotFermion.Qiblah;

namespace TableStorageTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //StorageAccountInfo info = null;
        //BlobStorage blobStorage = null;
        //BlobContainer container = null;
        //LocationCacheDataServiceContext cache = null;
        DotFermion.LibCache.LocationCache cache = new DotFermion.LibCache.LocationCache();
        List<DotFermion.LibCache.LocationCacheRecord> IMOS = new List<DotFermion.LibCache.LocationCacheRecord>();
        private void btnCreateTables_Click(object sender, EventArgs e)
        {
            //info = StorageAccountInfo.GetDefaultTableStorageAccountFromConfiguration(true);
            //info = StorageAccountInfo.GetDefaultBlobStorageAccountFromConfiguration(true);
            //info = new StorageAccountInfo(new Uri(Properties.Settings.Default.BlobStorageEndpoint), null, Properties.Settings.Default.AccountName, Properties.Settings.Default.AccountSharedKey);
            //info.CheckComplete();
            //blobStorage = BlobStorage.Create(info);
            //container = blobStorage.GetBlobContainer("locationcache");
            //if (!container.DoesContainerExist())
            //{
            //    container.CreateContainer();
            //}
            //container.SetContainerAccessControl(ContainerAccessControl.Public);
            //TableStorage storage = TableStorage.Create(info);
            //if (!storage.DoesTableExist("LocationCache"))
            //    storage.CreateTable("LocationCache");
            //TableStorage.CreateTablesFromModel(typeof(LocationCacheDataServiceContext), info);
            //cache = new LocationCacheDataServiceContext(info);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            GeoCoder geoCoder = new GeoCoder(Properties.Settings.Default.GoogleMapsApiKey);
            geoCoder.Address = txtQuery.Text;
            geoCoder.SensorUsed = false;
            List<Placemark> placemarks = geoCoder.FetchExtendedGeoCodeData();
            DotFermion.LibCache.LocationCacheRecord cacheRecord = new DotFermion.LibCache.LocationCacheRecord() { Query = txtQuery.Text, Placemarks = placemarks };
            IMOS.Add(cacheRecord);
            lstResults.Items.Clear();
            foreach (var placemark in placemarks)
            {
                lstResults.Items.Add(placemark.Address);
            }

        }

        private void btnAddToCache_Click(object sender, EventArgs e)
        {
            List<int> purgedIndexes = new List<int>();
            var query = IMOS.Select((lcr, index) => new { LCR = lcr, Index = index });
            foreach (var lcr in query)
            {
                cache.Write(lcr.LCR.Query, lcr.LCR);
                ////cache.AddLocation(lcr.Query, lcr.Placemarks);
                //BlobProperties properties = new BlobProperties(lcr.LCR.Query);
                ////properties.ContentEncoding = "UTF-8";
                ////properties.ContentType = "text/xml";
                //BinaryFormatter formatter = new BinaryFormatter();
                //System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //formatter.Serialize(ms, lcr.LCR);
                //BlobContents contents = new BlobContents(ms.ToArray());
                //container.CreateBlob(properties, contents, true);
                purgedIndexes.Add(lcr.Index);
                //ms.Close();
            }
            foreach (var i in purgedIndexes)
            {
                IMOS.RemoveAt(i);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            //for (int i = 0; i < 10000; i++)
            //{
            //    long ticks = System.DateTime.UtcNow.Ticks;
            //}
            //stopWatch.Stop();
            //MessageBox.Show(stopWatch.ElapsedTicks.ToString());
            //stopWatch.Reset();
            //stopWatch.Start();
            //for (int i = 0; i < 10000; i++)
            //{
            //    long ticks = Environment.TickCount;
            //}
            //stopWatch.Stop();
            //MessageBox.Show(stopWatch.ElapsedTicks.ToString());
            //stopWatch.Reset();
            //stopWatch.Start();
            //for (int i = 0; i < 10000; i++)
            //{
            //    long ticks = DateTime.Now.Ticks;
            //}
            //stopWatch.Stop();
            //MessageBox.Show(stopWatch.ElapsedTicks.ToString());

            //StorageAccountInfo storageAccountInfo = new StorageAccountInfo(new Uri(Properties.Settings.Default.BlobStorageEndpoint), null, Properties.Settings.Default.AccountName, Properties.Settings.Default.AccountSharedKey);
            //storageAccountInfo.CheckComplete();
            //System.Collections.Specialized.NameValueCollection nvColl = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationManager.GetSection("appSettings");
            //MessageBox.Show(nvColl["greeting"]);
        }

        private void btnReadFromCache_Click(object sender, EventArgs e)
        {
            lstResults.Items.Clear();
            foreach (var kvp in cache.Items)
            {
                lstResults.Items.Add(kvp.Value.Query + "==>" + kvp.Value.Placemarks[0].Address);
            }

            //foreach (var b in container.ListBlobs(string.Empty, false))
            //{
            //    BlobProperties bp = (BlobProperties)b;
            //    lstResults.Items.Add(bp.Uri);
            //    System.Net.WebClient client = new System.Net.WebClient ( );
            //    byte[] blobData = client.DownloadData(bp.Uri);
            //    BinaryFormatter formatter = new BinaryFormatter();
            //    LocationCacheRecord lcr = (LocationCacheRecord)formatter.Deserialize(new System.IO.MemoryStream(blobData));
            //    lstResults.Items.Add(lcr.Query + "==>" + lcr.Placemarks[0].Address);
            //    //string contents = new System.IO.StreamReader(System.Net.WebRequest.Create(bp.Uri).GetResponse().GetResponseStream()).ReadToEnd();
            //    //contents = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.UTF8.GetBytes(contents));
            //    //lstResults.Items.Add(contents);
            //}
        }
    }
}
