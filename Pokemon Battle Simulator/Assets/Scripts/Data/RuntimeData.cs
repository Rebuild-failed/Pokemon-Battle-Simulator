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
    public static void SetCurrentIndex(int _index)
    {
        currentIndex = _index;
    }
    public static int GetCurrentIndex()
    {
        return currentIndex;
    }
}
