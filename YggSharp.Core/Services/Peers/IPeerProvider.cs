using YggSharp.Core.Models.Peer;

namespace YggSharp.Core.Services.Peers;

public interface IPeerProvider
{
    IAsyncEnumerable<Peer> GetPeers();
}