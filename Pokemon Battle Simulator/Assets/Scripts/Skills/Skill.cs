using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    private SkillModel model;
    public SkillModel Model
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
    public Skill(SkillModel _model)
    {
        model = _model;
    }
    public virtual void Do(bool isMe)
    {
        if(isMe)
        {
            Pokemon oppPokemon = RuntimeData.GetCurrentOppPokemon();
            if (model.power > 0)
            {
                oppPokemon.CurrentHp -= model.power;
            }
        }
        else
        {
            Pokemon myPokemon = RuntimeData.GetCurrentMyPokemon();
            if (model.power > 0)
            {
                myPokemon.CurrentHp -= model.power;
            }
        }
    }
}
