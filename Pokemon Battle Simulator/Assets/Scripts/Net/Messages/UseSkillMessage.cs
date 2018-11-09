using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class UseSkillMessage : MessageBase
{
    public static readonly short type = 103;
    public int skillIndex;
    public UseSkillMessage()
    {

    }
    public UseSkillMessage(int _index)
    {
        skillIndex = _index;
    }
}
