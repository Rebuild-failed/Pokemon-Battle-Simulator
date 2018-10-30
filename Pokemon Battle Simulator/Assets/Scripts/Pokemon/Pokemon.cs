using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{
    private  PokemonModel model;
    private  Character character;
    private  Personality personality;
    private  Item item;
    private  Skill[] skills;
    private Statu statu;
    private int hitRate;//命中等级
    private int dodgeRate;//闪避等级
    public Pokemon(PokemonModel _pokemon,CharacterModel _character, PersonalityModel _personality,ItemModel _item,SkillModel[] _skills)
    {
        model = _pokemon;
        character = new Character(_character);
        personality = new Personality(_personality);
        item = new Item(_item);
        skills =new Skill[4];
        for(int i=0;i<skills.Length;i++)
        {
            skills[i] = new Skill(_skills[i]);
        }
    }
    public  PokemonModel GetModel()
    {
        return model;
    }
    public Character GetCharacter()
    {
        return character;
    }
    public Personality GetPersonality()
    {
        return personality;
    }
    public Item GetItem()
    {
        return item;
    }
    public Skill[] GetSkills()
    {
        return skills;
    }
}
