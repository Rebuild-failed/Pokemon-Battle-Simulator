using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RDUI
{
    public class StartPage : BasePage
    {
        public Slot[] slots;
        public Button joinBtn;
        public Button createBtn;
        public Button exitBtn;
        protected override void Init()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                int index = i;
                slots[i].editBtn.onClick.AddListener(delegate { OnClickSlot(index); });
            }
            joinBtn.onClick.AddListener(OnClickJoinBtn);
            createBtn.onClick.AddListener(OnClickCreatBtn);
            exitBtn.onClick.AddListener(OnClickExitBtn);
            base.Init();
        }
        public override void Open()
        {
            UIDelegateManager.AddObserver(UIMessageType.RefreshParty, RefreshCurrentParty);
            base.Open();
        }
        public override void Close()
        {
            UIDelegateManager.RemoveObserver(UIMessageType.RefreshParty, RefreshCurrentParty);
            base.Close();
        }
        private void OnClickSlot(int _index)
        {
            RuntimeData.SetCurrentMyIndex(_index);
            UIManager.instance.OpenPage(PageCollection.EditPage);
        }
        private void OnClickJoinBtn()
        {
            PNetworkManager.LanGame(1);
        }
        private void OnClickCreatBtn()
        {
            if (!RuntimeData.IsMyPokemonsFull())
            {
                return;
            }
            RuntimeData.SetCurrentMyIndex(0);
            RuntimeData.SetCurrentOppIndex(0);
            PNetworkManager.LanGame(0);
        }
        private void OnClickExitBtn()
        {

        }
        private void RefreshCurrentParty(object _index)
        {
            PokemonModel p = RuntimeData.GetCurrentMyPokemon().GetModel();
            Sprite icon = Resources.Load<Sprite>("PokemonSprites/" + StringUtil.FormatId(p.id) + "/Icon/IMG00000");
            slots[(int)_index].SetProperty(icon, p.name_ch, p.hp);
        }
    }
}

