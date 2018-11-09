using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GiveUpMessage : MessageBase
{
    public static readonly short type = 104;
    public int connnectId;
    public GiveUpMessage()
    {

    }
    public GiveUpMessage(int _connectId)
    {
        connnectId = _connectId;
    }
}
