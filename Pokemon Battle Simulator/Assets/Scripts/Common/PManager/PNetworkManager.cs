using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PNetworkManager : NetworkManager
{

    public static void LanGame(int model)
    {
        singleton.StartCoroutine((singleton as PNetworkManager).DiscoverNetwork(model));
    }

    public IEnumerator DiscoverNetwork(int model)
    {
        //监听其它的服务器
        PNetworkDiscovery discovery = GetComponent<PNetworkDiscovery>();
        discovery.Initialize();
        if(model == 0)
        {
            discovery.StartAsServer();
            StartHost();
            
        }
        if(model == 1)
        {
            discovery.StartAsClient();
            //等待2秒，等监听结果
            yield return new WaitForSeconds(2);

            //没有找到局域网服务器停止监听
            if (discovery.running)
            {
                discovery.StopBroadcast();
                yield return new WaitForSeconds(0.5f);
            }
        }

        
    }
}
