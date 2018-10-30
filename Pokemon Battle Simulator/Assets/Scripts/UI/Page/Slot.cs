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
        protected override void Init()
        {
            base.Init();
        }
        public void SetProperty(Sprite _icon,string _name,int _hp)
        {
            iconImg.sprite = _icon;
            nameText.text = _name;
            nameTextShadow.text = _name;
            maxHpText.text = _hp.ToString();
            maxHpTextShadow.text = _hp.ToString();
            currentHpText.text = _hp.ToString();
            currentHpTextShadow.text = _hp.ToString();
        }
    }
}

