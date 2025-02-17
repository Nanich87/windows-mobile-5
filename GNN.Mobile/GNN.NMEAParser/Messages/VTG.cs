namespace GNN.NMEAParser.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using GNN.NMEAParser.Contracts;

    public class VTG : ISpeedMessage
    {
        public const string Name = "VTG";

        private const int SpeedIndex = 7;

        public VTG(double speed)
        {
            Speed = speed;
        }

        public double Speed { get; set; }

        public static VTG Create(string message)
        {
            var fields = message.Split(',');

            if (SpeedIndex < fields.Length && !string.IsNullOrEmpty(fields[SpeedIndex]))
            {
                var speed = double.Parse(fields[SpeedIndex], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                return new VTG(speed);
            }

            return null;
        }
    }
}