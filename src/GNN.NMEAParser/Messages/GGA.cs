namespace GNN.NMEAParser.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using GNN.NMEAParser.Contracts;

    public class GGA : ILocationMessage, IElevationMessage
    {
        public const string Name = "GGA";

        private const int AltitudeIndex = 9;

        public GGA(double altitude)
        {
            Latitude = double.NaN;
            Longitude = double.NaN;
            Altitude = altitude;
        }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Altitude { get; set; }

        public static GGA Create(string message)
        {
            var fields = message.Split(',');

            if (AltitudeIndex < fields.Length && !string.IsNullOrEmpty(fields[AltitudeIndex]))
            {
                var altitude = double.Parse(fields[AltitudeIndex], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                return new GGA(altitude);
            }

            return null;
        }
    }
}