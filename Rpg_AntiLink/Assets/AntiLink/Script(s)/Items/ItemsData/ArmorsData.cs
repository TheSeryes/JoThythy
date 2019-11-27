using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Items Data/Armor", fileName = "Armor")]
public class ArmorsData : ItemsData
{
    [SerializeField]
    private int m_Armor;
    public int GetArmor
    {
        get{ return m_Armor;}
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
