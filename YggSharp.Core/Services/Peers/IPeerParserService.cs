using YggSharp.Core.Models.Peer;

namespace YggSharp.Core.Services.Peers;

public interface IPeerParserService
{
    IEnumerable<Peer> GetPeers();
}