namespace GNN.NMEAParser.Contracts
{
    public interface ISpeedMessage : IMessage
    {
        double Speed { get; set; }
    }
}