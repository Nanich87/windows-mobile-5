namespace GNN.NMEAParser.Contracts
{
    using System;

    public interface IDateTimeMessage : IMessage
    {
        DateTime DateTime { get; set; }
    }
}