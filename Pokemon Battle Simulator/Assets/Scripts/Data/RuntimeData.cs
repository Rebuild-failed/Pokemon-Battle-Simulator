using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeData
{
    private static Pokemon[] myPokemons = new Pokemon[6];
    private static Pokemon[] oppPokemons = new Pokemon[6];
    private static int currentIndex = 0;
    public static void SetCurrentMyPokemon(Pokemon _p)
    {
        myPokemons[currentIndex] = _p;
    }
    public static Pokemon GetCurrentMyPokemon()
    {
        return myPokemons[currentIndex];
    }
    public static void SetCurrentIndex(int _index)
    {
        if(_index>-1&&_index<myPokemons.Length)
        {
            currentIndex = _index;
        }
    }
    public static int GetCurrentIndex()
    {
        return currentIndex;
    }
}
