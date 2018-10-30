/********************************************************************************* 
  *Author:Rebuild failed
  *Date:  2018-10-9
  *Description: 存储CSV表数据
**********************************************************************************/

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;

public class PublicDataManager : MonoBehaviour
{
    public static PublicDataManager instance = null;
    private Dictionary<int, PokemonModel> pokemonModel;
    private Dictionary<int, CharacterModel> characterModel;
    private Dictionary<int, PersonalityModel> personalityModel;
    private Dictionary<int, ItemModel> itemModel;
    private Dictionary<int, SkillModel> skillModel;
    void Awake()
    {
        //单例，关卡切换不销毁
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        //初始化Ini
        InitIni();
        //初始化CSV
        InitCsv();
    }
    private void InitIni()
    {
    }
    private void InitCsv()
    {
        //在这初始化每个Dictionary
        /*Pokemon*/
        InitFromCsv<PokemonModel>(ref pokemonModel, "pokemons.csv");

        /*Character*/
        InitFromCsv<CharacterModel>(ref characterModel, "characters.csv");

        /*Personality*/
        InitFromCsv<PersonalityModel>(ref personalityModel, "Personalitys.csv");
        /*Skill*/
        InitFromCsv<SkillModel>(ref skillModel, "skills.csv");

        /*Item*/
        InitFromCsv<ItemModel>(ref itemModel, "items.csv");

    }
    //初始化CSV表
    private void InitFromCsv<T>(ref Dictionary<int, T> _dataModel, string _fileName)
    {
        _dataModel = LoadCsvData<T>(_fileName);

    }
    //从CSV表初始化Dictionary
    private static Dictionary<int, T> LoadCsvData<T>(string _fileName)
    {
        Dictionary<int, T> dic = new Dictionary<int, T>();

        /* 从CSV文件读取数据 */
        Dictionary<string, Dictionary<string, string>> result = PFileStream.ReadCsvFile(_fileName);
        /* 遍历每一行数据 */
        foreach (string id in result.Keys)
        {
            /* CSV的一行数据 */
            Dictionary<string, string> datas = result[id];

            /* 读取Csv数据对象的属性 */
            PropertyInfo[] props = typeof(T).GetProperties();
            /* 使用反射，将CSV文件的数据赋值给CSV数据对象的相应字段，要求CSV文件的字段名和CSV数据对象的字段名完全相同 */
            T obj = Activator.CreateInstance<T>();
            foreach (PropertyInfo p in props)
            {
                PReflectUtil.PiSetValue<T>(datas[p.Name], p, obj);
            }
            /* 按id-数据的形式存储 */
            dic.Add(int.Parse(id), obj);      
        }

        return dic;
    }
    /*Pokemon*/
    public Dictionary<int, PokemonModel>.KeyCollection GetPokemonModelKeys()
    {
        return pokemonModel.Keys;
    }
    public PokemonModel GetPokemonModel(int id)
    {
        return new PokemonModel(pokemonModel[id]);
    }
    public string GetPokemonModelNameCh(int id)
    {
        return pokemonModel[id].name_ch;
    }
    public string GetCharacterIds(int id)
    {
        return pokemonModel[id].character;
    }
    public string GetSkillIds(int id)
    {
        return pokemonModel[id].skills;
    }
    /*Character*/
    public Dictionary<int, CharacterModel>.KeyCollection GetCharacterModelKeys()
    {
        return characterModel.Keys;
    }
    public CharacterModel GetCharacterModel(int id)
    {
        return new CharacterModel(characterModel[id]);
    }
    public string GetCharacterModelNameCh(int id)
    {
        return characterModel[id].name_ch;
    }

    /*Personality*/
    public Dictionary<int, PersonalityModel>.KeyCollection GetPersonalityModelKeys()
    {
        return personalityModel.Keys;
    }
    public PersonalityModel GetPersonalityModel(int id)
    {
        return new PersonalityModel(personalityModel[id]);
    }
    public string GetPersonalityModelNameCh(int id)
    {
        return personalityModel[id].name_ch;
    }
    public PersonalityModel GetPersonalityModel(string namech)
    {
        return personalityModel.FirstOrDefault(p => p.Value.name_ch == namech).Value;
    }
    /*Skill*/
    public Dictionary<int, SkillModel>.KeyCollection GetSkillModelKeys()
    {
        return skillModel.Keys;
    }
    public SkillModel GetSkillModel(int id)
    {
        return skillModel[id];
    }
    public string GetSkillModelNameCh(int id)
    {
        return skillModel[id].name_ch;
    }

    /*Item*/
    public Dictionary<int, ItemModel>.KeyCollection GetItemModelKeys()
    {
        return itemModel.Keys;
    }
    public ItemModel GetItemModel(int id)
    {
        return new ItemModel(itemModel[id]);
    }
    public string GetItemModelNameCh(int id)
    {
        return itemModel[id].name_ch;
    }
    public ItemModel GetItemModel(string namech)
    {
        return itemModel.FirstOrDefault(i => i.Value.name_ch == namech).Value;
    }
}
