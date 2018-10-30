using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public SkillModel model;
    public Skill(SkillModel _model)
    {
        model = _model;
    }
    public SkillModel GetModel()
    {
        return model;
    }
    public virtual void Do()
    {

    }
}
