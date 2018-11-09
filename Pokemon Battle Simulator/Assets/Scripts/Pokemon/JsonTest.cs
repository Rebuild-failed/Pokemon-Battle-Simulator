using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonTest{
    public bool isMine = false;
    private int currentHp;
    public int CurrentHp
    {
        set
        {
            currentHp = value;
           

        }
        get
        {
            return currentHp;
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
    private PokemonModel model;
    public PokemonModel Model
    {
        set
        {
            model = value;
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
    public JsonTest()
    {

    }
    public JsonTest(PokemonModel p,CharacterModel c, PersonalityModel per, ItemModel i)
    {
        model = p;
        currentHp = p.hp;
        character = new Character(c);
        personality = new Personality(per);
        item = new Item(i);
    }
}
