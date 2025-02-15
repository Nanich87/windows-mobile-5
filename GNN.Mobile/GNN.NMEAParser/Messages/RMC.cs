namespace GNN.NMEAParser.Messages
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    using GNN.NMEAParser.Contracts;

    public class RMC : ISpeedMessage, ILocationMessage
    {
        public const string Name = "RMC";

        private const int PositionStatusIndex = 2;
        private const int LatitudeIndex = 3;
        private const int LongitudeIndex = 5;
        private const int SpeedIndex = 7;

        public RMC(double latitude, double longitude, double speed)
        {
            Latitude = latitude;
            Longitude = longitude;
            Speed = speed;
        }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Speed { get; set; }

        public static double ParseCoordinate(string coordinate, int degreesDigits)
        {
            var degrees = coordinate.Substring(0, degreesDigits);
            var minutes = coordinate.Substring(degreesDigits, coordinate.Length - degreesDigits);

            var degreesParsed = int.Parse(degrees);
            var minutesParsed = double.Parse(minutes, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

            return degreesParsed + minutesParsed / 60;
        }

        public static RMC Create(string message)
        {
            var isValidPosition = false;
            var latitude = 0.0;
            var longitude = 0.0;
            var speed = 0.0;

            var fields = message.Split(',');

            if (PositionStatusIndex < fields.Length)
            {
                // Position status (A = data valid, V = data invalid)
                var positionStatus = fields[PositionStatusIndex];
                if (positionStatus == "A")
                {
                    isValidPosition = true;
                }
            }

            if (!isValidPosition)
            {
                return null;
            }

            if (LatitudeIndex < fields.Length)
            {
                latitude = ParseCoordinate(fields[LatitudeIndex], 2);
            }

            if (LongitudeIndex < fields.Length)
            {
                longitude = ParseCoordinate(fields[LongitudeIndex], 3);
            }

            if (SpeedIndex < fields.Length)
            {
                var knots = double.Parse(fields[7], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                speed = knots * 1.852;
            }

            return new RMC(latitude, longitude, speed);
        }
    }
}