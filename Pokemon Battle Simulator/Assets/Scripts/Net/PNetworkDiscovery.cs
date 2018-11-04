using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PNetworkDiscovery : NetworkDiscovery
{

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        if (isClient)
        {        
            if(!PNetworkManager.instance.IsClientConnected())
            {
                PNetworkManager.instance.networkAddress = fromAddress;
                PNetworkManager.instance.networkPort = 7777;
                PNetworkManager.instance.client = PNetworkManager.instance.StartClient();
            }
        }
    }

}