namespace GNN.NMEAParser
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    using GNN.NMEAParser.Factories;
    using GNN.NMEAParser.Contracts;

    public sealed class NMEAParser
    {
        private static NMEAParser instance;

        private MessageFactory factory;

        private string message = string.Empty;

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

        private void ProcessMessage(string line)
        {
            var message = factory.CreateMessage(line);

            if (message is ISpeedMessage)
            {
                speed = ((ISpeedMessage)message).Speed;
            }

            if (message is ILocationMessage)
            {
                latitude = ((ILocationMessage)message).Latitude;
                longitude = ((ILocationMessage)message).Longitude;
            }
        }
    }
}