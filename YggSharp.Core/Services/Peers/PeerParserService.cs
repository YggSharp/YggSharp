using YggSharp.Core.Models.Peer;

namespace YggSharp.Core.Services.Peers;

public class PeerParserService : IPeerParserService
{
    private readonly HashSet<IPeerProvider> _peerProviders = new();

    public PeerParserService()
    {
        AddPeerProvider<PublicPeerProvider>();
    }

    protected void AddPeerProvider<TProvider>()
        where TProvider : IPeerProvider, new()
    {
        _peerProviders.Add(new TProvider());
    }

    public IEnumerable<Peer> GetPeers()
    {
        return _peerProviders
            .Select(p => p.GetPeers())
            .Select(peerCollection => peerCollection.ToBlockingEnumerable())
            .SelectMany(x => x)
            .Distinct();
    }
}
