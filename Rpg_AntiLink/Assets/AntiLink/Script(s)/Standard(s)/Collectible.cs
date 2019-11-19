using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    private CollectibleObj m_Obj;

    public void Collect()
    {
        //Ajoute au inventoryManager l'objet
        //InventoryManager.Instance.AddToInventory(m_Obj);
    }
}
