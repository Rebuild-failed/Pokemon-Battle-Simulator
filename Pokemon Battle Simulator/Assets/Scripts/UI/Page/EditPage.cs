/********************************************************************************* 
  *Author:Rebuild failed
  *Date:  2018-10-18
  *Description: 编辑阵容页面
**********************************************************************************/
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
namespace RDUI
{
    public class EditPage : BasePage
    {
        public Dropdown pokemonDP;
        public Dropdown characterDP;
        public Dropdown personalityDP;
        public Dropdown itemDP;
        public InputField[] effectValueIFs;
        public Text sumText;
        public Dropdown[] skillDPs;
        public Image pokemonImage;
        public Image pokemonTypeImage0;
        public Image pokemonTypeImage1;
        public Text[] abilityValueTexts;
        public Button cancelBtn;
        public Button saveBtn;
        public Text errorText;

        private const int MAX_PRE_EV = 252;//单项努力值最大值
        private const int MAX_SUM_EV = 510;//努力值总和最大值
        private const int MAX_PRE_IV = 31;//单项个体值最大值
        private const int MAX_LVL = 100;//最大等级

        private PokemonModel pokemon;
        private PersonalityModel personality;
        private CharacterModel character;
        private ItemModel item;
        private SkillModel[] skills;

        private List<string> charactersTemp;
        private List<string> skillsTemp;
        protected override void Init()
        {
            //初始化变量
            skills = new SkillModel[4];
            charactersTemp = new List<string>();
            skillsTemp = new List<string>();

            //精灵选择
            foreach (int id in PublicDataManager.instance.GetPokemonModelKeys())
            {
                string name = PublicDataManager.instance.GetPokemonModelNameCh(id);
                string option = StringUtil.FormatIdName(id, name);
                pokemonDP.options.Add(new Dropdown.OptionData(option));
            }
            pokemonDP.onValueChanged.AddListener(delegate { OnSelectPokemon(); });

            //特性选择
            characterDP.onValueChanged.AddListener(delegate { OnSelectCharacter(int.Parse(characterDP.captionText.text.Substring(0, 3))); });
            //性格选择
            foreach (int id in PublicDataManager.instance.GetPersonalityModelKeys())
            {
                string name = PublicDataManager.instance.GetPersonalityModelNameCh(id);
                personalityDP.options.Add(new Dropdown.OptionData(name));
            }
            personalityDP.onValueChanged.AddListener(delegate { OnSelectPersonality(); });
            //携带物选择
            foreach (int id in PublicDataManager.instance.GetItemModelKeys())
            {
                string name = PublicDataManager.instance.GetItemModelNameCh(id);
                itemDP.options.Add(new Dropdown.OptionData(name));
            }
            itemDP.onValueChanged.AddListener(delegate { OnSelectItem(); });
            //努力值设定
            foreach (InputField i in effectValueIFs)
            {
                i.onValueChanged.AddListener(delegate { OnEffectValueInput(i, i.text); });
            }
            //招式选择
            foreach (Dropdown d in skillDPs)
            {
                d.onValueChanged.AddListener(delegate { OnSelectSkill(); });
            }

            cancelBtn.onClick.AddListener(Close);
            saveBtn.onClick.AddListener(Save);
            base.Init();
        }
        public override void Open()
        {
            Pokemon p = RuntimeData.GetCurrentMyPokemon();
            if (p != null)
            {
                InitFromPokmeon(p);
            }
            else
            {
                Reset();
            }
            base.Open();
        }
        //选择Pokemon
        private void OnSelectPokemon()
        {
            string id = pokemonDP.captionText.text.Substring(0, 3);
            pokemon = PublicDataManager.instance.GetPokemonModel(int.Parse(id));
            pokemonImage.sprite = Resources.Load<Sprite>("PokemonSprites/" + id + "/Front/IMG00000");
            pokemonTypeImage0.sprite = Resources.Load<Sprite>(StringUtil.GetTypeUrl(pokemon.type_1));
            if (pokemon.type_2 == string.Empty)
            {
                pokemonTypeImage1.gameObject.SetActive(false);
            }
            else
            {
                pokemonTypeImage1.sprite = Resources.Load<Sprite>(StringUtil.GetTypeUrl(pokemon.type_2));
                pokemonTypeImage1.gameObject.SetActive(true);
            }
            //特性
            charactersTemp.Clear();
            string[] chIds = PublicDataManager.instance.GetCharacterIds(pokemon.id).Split('|');
            foreach (string s in chIds)
            {
                int n = int.Parse(s);
                string name = PublicDataManager.instance.GetCharacterModelNameCh(n);
                charactersTemp.Add(StringUtil.FormatIdName(n, name));
            }
            characterDP.ClearOptions();
            characterDP.AddOptions(charactersTemp);

            //技能
            string[] skillIds = pokemon.skills.Split('|');
            skillsTemp.Clear();
            foreach (string s in skillIds)
            {
                int n = int.Parse(s);
                string name = PublicDataManager.instance.GetSkillModelNameCh(n);
                skillsTemp.Add(StringUtil.FormatIdName(n, name));
            }
            for (int i = 0; i < skillDPs.Length; i++)
            {
                skillDPs[i].ClearOptions();
                skillDPs[i].AddOptions(skillsTemp);
                skillDPs[i].value = i;
                skillDPs[i].RefreshShownValue();
            }
            RefreshAbilityValue();
        }
        private void OnSelectCharacter(int id)
        {
            character = PublicDataManager.instance.GetCharacterModel(id);
        }
        //选择性格
        private void OnSelectPersonality()
        {
            personality = PublicDataManager.instance.GetPersonalityModel(personalityDP.captionText.text);
            RefreshAbilityValue();
        }
        //选择携带物
        private void OnSelectItem()
        {
            item = PublicDataManager.instance.GetItemModel(itemDP.captionText.text);
        }
        //设定努力值
        private void OnEffectValueInput(InputField input, string value)
        {
            int v = 0, s = 0;
            if (value != string.Empty)
            {
                v = int.Parse(value);
            }
            if (v > MAX_PRE_EV)
            {
                input.GetComponentInChildren<Text>().color = Color.red;
            }
            else
            {
                input.GetComponentInChildren<Text>().color = Color.black;
                //计算努力值和
                foreach (InputField i in effectValueIFs)
                {
                    if (i.text != string.Empty)
                    {
                        s += int.Parse(i.text);
                    }
                }
                sumText.text = s.ToString();
                if (s > MAX_SUM_EV)
                {
                    sumText.color = Color.red;
                }
                else
                {
                    sumText.color = Color.black;
                    //更新能力值
                    RefreshAbilityValue();
                }
            }
        }
        private void OnSelectSkill()
        {

        }

        private void Save()
        {
            errorText.text = string.Empty;
            List<int> skillIds = new List<int>();
            for (int i = 0; i < skillDPs.Length; i++)
            {
                skillIds.Add(int.Parse(skillDPs[i].captionText.text.Substring(0, 3)));
            }
            //存在重复技能
            if (skillIds.Count() != skillIds.Distinct().Count())
            {
                errorText.text = "技能不能重复选择";
            }
            else
            {
                for (int i = 0; i < skills.Length; i++)
                {
                    skills[i] = PublicDataManager.instance.GetSkillModel(skillIds[i]);
                }
                //各项属性修改为最终能力值
                for (int i = 0; i < abilityValueTexts.Length; i++)
                {
                    switch (i)
                    {
                        case 0: pokemon.hp = int.Parse(abilityValueTexts[i].text); break;
                        case 1: pokemon.attack = int.Parse(abilityValueTexts[i].text); break;
                        case 2: pokemon.defense = int.Parse(abilityValueTexts[i].text); break;
                        case 3: pokemon.sp_attack = int.Parse(abilityValueTexts[i].text); break;
                        case 4: pokemon.sp_defense = int.Parse(abilityValueTexts[i].text); break;
                        case 5: pokemon.speed = int.Parse(abilityValueTexts[i].text); break;
                    }
                }
                Pokemon p = new Pokemon(pokemon, character, personality, item, skills);
                p.isMine = true;
                RuntimeData.SetCurrentMyPokemon(p);
                Close();
            }
        }

        //更新能力值
        private void RefreshAbilityValue()
        {
            for (int i = 0; i < effectValueIFs.Length; i++)
            {
                if (effectValueIFs[i].text != string.Empty)
                {
                    int baseStats = 0;
                    if (pokemon != null)
                    {
                        switch (i)
                        {
                            case 0: baseStats = pokemon.hp; break;
                            case 1: baseStats = pokemon.attack; break;
                            case 2: baseStats = pokemon.defense; break;
                            case 3: baseStats = pokemon.sp_attack; break;
                            case 4: baseStats = pokemon.sp_defense; break;
                            case 5: baseStats = pokemon.speed; break;
                        }
                    }
                    //HP
                    if (i == 0)
                    {
                        int hp = ((MAX_PRE_IV + 2 * baseStats + int.Parse(effectValueIFs[i].text) / 4) * MAX_LVL) / 100 + 10 + MAX_LVL;
                        abilityValueTexts[i].text = hp.ToString();
                    }
                    else
                    {
                        int aV = ((2 * baseStats + MAX_PRE_IV + int.Parse(effectValueIFs[i].text) / 4) * MAX_LVL) / 100 + 5;
                        //性格修正
                        if (personality != null)
                        {
                            if (i == personality.enhance)
                            {
                                aV = Mathf.FloorToInt(aV * 1.1f);
                            }
                            else if (i == personality.reduce)
                            {
                                aV = Mathf.FloorToInt(aV * 0.9f);
                            }
                        }
                        abilityValueTexts[i].text = aV.ToString();
                    }
                }
            }
        }
        private void InitFromPokmeon(Pokemon _p)
        {
            PokemonModel pModel = _p.Model;
            if (pModel != null)
            {
                pokemonDP.value = pokemonDP.options.FindIndex(t => t.text == StringUtil.FormatIdName(pModel.id, pModel.name_ch));
                OnSelectPokemon();

                if (_p.Character != null)
                {
                    CharacterModel pCh = _p.Character.Model;
                    if (pCh != null)
                    {
                        characterDP.value = characterDP.options.FindIndex(t => t.text == StringUtil.FormatIdName(pCh.id, pCh.name_ch));
                        OnSelectCharacter(pCh.id);
                    }
                }
                if (_p.Personality != null)
                {
                    PersonalityModel pPer = _p.Personality.Model;
                    if (pPer != null)
                    {
                        personalityDP.value = personalityDP.options.FindIndex(t => t.text == pPer.name_ch);
                        OnSelectPersonality();
                    }
                }
                if (_p.Item != null)
                {
                    ItemModel pI = _p.Item.Model;
                    if (pI != null)
                    {
                        itemDP.value = itemDP.options.FindIndex(t => t.text == pI.name_ch);
                    }
                    OnSelectItem();
                }
                //能力值,根据能力值计算努力值
                PokemonModel initModel = PublicDataManager.instance.GetPokemonModel(pModel.id);
                for (int i = 0; i < abilityValueTexts.Length; i++)
                {
                    int baseV = 0;
                    float av = 0f;
                    switch (i)
                    {
                        case 0: av = pModel.hp; baseV = initModel.hp; abilityValueTexts[i].text = pModel.hp.ToString(); break;
                        case 1: av = pModel.attack; baseV = initModel.attack; abilityValueTexts[i].text = pModel.attack.ToString(); break;
                        case 2: av = pModel.defense; baseV = initModel.defense; abilityValueTexts[i].text = pModel.defense.ToString(); break;
                        case 3: av = pModel.sp_attack; baseV = initModel.sp_attack; abilityValueTexts[i].text = pModel.sp_attack.ToString(); break;
                        case 4: av = pModel.sp_defense; baseV = initModel.sp_defense; abilityValueTexts[i].text = pModel.sp_defense.ToString(); break;
                        case 5: av = pModel.speed; baseV = initModel.speed; abilityValueTexts[i].text = pModel.speed.ToString(); break;
                    }
                    //性格修正恢复,存在0-4点误差
                    if (i != 0)
                    {
                        if (_p.Personality != null)
                        {
                            if (i == personality.enhance)
                            {
                                av = (av / 1.1f);
                            }
                            else if (i == personality.reduce)
                            {
                                av = (av / 0.9f);
                            }
                        }
                    }
                    //HP
                    if (i == 0)
                    {
                        int hpEv = Mathf.RoundToInt(4 * ((((av - MAX_LVL - 10) * 100f) / (float)MAX_LVL) - MAX_PRE_IV - 2 * baseV));
                        effectValueIFs[i].text = hpEv.ToString();
                    }
                    else
                    {
                        int otherEv = Mathf.RoundToInt(4 * (((av - 5) * 100f / (float)MAX_LVL) - MAX_PRE_IV - 2 * baseV));
                        effectValueIFs[i].text = otherEv.ToString();
                    }
                }
                //技能
                if (_p.Skills != null)
                {
                    for (int i = 0; i < _p.Skills.Length; i++)
                    {
                        SkillModel pSk = _p.Skills[i].Model;
                        if (pSk != null)
                        {
                            skillDPs[i].value = skillDPs[i].options.FindIndex(t => t.text == StringUtil.FormatIdName(pSk.id, pSk.name_ch));
                            OnSelectSkill();
                        }
                    }
                }
            }
        }
        private void Reset()
        {
            //Pokemon及其特性、性格/携带物默认选中列表第一个
            if (pokemonDP.value == 0)
            {
                OnSelectPokemon();
            }
            pokemonDP.value = 0;
            characterDP.value = 0;
            personalityDP.value = 0;
            itemDP.value = 0;
            //清空努力值
            foreach (InputField i in effectValueIFs)
            {
                i.text = "0";
            }
            sumText.text = "0";
            //更新能力值
            RefreshAbilityValue();
        }
    }
}
