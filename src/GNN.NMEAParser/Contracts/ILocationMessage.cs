namespace GNN.NMEAParser.Contracts
{
    public interface ILocationMessage : IMessage
    {
        double Latitude { get; set; }

        double Longitude { get; set; }
    }
}