using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalityModel : CSVModel
{
    public string name_ch { get; set; }
    public int enhance { get; set; }
    public int reduce { get; set; }
    public PersonalityModel()
    {

    }
    public PersonalityModel(PersonalityModel _source)
    {
        id = _source.id;
        name_ch = _source.name_ch;
        enhance = _source.enhance;
        reduce = _source.reduce;
    }
}
