using YggSharp.Core.Models.Peer;

namespace YggSharp.Core.Services.PeerParser;

public interface IPeerParser
{
    IAsyncEnumerable<Peer> GetPeers();
}