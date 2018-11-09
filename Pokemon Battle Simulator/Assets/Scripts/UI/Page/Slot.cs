using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
namespace RDUI
{
    public class Slot : BasePage
    {
        public Button editBtn;
        public Image iconImg;
        public Text nameText;
        public Text nameTextShadow;
        public Text maxHpText;
        public Text maxHpTextShadow;
        public Text currentHpText;
        public Text currentHpTextShadow;
        public Image statuImg;
        protected override void Init()
        {
            if (statuImg.sprite == null)
            {
                statuImg.gameObject.SetActive(false);
            }
            else
            {
                statuImg.gameObject.SetActive(true);
            }
            if (iconImg.sprite == null)
            {
                iconImg.gameObject.SetActive(false);
            }
            else
            {
                iconImg.gameObject.SetActive(true);
            }
            SetSelectedable(true);
            base.Init();
        }
        public void SetProperty(Sprite _icon, string _name, int _hp)
        {
            iconImg.sprite = _icon;
            nameText.text = _name;
            nameTextShadow.text = _name;
            maxHpText.text = _hp.ToString();
            maxHpTextShadow.text = _hp.ToString();
            currentHpText.text = _hp.ToString();
            currentHpTextShadow.text = _hp.ToString();
            statuImg.sprite = null;
            iconImg.gameObject.SetActive(true);
            statuImg.gameObject.SetActive(false);
            SetSelectedable(true);
        }
        public void SetCurrentHp(int _hp)
        {
            currentHpText.text = _hp.ToString();
            currentHpTextShadow.text = _hp.ToString();
        }
        public void SetStatuImg(Sprite _img)
        {
            if (_img != null)
            {
                statuImg.sprite = _img;
                statuImg.gameObject.SetActive(true);
            }
        }
        public void SetSelectedable(bool _canBeSelected)
        {
            gameObject.GetComponent<Button>().interactable = _canBeSelected;
            if (_canBeSelected)
            {
                gameObject.GetComponent<Image>().color = Color.white;
            }
            else
            {
                gameObject.GetComponent<Image>().color = Color.gray;
            }

        }
    }
}

