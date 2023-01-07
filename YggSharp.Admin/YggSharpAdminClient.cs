using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using YggSharp.Admin.Requests;
using YggSharp.Admin.Requests.AddPeer;
using YggSharp.Admin.Requests.GetDHT;
using YggSharp.Admin.Requests.GetPaths;
using YggSharp.Admin.Requests.GetPeers;
using YggSharp.Admin.Requests.GetSelf;
using YggSharp.Admin.Requests.GetSessions;
using YggSharp.Admin.Requests.RemovePeer;

namespace YggSharp.Admin;

public class YggSharpAdminClient : IYggSharpAdminClient
{
    private readonly IPAddress _ipAddress;
    private readonly ushort _port;
    
    private readonly TcpClient _client;
    private NetworkStream? _stream;

    public YggSharpAdminClient(string ipAddress, ushort port) : this(IPAddress.Parse(ipAddress), port) {}
    
    public YggSharpAdminClient(IPAddress ipAddress, ushort port)
    {
        _ipAddress = ipAddress;
        _port = port;
        _client = new TcpClient();
    }

    public async Task<bool> Connect(CancellationToken ct)
    {
        try
        {
            await _client.ConnectAsync(_ipAddress, _port, ct);
        }
        catch
        {
            // TODO: log
            return false;
        }

        if (!_client.Connected)
        {
            // TODO: log
            return false;
        }

        _stream = _client.GetStream();
        return true;
    }

    private async Task<YggAdminResponse<TRequest, TResponse>?> Send<TRequest, TResponse>(TRequest request, CancellationToken ct)
        where TRequest : YggAdminRequest
        where TResponse : class
    {
        if (_stream == null)
        {
            throw new InvalidOperationException("Trying to send JSON RCP request while no stream is available.");
        }
        
        await JsonSerializer.SerializeAsync(_stream, request, cancellationToken: ct);
        await _stream.FlushAsync(ct);

        var responseStream = new MemoryStream();

        while (true)
        {
            var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMilliseconds(250));
            var buff = new byte[256];
            int read;
            
            try
            {
                read = await _stream.ReadAtLeastAsync(buff, 1, false, cancellationTokenSource.Token);

                if (read == 0)
                {
                    break;
                }
            }
            catch (OperationCanceledException)
            {
                break;
            }

            await responseStream.WriteAsync(buff, 0, read, ct);
        }

        responseStream.Seek(0, SeekOrigin.Begin);
        var result = await JsonSerializer.DeserializeAsync<YggAdminResponse<TRequest, TResponse>>(responseStream, cancellationToken: ct);

        if (result == null)
        {
            // TODO: log
        }

        return result;
    }

    public async Task<YggAdminResponse<YggAdminPeersRequest, YggAdminPeersResponse>?> GetPeers(CancellationToken ct)
    {
        return await Send<YggAdminPeersRequest, YggAdminPeersResponse>(new YggAdminPeersRequest(), ct);
    }

    public async Task<YggAdminResponse<YggAdminAddPeerRequest, YgAdminAddPeerResponse>?> AddPeer(string uri, string? iface, CancellationToken ct)
    {
        return await Send<YggAdminAddPeerRequest, YgAdminAddPeerResponse>(new YggAdminAddPeerRequest(uri, iface), ct);
    }
    
    public async Task<YggAdminResponse<YggAdminGetDHTRequest, YggAdminGetDHTResponse>?> GetDHT(CancellationToken ct)
    {
        return await Send<YggAdminGetDHTRequest, YggAdminGetDHTResponse>(new YggAdminGetDHTRequest(), ct);
    }
    
    public async Task<YggAdminResponse<YggAdminGetPathsRequest, YggAdminGetPathsResponse>?> GetPaths(CancellationToken ct)
    {
        return await Send<YggAdminGetPathsRequest, YggAdminGetPathsResponse>(new YggAdminGetPathsRequest(), ct);
    }
    
    public async Task<YggAdminResponse<YggAdminGetSelfRequest, YggAdminGetSelfResponse>?> GetSelf(CancellationToken ct)
    {
        return await Send<YggAdminGetSelfRequest, YggAdminGetSelfResponse>(new YggAdminGetSelfRequest(), ct);
    }
    
    public async Task<YggAdminResponse<YggAdminGetSessionsRequest, YggAdminGetSessionsResponse>?> GetSessions(CancellationToken ct)
    {
        return await Send<YggAdminGetSessionsRequest, YggAdminGetSessionsResponse>(new YggAdminGetSessionsRequest(), ct);
    }
    
    public async Task<YggAdminResponse<YggAdminRemovePeerRequest, YggAdminRemovePeerResponse>?> RemovePeer(string uri, string? iface, CancellationToken ct)
    {
        return await Send<YggAdminRemovePeerRequest, YggAdminRemovePeerResponse>(new YggAdminRemovePeerRequest(uri, iface), ct);
    }
}
