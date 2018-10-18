using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{
    private PokemonModel model;
    private Character character;
    private Personality personality;
    private Item item;
    private Skill[] skills;
    private Statu statu;
    private int hitRate;//命中等级
    private int dodgeRate;//闪避等级
    public Pokemon(PokemonModel _pokemon,CharacterModel _character, PersonalityModel _personality,ItemModel _item,SkillModel[] _skills)
    {
        model = _pokemon;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
