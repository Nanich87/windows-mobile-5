namespace GNN.NMEAParser.Messages
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    using GNN.NMEAParser.Contracts;

    public class VTG : ISpeedMessage
    {
        public const string Name = "VTG";

        private const int Length = 10;

        public VTG(double speed)
        {
            Speed = speed;
        }

        public double Speed { get; set; }

        public static VTG Create(string message)
        {
            var fields = message.Split(',');
            if (fields.Length == Length)
            {
                try
                {
                    var speed = double.Parse(fields[7], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                    return new VTG(speed);
                }
                catch (Exception)
                {
                }
            }

            return null;
        }
    }
}