/********************************************************************************* 
  *Author:Rebuild failed
  *Date:  2018-10-9
  *Description: characters表对应类
**********************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel :CSVModel
{
    public string name_ch{ get; set; }//中文名
    public string name_jp { get; set; }//日文名
    public string name_en { get; set; }//英文名
    public string description { get; set; }//描述
    public CharacterModel()
    {

    }
    public CharacterModel(CharacterModel _source)
    {
        id = _source.id;
        name_ch = _source.name_ch;
        name_jp = _source.name_jp;
        name_en = _source.name_en;
        description = _source.description;
    }
}
