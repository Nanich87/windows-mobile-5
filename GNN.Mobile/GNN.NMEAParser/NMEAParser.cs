namespace GNN.NMEAParser
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;

    public sealed class NMEAParser
    {
        private static NMEAParser instance;

        private string message = string.Empty;

        private double speed;

        private NMEAParser()
        {
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

        public double GetSpeed()
        {
            return speed;
        }

        private void ProcessMessage(string message)
        {
            if (message.Length < 6 || message[0] != '$')
            {
                return;
            }

            var messageType = message.Substring(3, 3);
            switch (messageType)
            {
                case "VTG":
                    {
                        ParseVTG(message);
                        break;
                    }
            }
        }

        private void ParseVTG(string message)
        {
            var fields = message.Split(',');
            if (fields.Length == 10)
            {
                try
                {
                    speed = double.Parse(fields[7], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}