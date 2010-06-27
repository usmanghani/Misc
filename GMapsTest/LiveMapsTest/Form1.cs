using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LiveMapsTest.LiveMapsTokenService;
using LiveMapsTest.LiveMapsGeoCoderService;

namespace LiveMapsTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CommonService tokenService = new CommonService();
            tokenService.Credentials = new System.Net.NetworkCredential("137871", "Mugga25.");
            TokenSpecification tokenSpecs = new TokenSpecification();
            tokenSpecs.ClientIPAddress = "0.0.0.0   ";
            tokenSpecs.TokenValidityDurationMinutes = 60;
            string token = tokenService.GetClientToken(tokenSpecs);
            textBox1.AppendText(token + Environment.NewLine);
            GeocodeRequest request = new GeocodeRequest();
            request.Credentials = new Credentials();
            request.Credentials.Token = token;
            request.Query = "1234";
            GeocodeOptions options = new GeocodeOptions();
            ConfidenceFilter[] filters = new ConfidenceFilter[] { new ConfidenceFilter() { MinimumConfidence = Confidence.Low } };
            options.Filters = filters;
            request.Options = options;
            GeocodeServiceClient geoCoder = new GeocodeServiceClient("BasicHttpBinding_IGeocodeService");
            GeocodeResponse response = geoCoder.Geocode(request);
            foreach (var result in response.Results)
            {
                textBox1.AppendText("Display Name: " + result.DisplayName + Environment.NewLine);
                textBox1.AppendText("Confidence: " + result.Confidence.ToString() + Environment.NewLine);
                textBox1.AppendText("Entity Type: " + result.EntityType + Environment.NewLine);
                foreach (var location in result.Locations)
                {
                    textBox1.AppendText("Longitude: " + location.Longitude + Environment.NewLine);
                    textBox1.AppendText("Latitude: " + location.Latitude + Environment.NewLine);
                    textBox1.AppendText("Altitude: " + location.Altitude + Environment.NewLine);
                }
            }

            //LiveMapsTokenService.CommonServiceSoapClient tokenService = new LiveMapsTest.LiveMapsTokenService.CommonServiceSoapClient("CommonServiceSoap");
            //GetClientTokenRequest tokenRequest = new GetClientTokenRequest();
            //tokenService.ClientCredentials.UserName.UserName = "137871";
            //tokenService.ClientCredentials.UserName.Password = "Mugga25.";
            
        }
    }
}
