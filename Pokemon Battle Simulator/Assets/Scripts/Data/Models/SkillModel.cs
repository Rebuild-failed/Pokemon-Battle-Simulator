/********************************************************************************* 
  *Author:Rebuild failed
  *Date:  2018-10-9
  *Description: skills表对应类
**********************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillModel : CSVModel
{

    public string name_ch { get; set; }//中文名
    public string name_jp { get; set; }//日文名
    public string name_en { get; set; }//英文名
    public string type { get; set; }//属性
    public string category { get; set; }//分类（物理、特殊、变化）
    public int power { get; set; } //威力
    public int rate { get; set; }//命中
    public int pp { get; set; }
}
