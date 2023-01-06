﻿using System.Net;
using HtmlAgilityPack;
using YggSharp.Core.Models.Peer;

namespace YggSharp.Core.PeerParser;

public class PublicPeerParser : IPeerParser
{
    public const string PublicPeerSource = "https://publicpeers.neilalexander.dev/";

    private HttpClient _httpClient = new();

    public async Task<List<Peer>> GetPeers()
    {
        var web = new HtmlWeb();
        var doc = await web.LoadFromWebAsync(PublicPeerSource);
        var peerTable = doc.DocumentNode.SelectSingleNode("//body/table");
        var currentRegion = "unknown";
        var peers = new List<Peer>();
        
        foreach (var peerTableChildNode in peerTable.ChildNodes)
        {
            if (peerTableChildNode.Attributes.Any(attribute => attribute.Value.StartsWith("status")))
            {
                // This is a node with peer info
                var peer = new Peer();

                var addrNode = peerTableChildNode.SelectSingleNode(".//*[@id=\"address\"]");
                var versionNode = peerTableChildNode.SelectSingleNode(".//*[@id=\"version\"]");
                var statusNode = peerTableChildNode.SelectSingleNode(".//*[@id=\"status\"]");
                var reliabilityNode = peerTableChildNode.SelectSingleNode(".//*[@id=\"reliability\"]");

                peer.Uri = new Uri(addrNode.InnerText);
                peer.Online = statusNode.InnerText.ToLowerInvariant() == "online";
                peer.ReliabilityString = reliabilityNode.InnerText;
                peer.Version = WebUtility.HtmlDecode(versionNode.InnerText);
                peer.Region = currentRegion;
                
                peers.Add(peer);
            }
            else
            {
                // Node might be with region info then
                var countryNode = peerTableChildNode.SelectSingleNode("./th");

                if (countryNode is { Id: "country" })
                {
                    currentRegion = countryNode.InnerText;
                }
            }
            
        }

        return peers;
    }
}