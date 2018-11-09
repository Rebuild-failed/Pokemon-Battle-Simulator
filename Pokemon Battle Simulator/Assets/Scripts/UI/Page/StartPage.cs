using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RDUI
{
    public class StartPage : BasePage
    {
        public GameObject selectPanel;
        public Button editBtn;
        public Button joinBtn;
        public Button createBtn;
        public Button exitBtn;

        public Slot[] slots;
        public GameObject editPanel;
        public Button returnBtn;

        public GameObject WaitPanel;
        public Text WaitText;
        public Button cancelBtn;

        public AudioSource clickAudio;
        protected override void Init()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                int index = i;
                slots[i].editBtn.onClick.AddListener(delegate { OnClickSlot(index); });
            }
            editBtn.onClick.AddListener(OnClickEditBtn);
            joinBtn.onClick.AddListener(OnClickJoinBtn);
            createBtn.onClick.AddListener(OnClickCreatBtn);
            exitBtn.onClick.AddListener(OnClickExitBtn);

            returnBtn.onClick.AddListener(OnClickReturnBtn);

            cancelBtn.onClick.AddListener(OnClickCancelBtn);
            base.Init();
        }
        public override void Open()
        {
            UIDelegateManager.AddObserver(UIMessageType.RefreshParty, RefreshParty);
            UIDelegateManager.AddObserver(UIMessageType.CreateRoomSucceed, ShowCreateRoomSucceed);
            UIDelegateManager.AddObserver(UIMessageType.SearchRoom, ShowSearchRoom);
            //加载阵容
            PFileStream.LoadPokemonFromJsonFile();
            WaitPanel.SetActive(false);
            selectPanel.SetActive(true);
            base.Open();
        }
        public override void Close()
        {
            UIDelegateManager.RemoveObserver(UIMessageType.RefreshParty, RefreshParty);
            UIDelegateManager.RemoveObserver(UIMessageType.CreateRoomSucceed, ShowCreateRoomSucceed);
            UIDelegateManager.RemoveObserver(UIMessageType.SearchRoom, ShowSearchRoom);
            base.Close();
        }
        private void OnClickSlot(int _index)
        {
            clickAudio.PlayOneShot(clickAudio.clip);
            RuntimeData.SetCurrentMyIndex(_index);
            UIManager.instance.OpenPage(PageCollection.EditPage);
        }
        private void OnClickEditBtn()
        {
            clickAudio.PlayOneShot(clickAudio.clip);
            selectPanel.SetActive(false);
            editPanel.SetActive(true);
        }
        private void OnClickJoinBtn()
        {
            clickAudio.PlayOneShot(clickAudio.clip);
            selectPanel.SetActive(false);
            if (!RuntimeData.IsMyPokemonsFull())
            {
                WaitText.text = "请先编辑阵容";
                WaitPanel.SetActive(true);
            }
            else
            {
                PNetworkManager.instance.LanGame(1);
            }
        }
        private void OnClickCreatBtn()
        {
            clickAudio.PlayOneShot(clickAudio.clip);
            selectPanel.SetActive(false);
            if (!RuntimeData.IsMyPokemonsFull())
            {
                WaitText.text = "请先编辑阵容";
                WaitPanel.SetActive(true);
            }
            else
            {
                PNetworkManager.instance.LanGame(0);
            }
        }
        private void OnClickExitBtn()
        {
            clickAudio.PlayOneShot(clickAudio.clip);
            Application.Quit();
        }
        private void OnClickReturnBtn()
        {
            clickAudio.PlayOneShot(clickAudio.clip);
            //返回时保存阵容到文件
            PFileStream.SavePokemonToJsonFile();
            editPanel.SetActive(false);
            selectPanel.SetActive(true);
        }
        private void OnClickCancelBtn()
        {
            clickAudio.PlayOneShot(clickAudio.clip);
            PNetworkManager.instance.Stop();
            WaitPanel.SetActive(false);
            selectPanel.SetActive(true);
        }
        private void RefreshParty(object[] _index)
        {
            if(_index.Length <1)
            {
                Debug.LogError("Refresh Current Party arg lenght <1");
                return;
            }
            int i = (int)_index[0];
            Pokemon p = RuntimeData.GetMyPokemonByIndex(i);
            if(p !=null)
            {
                PokemonModel pModel = p.Model;
                if (pModel != null)
                {
                    Sprite icon = Resources.Load<Sprite>("PokemonSprites/" + StringUtil.FormatId(pModel.id) + "/Icon/IMG00000");
                    slots[i].SetProperty(icon, pModel.name_ch, pModel.hp);
                }
            }        
        }
        private void ShowCreateRoomSucceed(object[] _index)
        {
            WaitText.text = "已创建房间，等待玩家加入";
            WaitPanel.SetActive(true);
        }
        private void ShowSearchRoom(object[] _index)
        {
            WaitText.text = "正在寻找房间";
            WaitPanel.SetActive(true);
        }
    }
}

