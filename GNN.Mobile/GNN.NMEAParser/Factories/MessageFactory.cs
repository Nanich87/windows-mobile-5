namespace GNN.NMEAParser.Factories
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    using GNN.NMEAParser.Contracts;
    using GNN.NMEAParser.Messages;

    public class MessageFactory
    {
        public IMessage CreateMessage(string line)
        {
            if (line.Length < 6 || line[0] != '$')
            {
                return null;
            }

            var type = line.Substring(3, 3);

            switch (type)
            {
                case VTG.Name:
                    {
                        return VTG.Create(line);
                    }

                case RMC.Name:
                    {
                        return RMC.Create(line);
                    }

                default:
                    {
                        return null;
                    }
            }
        }
    }
}