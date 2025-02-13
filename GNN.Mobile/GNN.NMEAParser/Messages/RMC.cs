namespace GNN.NMEAParser.Messages
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    using GNN.NMEAParser.Contracts;

    public class RMC : ISpeedMessage
    {
        public const string Name = "RMC";

        private const int Length = 13;

        public RMC(double speed)
        {
            Speed = speed;
        }

        public double Speed { get; set; }

        public static RMC Create(string message)
        {
            var fields = message.Split(',');
            if (fields.Length == Length)
            {
                try
                {
                    var knots = double.Parse(fields[7], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                    var speed = knots * 1.852;
                    return new RMC(speed);
                }
                catch (Exception)
                {
                }
            }

            return null;
        }
    }
}