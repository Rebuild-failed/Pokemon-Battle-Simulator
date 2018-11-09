using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDUI;
public class Pokemon
{
    public bool isMine = false;
    private int currentHp;
    public int CurrentHp
    {
        set
        {
            currentHp = value;
            if (model != null)
            {
                if (currentHp <= 0)
                {
                    currentHp = 0;
                    UIDelegateManager.NotifyUI(UIMessageType.PokemonCantFight, new object[] { isMine });
                }
                if (isMine)
                {

                    UIDelegateManager.NotifyUI(UIMessageType.RefreshMyHpBar, new object[] { (float)value / (float)model.hp });
                    UIDelegateManager.NotifyUI(UIMessageType.RefreshMyHpText, new object[] { value });

                }
                else
                {
                    UIDelegateManager.NotifyUI(UIMessageType.RefreshOpponentHpBar, new object[] { (float)value / (float)model.hp });
                }
            }
        }
        get
        {
            return currentHp;
        }
    }

    private PokemonModel model;
    public PokemonModel Model
    {
        set
        {
            if (value != null)
            {
                model = value;
            }
        }
        get
        {
            return model;
        }
    }
    private Character character;
    public Character Character
    {
        set
        {
            character = value;
        }
        get
        {
            return character;
        }
    }
    private Personality personality;
    public Personality Personality
    {
        set
        {
            personality = value;
        }
        get
        {
            return personality;
        }
    }
    private Item item;
    public Item Item
    {
        set
        {
            item = value;
        }
        get
        {
            return item;
        }
    }
    private Skill[] skills;
    public Skill[] Skills
    {
        set
        {
            skills = value;
        }
        get
        {
            return skills;
        }
    }
    private Statu statu;
    public Statu Statu
    {
        set
        {
            statu = value;
        }
        get
        {
            return statu;
        }
    }
    private int hitRate;//命中等级
    public int HitRate
    {
        set
        {
            hitRate = value;
        }
        get
        {
            return hitRate;
        }
    }
    private int dodgeRate;//闪避等级
    public int DodgeRate
    {
        set
        {
            dodgeRate = value;
        }
        get
        {
            return dodgeRate;
        }
    }
    public Pokemon()
    {

    }
    public Pokemon(PokemonModel _pokemon, CharacterModel _character, PersonalityModel _personality, ItemModel _item, SkillModel[] _skills)
    {
        model = _pokemon;
        currentHp = _pokemon.hp;
        character = new Character(_character);
        personality = new Personality(_personality);
        item = new Item(_item);
        skills = new Skill[4];
        for (int i = 0; i < skills.Length; i++)
        {
            skills[i] = new Skill(_skills[i]);
        }
        statu = null;
    }
}
