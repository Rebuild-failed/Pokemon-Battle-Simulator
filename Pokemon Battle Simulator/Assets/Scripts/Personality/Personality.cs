using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personality
{
    private readonly PersonalityModel model;
    public Personality(PersonalityModel _model)
    {
        model = _model;
    }
    public PersonalityModel GetModel()
    {
        return model;
    }
}
