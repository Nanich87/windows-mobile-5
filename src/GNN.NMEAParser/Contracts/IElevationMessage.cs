namespace GNN.NMEAParser.Contracts
{
    public interface IElevationMessage : IMessage
    {
        double Altitude { get; set; }
    }
}