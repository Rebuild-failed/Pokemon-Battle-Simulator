using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class UseSkillMessage : MessageBase
{
    public int skillIndex;
    public UseSkillMessage(int _index)
    {
        skillIndex = _index;
    }
}
