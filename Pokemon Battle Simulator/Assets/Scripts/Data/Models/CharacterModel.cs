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
}
