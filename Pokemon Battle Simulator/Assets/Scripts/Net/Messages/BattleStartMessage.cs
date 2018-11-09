using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BattleStartMessage : MessageBase
{
    public static readonly short type = 100;
    public int index;
    public int pokemonId;
    public int hpAv;
    public int attackAv;
    public int defenceAv;
    public int sp_attackAv;
    public int sp_defenceAv;
    public int speedAv;
    public int charavterId;
    public int personalityId;
    public int itemId;
    public int[] skillIds = new int[4];
    public BattleStartMessage()
    {

    }
    public BattleStartMessage(Pokemon _pokemon,int _index)
    {
        index = _index;
        PokemonModel pM = _pokemon.Model;
        CharacterModel cM = _pokemon.Character.Model;
        PersonalityModel perM = _pokemon.Personality.Model;
        ItemModel iM = _pokemon.Item.Model;
        Skill[] skM = _pokemon.Skills;//固定为4个技能
        pokemonId = pM.id;
        hpAv = pM.hp;
        attackAv = pM.attack;
        defenceAv = pM.defense;
        sp_attackAv = pM.sp_attack;
        sp_defenceAv = pM.sp_defense;
        speedAv = pM.speed;
        charavterId = cM.id;
        personalityId = perM.id;
        itemId = iM.id;
        for(int i=0;i<4;i++)
        {
            skillIds[i] = skM[i].Model.id;
        }
    }
}
