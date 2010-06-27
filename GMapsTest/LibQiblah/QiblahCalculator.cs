using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotFermion.Qiblah
{
    public class QiblahCalculator
    {
        public QiblahCalculator(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Qiblah
        {
            get
            {
                return calculateQiblah(this.Latitude, this.Longitude);
            }
        }
        private double toDeg(double radians)
        {
            return radians * 180 / Math.PI;
        }
        private double toRad(double degrees)
        {
            return degrees * Math.PI / 180;
        }
        private double calculateQiblah(double latitude, double longitude)
        {
            double lambdaK = 39.823333;
            double lambda = longitude;
            double phiK = 21.423333;
            double phi = latitude;

            double numerator = Math.Sin(toRad(lambdaK) - toRad(lambda));
            double denominator = Math.Cos(toRad(phi)) * Math.Tan(toRad(phiK)) - Math.Sin(toRad(phi)) * Math.Cos(toRad(lambdaK) - toRad(lambda));
            double q = toDeg(Math.Atan2(numerator, denominator));
            return q;
        }
    }
}
