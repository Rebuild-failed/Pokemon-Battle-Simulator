using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringUtil
{
    public static string GetTypeUrl(string _type)
    {
        switch(_type)
        {
            case "毒":return string.Format("PokemonTypes/typePOISON");break;
            case "草": return string.Format("PokemonTypes/typeGRASS"); break;
            case "火": return string.Format("PokemonTypes/typeFIRE"); break;
            case "水": return string.Format("PokemonTypes/typeWATER"); break;
            case "一般": return string.Format("PokemonTypes/typeNORMAL"); break;
            case "飞行": return string.Format("PokemonTypes/typeFLYING"); break;
            case "电": return string.Format("PokemonTypes/typeELECTRIC"); break;
            default:return null;
        }
    }
    //暂时只有濒死状态，之后改成CSV表
    public static string GetStatuUrl(int _id)
    {
        return string.Format("Status/statusFAINTED");
    }
    //格式化ID
    public static string FormatId(int _id)
    {
        if (_id < 10)
        {
            return string.Format("00{0}", _id);
        }
        else if (_id < 100)
        {
            return string.Format("0{0}", _id);
        }
        else
        {
            return string.Format("{0}", _id);
        }
    }
    //格式化ID 名称
    public static string FormatIdName(int _id, string _name)
    {
        if (_id < 10)
        {
            return string.Format("00{0} {1}", _id, _name);
        }
        else if (_id < 100)
        {
            return string.Format("0{0} {1}", _id, _name);
        }
        else
        {
            return string.Format("{0} {1}", _id, _name);
        }
    }
}
