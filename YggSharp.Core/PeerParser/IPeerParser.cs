using YggSharp.Core.Models.Peer;

namespace YggSharp.Core.PeerParser;

public interface IPeerParser
{
    Task<List<Peer>> GetPeers();
}