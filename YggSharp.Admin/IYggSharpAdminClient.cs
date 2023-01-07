using YggSharp.Admin.Requests;
using YggSharp.Admin.Requests.AddPeer;
using YggSharp.Admin.Requests.GetDHT;
using YggSharp.Admin.Requests.GetPaths;
using YggSharp.Admin.Requests.GetPeers;
using YggSharp.Admin.Requests.GetSelf;
using YggSharp.Admin.Requests.GetSessions;
using YggSharp.Admin.Requests.RemovePeer;

namespace YggSharp.Admin;

public interface IYggSharpAdminClient
{
    Task<bool> Connect(CancellationToken ct);
    Task<YggAdminResponse<YggAdminPeersRequest, YggAdminPeersResponse>?> GetPeers(CancellationToken ct);
    Task<YggAdminResponse<YggAdminAddPeerRequest, YgAdminAddPeerResponse>?> AddPeer(string uri, string? iface, CancellationToken ct);
    Task<YggAdminResponse<YggAdminGetDHTRequest, YggAdminGetDHTResponse>?> GetDHT(CancellationToken ct);
    Task<YggAdminResponse<YggAdminGetPathsRequest, YggAdminGetPathsResponse>?> GetPaths(CancellationToken ct);
    Task<YggAdminResponse<YggAdminGetSelfRequest, YggAdminGetSelfResponse>?> GetSelf(CancellationToken ct);
    Task<YggAdminResponse<YggAdminGetSessionsRequest, YggAdminGetSessionsResponse>?> GetSessions(CancellationToken ct);
    Task<YggAdminResponse<YggAdminRemovePeerRequest, YggAdminRemovePeerResponse>?> RemovePeer(string uri, string? iface, CancellationToken ct);
}