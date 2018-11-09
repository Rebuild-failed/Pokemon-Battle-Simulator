using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    private CharacterModel model;
    public CharacterModel Model
    {
        set
        {
            if(value != null)
            {
                model = value;
            }
        }
        get
        {
            return model;
        }
    }
    public Character(CharacterModel _model)
    {
        model = _model;
    }
    public virtual void Do()
    {

    }

}
