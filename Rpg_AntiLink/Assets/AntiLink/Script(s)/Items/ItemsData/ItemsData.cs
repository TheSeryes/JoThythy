using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ItemsData : ScriptableObject
{
    [SerializeField]
    private string m_NameItems = "";
    public string GetName
    {
        get { return m_NameItems; }
    }

    [SerializeField]
    private Sprite m_Sprite;
    public Sprite GetSprite
    {
        get { return m_Sprite; }
    }

    public abstract void Use();

    public abstract void Destroy();

    public abstract void PlaceItem();

    public abstract void RemoveItem();
}
