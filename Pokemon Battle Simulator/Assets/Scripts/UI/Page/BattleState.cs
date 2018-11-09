using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RDUI
{
    public class BattleState : BasePage
    {
        public Text nameText;
        public Text nameShadowText;
        public Image hpBar;
        public Image statuImg;
        public Text maxHPText;
        public Text maxHPShdowText;
        public Text currentHPText;
        public Text currentHPShadowText;

        public void SetProperty(string _name, int _maxHp, int _currentHp, Sprite _img)
        {
            SetName(_name);
            SetCurrentHP(_currentHp);
            SetMaxHP(_maxHp);
            SetHpBar((float)_currentHp / (float)_maxHp);
            SetStatuImg(_img);
        }
        public void SetName(string _name)
        {
            nameText.text = _name;
            nameShadowText.text = _name;
        }
        public void SetMaxHP(int _maxHp)
        {
            if (maxHPText)
            {
                maxHPText.text = _maxHp.ToString();
            }
            if (maxHPShdowText)
            {
                maxHPShdowText.text = _maxHp.ToString();
            }
        }
        public void SetCurrentHP(int _hp)
        {
            if (_hp < 0)
            {
                _hp = 0;
            }
            if (currentHPText)
            {
                currentHPText.text = _hp.ToString();
            }
            if (currentHPShadowText)
            {
                currentHPShadowText.text = _hp.ToString();
            }
        }
        public void SetHpBar(float _rate)
        {
            hpBar.fillAmount = _rate;
        }
        public void SetStatuImg(Sprite _img)
        {
            if (_img == null)
            {
                statuImg.gameObject.SetActive(false);
            }
            else
            {
                statuImg.sprite = _img;
                statuImg.gameObject.SetActive(true);
            }
        }
    }
}

