/********************************************************************************* 
  *Author:Rebuild failed
  *Date:  2018-10-9
  *Description: 各种文件读写
**********************************************************************************/
using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class PFileStream
{

    //读取CSV文件
    public static Dictionary<string, Dictionary<string, string>> ReadCsvFile(string _fileName)
    {
        Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>();
        string url = PPlatform.SplitPath(new string[] { PPlatform.DATA_PATH, "CSV", _fileName });
        string[] fileData = File.ReadAllLines(url);
        if (fileData.Length < 3)
        {
            return result;
        }

        /* CSV文件的第一行为Key字段，第二行为说明（不需要读取），第三行开始是数据。第一个字段一定是ID。 */
        string[] keys = fileData[0].Split(',');
        for (int i = 2; i < fileData.Length; i++)
        {
            string[] line = fileData[i].Split(',');

            /* 以ID为key值，创建一个新的集合，用于保存当前行的数据 */
            string ID = line[0];
            result[ID] = new Dictionary<string, string>();
            for (int j = 0; j < line.Length; j++)
            {
                /* 每一行的数据存储规则：Key字段-Value值 */
                result[ID].Add(keys[j],line[j]);
            }
        }
        return result;
    }
    //保存阵容-暂时全部保存，可优化为保存id和能力值
    public static void SavePokemonToJsonFile()
    {
        string url = PPlatform.SplitPath(new string[] { PPlatform.DATA_PATH, "Save", "pokemons.json"});
        FileStream fs = new FileStream(url, FileMode.Create);
        using (StreamWriter jsonWriter = new StreamWriter(fs))
        {
            JsonSerializer serializer = new JsonSerializer();            
            serializer.Serialize(jsonWriter, RuntimeData.GetMyPokemons());
        }
        fs.Close();
    }
    //加载阵容
    public static void LoadPokemonFromJsonFile()
    {
        string url = PPlatform.SplitPath(new string[] { PPlatform.DATA_PATH, "Save", "pokemons.json" });

        FileStream fs = new FileStream(url, FileMode.OpenOrCreate);
        using (StreamReader jsonReader = new StreamReader(fs))
        {
            JsonSerializer serializer = new JsonSerializer();
            Pokemon[] p = (Pokemon[])serializer.Deserialize(jsonReader, typeof(Pokemon[]));
            if(p != null)
            {
                for (int i = 0; i < RuntimeData.PARTY_NUM; i++)
                {
                    RuntimeData.SetCurrentMyIndex(i);
                    RuntimeData.SetCurrentMyPokemon(p[i]);
                }
            }
        }
        fs.Close();
    }
}
