using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
namespace RDUI
{
    public class SkillBlock : BasePage
    {
        public Text skillName;
        public void SetProperty(string _name)
        {
            skillName.text = _name;
        }
    }
}

