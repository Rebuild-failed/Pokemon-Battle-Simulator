using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    private readonly CharacterModel model;
    public Character(CharacterModel _model)
    {
        model = _model;
    }
    public CharacterModel GetModel()
    {
        return model;
    }
    public virtual void Do()
    {

    }
	
}
