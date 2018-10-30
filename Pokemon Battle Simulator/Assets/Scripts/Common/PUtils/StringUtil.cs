using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringUtil
{
    public static string FormatId(int id)
    {
        if (id < 10)
        {
            return string.Format("00{0}", id);
        }
        else if (id < 100)
        {
            return string.Format("0{0}", id);
        }
        else
        {
            return string.Format("{0}", id);
        }
    }
    public static string FormatIdName(int id, string name)
    {
        if (id < 10)
        {
            return string.Format("00{0} {1}", id, name);
        }
        else if (id < 100)
        {
            return string.Format("0{0} {1}", id, name);
        }
        else
        {
            return string.Format("{0} {1}", id, name);
        }
    }
}
