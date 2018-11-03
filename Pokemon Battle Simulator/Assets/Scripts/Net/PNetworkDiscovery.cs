using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PNetworkDiscovery : NetworkDiscovery
{

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        if(isClient)
        {
            if (running)
            {
                StopBroadcast();
            }
            PNetworkManager.singleton.networkAddress = fromAddress;
            PNetworkManager.singleton.networkPort = 7777;
            PNetworkManager.singleton.client=PNetworkManager.singleton.StartClient();
        }
    }

}