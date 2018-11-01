using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringUtil
{
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
