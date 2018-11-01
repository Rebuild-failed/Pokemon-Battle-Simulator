<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PNetworkDiscovery : NetworkDiscovery
{

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        StopBroadcast();

        PNetworkManager.singleton.networkAddress = fromAddress;
        PNetworkManager.singleton.StartClient();
    }

}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PNetworkDiscovery : NetworkDiscovery
{

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        StopBroadcast();

        PNetworkManager.singleton.networkAddress = fromAddress;
        PNetworkManager.singleton.StartClient();
    }

}
>>>>>>> 8095a8e6047fc638c89da82d48881ea699845822
