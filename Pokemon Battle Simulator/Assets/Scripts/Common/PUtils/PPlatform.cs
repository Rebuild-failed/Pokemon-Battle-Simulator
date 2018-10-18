/********************************************************************************* 
  *Author:Rebuild failed
  *Date:  2018-10-9
  *Description: 多平台处理
**********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPlatform
{
    public static string DATA_PATH
    {
        get
        {
            return Application.streamingAssetsPath;
        }
    }

    public static string LOG_PATH
    {
        get 
        {
            return Application.streamingAssetsPath;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
        }
    }
    public static bool isOSXEditor
    {
        get
        {
            return Application.platform == RuntimePlatform.OSXEditor;
        }
    }

    public static bool isWindowsEditor
    {
        get
        {
            return Application.platform == RuntimePlatform.WindowsEditor;
        }
    }

    //拼接路径
    public static string SplitPath(string[] _paths){

        string splitCharacter = isOSXEditor ? "/" : "\\";

        string path = _paths[0];

        for (var i = 1; i < _paths.Length; i++){
            path += splitCharacter + _paths[i];
        }

        return path;
    }
}
