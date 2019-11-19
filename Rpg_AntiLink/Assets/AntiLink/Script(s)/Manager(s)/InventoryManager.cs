using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  InventoryManager: Singleton<InventoryManager>
{
    [SerializeField]
    private GameObject m_InventoryUI;

    [Header("Items")]
    [SerializeField]
    private Transform m_ItemsContainer;

    [SerializeField]
    private GameObject m_ItemSlotPrefabs;


    private List<ItemSlot> m_ItemSlot = new List<ItemSlot>(); // add an other scrip for items slots 

    private List<BaseItem> m_Item = new List<BaseItem>(); // code for what a base item is


    private void Start()
    {

    }

    private void Update()
    {

    }

    public void AddItem(BaseItem aItem)
    {

    }

    public void SelectItem(int aSlotIndex)
    {

    }

    public void UpdateItemSlot()
    {

    }
    private void CreateItemSlot()
    {

    }

    //we do not have other character that the stats can be affected 
    //(most find inventory that we did in session 2 for ref)




}
