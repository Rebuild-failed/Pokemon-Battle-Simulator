using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChangePokemonMessage : MessageBase
{
    public static readonly short type=101;
    public int index;
    public int pokemonId;
    public ChangePokemonMessage(int _index,int _id)
    {
        index = _index;
        pokemonId = _id;
    }
}
