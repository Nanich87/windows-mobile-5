namespace GNN.NMEAParser
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;

    public sealed class NMEAParser
    {
        private static NMEAParser instance;

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

        public void Parse(string data)
        {
        }

        public double GetSpeed()
        {
            return 0;
        }

        private void ParseMessage(string message)
        {

        }
    }
}