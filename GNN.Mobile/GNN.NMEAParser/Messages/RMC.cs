namespace GNN.NMEAParser.Messages
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    using GNN.NMEAParser.Contracts;

    public class RMC : ISpeedMessage, ILocationMessage, IDateTimeMessage
    {
        public const string Name = "RMC";

        private const int TimeIndex = 1;
        private const int PositionStatusIndex = 2;
        private const int LatitudeIndex = 3;
        private const int LongitudeIndex = 5;
        private const int SpeedIndex = 7;
        private const int DateIndex = 9;

        public RMC(double latitude, double longitude, double speed, DateTime dateTime)
        {
            Latitude = latitude;
            Longitude = longitude;
            Speed = speed;
            DateTime = dateTime;
        }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Speed { get; set; }

        public DateTime DateTime { get; set; }

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
            DateTime date = DateTime.MinValue;
            TimeSpan time = TimeSpan.MinValue;

            var fields = message.Split(',');

            if (TimeIndex < fields.Length && fields[TimeIndex].Length >= 6)
            {
                var hours = TimeSpan.FromHours(int.Parse(fields[TimeIndex].Substring(0, 2)));
                var minutes = TimeSpan.FromMinutes(int.Parse(fields[TimeIndex].Substring(2, 2)));
                var seconds = TimeSpan.FromSeconds(double.Parse(fields[TimeIndex].Substring(4, 2)));
                time = hours.Add(minutes).Add(seconds);
            }

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

            if (SpeedIndex < fields.Length && !string.IsNullOrEmpty(fields[SpeedIndex]))
            {
                var knots = double.Parse(fields[SpeedIndex], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                speed = knots * 1.852;
            }

            if (DateIndex < fields.Length && fields[DateIndex].Length == 6)
            {
                date = DateTime.ParseExact(fields[DateIndex], "ddMMyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
                date = date.Add(time);
            }

            return new RMC(latitude, longitude, speed, date);
        }
    }
}