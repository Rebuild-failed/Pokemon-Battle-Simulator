using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChangePokemonMessage : MessageBase
{
    public static readonly short type=102;
    public int index;
    public bool isCantFightChange;
    public ChangePokemonMessage()
    {

    }
    public ChangePokemonMessage(int _index,bool _isCantFightChange)
    {
        index = _index;
        isCantFightChange = _isCantFightChange;
    }
}
