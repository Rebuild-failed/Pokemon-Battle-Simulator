using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeData
{
    private static Pokemon[] myPokemons = new Pokemon[6];
    private static Pokemon[] oppPokemons = new Pokemon[6];
    private static int currentMyIndex = 0;
    private static int currentOppIndex = 0;
    public static void SetCurrentMyPokemon(Pokemon _p)
    {
        myPokemons[currentMyIndex] = _p;
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
        }
    }
    public static int GetCurrentMyIndex()
    {
        return currentMyIndex;
    }
    public static bool IsMyPokemonsFull()
    {
        if (myPokemons.Length == 6)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void SetCurrentOppPokemon(Pokemon _p)
    {
        oppPokemons[currentOppIndex] = _p;
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
        }
    }
    public static int GetCurrentOppIndex()
    {
        return currentOppIndex;
    }
    public static bool IsOppPokemonsFull()
    {
        if (oppPokemons.Length == 6)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
