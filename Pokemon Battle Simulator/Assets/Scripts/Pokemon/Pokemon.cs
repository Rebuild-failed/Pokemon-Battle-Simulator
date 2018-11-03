using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDUI;
public class Pokemon
{
    private int currentHp;
    public int CurrentHp
    {
        set
        {
            currentHp = value;
            if(isMe)
            {
                UIDelegateManager.NotifyUI(UIMessageType.RefreshMyHpBar, (float)value /(float)model.hp);
                UIDelegateManager.NotifyUI(UIMessageType.RefreshMyHpText, value);
            }
            else
            {
                UIDelegateManager.NotifyUI(UIMessageType.RefreshOpponentHpBar, (float)value / (float)model.hp);
            }
        }
        get
        {
            return currentHp;
        }
    }
    public bool isMe = false;
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
        currentHp = _pokemon.hp;
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
    public Statu GetStatu()
    {
        return statu;
    }
}
