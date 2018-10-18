/********************************************************************************* 
  *Author:Rebuild failed
  *Date:  2018-10-9
  *Description: pokemons表对应类
**********************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonModel : CSVModel
{
    public string name_ch { get; set; }
    public string character { get; set; }//特性
    public string type_1 { get; set; }//属性1
    public string type_2 { get; set; }//属性2
    public int hp { get; set; }//生命
    public int attack { get; set; }//攻击
    public int defense { get; set; }//防御
    public int sp_attack { get; set; }//特攻
    public int sp_defense { get; set; }//特防
    public int speed { get; set; }//速度
    public string skills { get; set; }//可选技能
}
