using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RDUI
{
    public class BattlePage : BasePage
    {
        public Slot[] slots;
        public GameObject select;
        public Button skillBtn;
        public GameObject skills;
        public Button skillReturnBtn;
        public Button pokemonBtn;
        public Button pokemonReturnBtn;
        public GameObject pokemons;
        protected override void Init()
        {
            for(int i=0;i< slots.Length; i++)
            {
                PokemonModel p = RuntimeData.GetMyPokemonByIndex(i).GetModel();
                Sprite icon = Resources.Load<Sprite>("PokemonSprites/" + StringUtil.FormatId(p.id) + "/Icon/IMG00000");
                slots[i].SetProperty(icon, p.name_ch, p.hp);
            }
            skillBtn.onClick.AddListener(OnClickSkillBtn);
            skillReturnBtn.onClick.AddListener(OnClickSkillReturnBtn);
            base.Init();
        }
        public void ChangePokemon(Pokemon _pokemon)
        {
            int pId = _pokemon.GetModel().id;

        }
        public void OnClickSkillBtn()
        {
            select.SetActive(false);
            skills.SetActive(true);
        }
        public void OnClickSkillReturnBtn()
        {
            skills.SetActive(false);
            select.SetActive(true);
        }
        public void OnClickPokemonBtn()
        {
            select.SetActive(false);
            pokemons.SetActive(true);
        }
        public void OnClickPokemonReturnBtn()
        {
            pokemons.SetActive(false);
            select.SetActive(true);
        }
    }

}
