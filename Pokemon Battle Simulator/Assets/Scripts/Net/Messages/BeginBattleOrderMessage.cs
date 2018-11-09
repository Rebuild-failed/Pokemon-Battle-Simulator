using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BeginBattleOrderMessage : MessageBase {
    public static readonly short type = 101;

    public bool isMyRound;
    public BeginBattleOrderMessage()
    {

    }
    public BeginBattleOrderMessage(bool _isMyRound)
    {
        isMyRound = _isMyRound;
    }
}
