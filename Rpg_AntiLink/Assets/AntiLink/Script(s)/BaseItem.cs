using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem : ScriptableObject
{
    public Sprite m_Icon;

    public abstract bool DeleteOnUse {get; }

    public abstract void Use(PlayerController aPlayer);
}
