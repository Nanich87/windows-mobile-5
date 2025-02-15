namespace GNN.NMEAParser
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using GNN.NMEAParser.Contracts;
    using GNN.NMEAParser.Factories;

    public sealed class NMEAParser
    {
        private static NMEAParser instance;

        private MessageFactory factory;

        private string message = string.Empty;

        private DateTime dateTime;
        private double latitude;
        private double longitude;
        private double speed;

        private NMEAParser()
        {
            factory = new MessageFactory();
        }

        public static NMEAParser Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NMEAParser();
                }

                return instance;
            }
        }

        public void Parse(string nmea)
        {
            var lines = new List<string>();

            message += nmea;

            var lineEnd = message.IndexOf('\n');
            while (lineEnd > -1)
            {
                var line = message.Substring(0, lineEnd);
                message = message.Substring(lineEnd + 1);

                if (!string.IsNullOrEmpty(line))
                {
                    lines.Add(line);
                }

                lineEnd = message.IndexOf('\n');
            }

            foreach (var line in lines)
            {
                ProcessMessage(line);
            }
        }

        public double GetLatitude()
        {
            return latitude;
        }

        public double GetLongitude()
        {
            return longitude;
        }

        public double GetSpeed()
        {
            return speed;
        }

        public DateTime GetDateTime()
        {
            return dateTime;
        }

        private void ProcessMessage(string line)
        {
            var message = factory.CreateMessage(line);

            if (message is IDateTimeMessage)
            {
                dateTime = ((IDateTimeMessage)message).DateTime;
            }

            if (message is ILocationMessage)
            {
                latitude = ((ILocationMessage)message).Latitude;
                longitude = ((ILocationMessage)message).Longitude;
            }

            if (message is ISpeedMessage)
            {
                speed = ((ISpeedMessage)message).Speed;
            }
        }
    }
}