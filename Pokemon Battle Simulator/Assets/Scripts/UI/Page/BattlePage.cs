using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RDUI
{
    public class BattlePage : BasePage
    {
        public Slot[] slots;
        public GameObject selectPanel;
        public Button skillBtn;
        public GameObject skillPanel;
        public Button skillReturnBtn;
        public Button pokemonBtn;
        public Button pokemonReturnBtn;
        public GameObject pokemonPanel;
        public BattleState myState;
        public BattleState opponentState;
        protected override void Init()
        {
            skillBtn.onClick.AddListener(OnClickSkillBtn);
            skillReturnBtn.onClick.AddListener(OnClickSkillReturnBtn);
            pokemonBtn.onClick.AddListener(OnClickPokemonBtn);
            pokemonReturnBtn.onClick.AddListener(OnClickPokemonReturnBtn);
            base.Init();
        }
        public override void Open()
        {
            //初始化Pokemon阵容
            for (int i = 0; i < slots.Length; i++)
            {
                PokemonModel p = RuntimeData.GetMyPokemonByIndex(i).GetModel();
                Sprite icon = Resources.Load<Sprite>("PokemonSprites/" + StringUtil.FormatId(p.id) + "/Icon/IMG00000");
                slots[i].SetProperty(icon, p.name_ch, p.hp);
                int index = i;
                //更换Pokemon
                slots[i].gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    RuntimeData.SetCurrentMyIndex(index);
                    ChangePokemon(RuntimeData.GetCurrentMyPokemon(), true);
                });
            }
            //默认第一个Pokemon先上场
            ChangePokemon(RuntimeData.GetCurrentMyPokemon(),true);
            ChangePokemon(RuntimeData.GetCurrentOppPokemon(), false);
            UIDelegateManager.AddObserver(UIMessageType.RefreshMyHpText, RefreshMyHpText);
            UIDelegateManager.AddObserver(UIMessageType.RefreshMyHpBar, RefreshMyHpBar);
            UIDelegateManager.AddObserver(UIMessageType.RefreshOpponentHpBar, RefreshOpponentHpBar);
            base.Open();
        }
        public override void Close()
        {
            UIDelegateManager.RemoveObserver(UIMessageType.RefreshMyHpText, RefreshMyHpText);
            UIDelegateManager.RemoveObserver(UIMessageType.RefreshMyHpBar, RefreshMyHpBar);
            UIDelegateManager.RemoveObserver(UIMessageType.RefreshOpponentHpBar, RefreshOpponentHpBar);
            base.Close();
        }
        private void RefreshMyHpText(object _value)
        {
            myState.SetCurrentHP((int)_value);
        }
        private void RefreshMyHpBar(object _value)
        {
            myState.SetHpBar((float)_value);
        }
        private void RefreshOpponentHpBar(object _value)
        {
            opponentState.SetHpBar((float)_value);
        }
        //更换精灵，0自己 其它为对手
        public void ChangePokemon(Pokemon _pokemon, bool _isMyPokemon)
        {
            PokemonModel pModel = _pokemon.GetModel();
            Sprite statuImg = null;
            //if (_pokemon.GetStatu() != null)
            //{
            //   statuImg=Resources.Load<Sprite>()
            //}
            if (_isMyPokemon)
            {
                myState.SetProperty(pModel.name_ch, pModel.hp, _pokemon.CurrentHp, statuImg);
            }
            else
            {
                opponentState.SetProperty(pModel.name_ch, pModel.hp, _pokemon.CurrentHp, statuImg);
            }

        }
        public void OnClickSkillBtn()
        {
            selectPanel.SetActive(false);
            skillPanel.SetActive(true);
        }
        public void OnClickSkillReturnBtn()
        {
            skillPanel.SetActive(false);
            selectPanel.SetActive(true);
        }
        public void OnClickPokemonBtn()
        {
            selectPanel.SetActive(false);
            pokemonPanel.SetActive(true);
        }
        public void OnClickPokemonReturnBtn()
        {
            pokemonPanel.SetActive(false);
            selectPanel.SetActive(true);
        }
    }

}
