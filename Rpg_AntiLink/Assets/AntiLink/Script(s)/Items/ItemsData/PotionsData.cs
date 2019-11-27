using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Items Data/Potion", fileName = "Potion")]
public class PotionsData : ItemsData
{
    [SerializeField]
    private int m_PotionHp;
    public int GetPotionHp
    {
        get { return m_PotionHp;}
    }

    [SerializeField]
    private int m_PotionSpeed;
    public int GetPotionSpeed
    {
        get{ return m_PotionSpeed;}
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
