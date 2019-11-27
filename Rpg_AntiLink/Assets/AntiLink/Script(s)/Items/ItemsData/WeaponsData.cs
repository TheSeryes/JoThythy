using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Items Data/Weapon", fileName = "Weapon")]
public  class WeaponsData : ItemsData
{
    [SerializeField]
    private int m_Damage;
    public int GetDamage
    {
        get { return m_Damage;}
    }

    public override void Use()
    {

    }

    public override void Destroy()
    {

    }

    public override void PlaceItem()
    {

    }

    public override void RemoveItem()
    {

    }
}
