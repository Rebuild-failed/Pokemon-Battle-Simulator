using System.Collections;
using System.Collections.Generic;
using RDUI;

public class RuntimeData
{
    public static bool isMyRound = true;
    public static readonly int PARTY_NUM = 6;
    private static Pokemon[] myPokemons = new Pokemon[PARTY_NUM];
    private static Pokemon[] oppPokemons = new Pokemon[PARTY_NUM];
    private static int currentMyIndex = 0;
    private static int currentOppIndex = 0;


    /*My*/
    public static bool IsMyGameOver()
    {
        for (int i = 0; i < PARTY_NUM; i++)
        {
            if (myPokemons[i].CurrentHp > 0)
            {
                return false;
            }
        }
        return true;
    }
    public static Pokemon[] GetMyPokemons()
    {
        return myPokemons;
    }
    public static void SetCurrentMyPokemon(Pokemon _p)
    {
        myPokemons[currentMyIndex] = _p;
        UIDelegateManager.NotifyUI(UIMessageType.RefreshParty, new object[] { currentMyIndex });
    }
    public static Pokemon GetMyPokemonByIndex(int _index)
    {
        return myPokemons[_index];
    }
    public static Pokemon GetCurrentMyPokemon()
    {
        return myPokemons[currentMyIndex];
    }
    public static void SetCurrentMyIndex(int _index)
    {
        if(_index>-1&&_index<myPokemons.Length)
        {
            currentMyIndex = _index;
            UIDelegateManager.NotifyUI(UIMessageType.RefreshBattlePokemon, new object[] { myPokemons[currentMyIndex] });
        }
    }
    public static int GetCurrentMyIndex()
    {
        return currentMyIndex;
    }
    public static bool IsMyPokemonsFull()
    {
        for (int i = 0; i < PARTY_NUM; i++)
        {
            if (myPokemons[i] == null)
            {
                return false;
            }
        }
        return true;
    }

    /*Opponent*/
    public static bool IsOppGameOver()
    {
        for (int i = 0; i < PARTY_NUM; i++)
        {
            if (oppPokemons[i].CurrentHp > 0)
            {
                return false;
            }
        }
        return true;
    }
    public static void SetOppPokemon(int _index, Pokemon _p)
    {
        if (_index > -1 && _index < oppPokemons.Length)
        {
            currentOppIndex = _index;
            oppPokemons[_index] = _p;
        }
    }
    public static Pokemon GetOppPokemonByIndex(int _index)
    {
        return oppPokemons[_index];
    }
    public static Pokemon GetCurrentOppPokemon()
    {
        return oppPokemons[currentOppIndex];
    }
    public static void SetCurrentOppIndex(int _index)
    {
        if (_index > -1 && _index < oppPokemons.Length)
        {
            currentOppIndex = _index;
            UIDelegateManager.NotifyUI(UIMessageType.RefreshBattlePokemon, new object[] { oppPokemons[currentOppIndex] });
        }
    }
    public static int GetCurrentOppIndex()
    {
        return currentOppIndex;
    }
    public static bool IsOppPokemonsFull()
    {
        for(int i=0;i< PARTY_NUM; i++)
        {
            if(oppPokemons[i] == null)
            {
                return false;
            }
        }
        return true;
    }

}
