using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personality
{
    private  PersonalityModel model;
    public PersonalityModel Model
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
    public Personality(PersonalityModel _model)
    {
        model = _model;
    }
}
