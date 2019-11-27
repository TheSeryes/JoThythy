using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    private List<ItemsData> m_ListItems = new List<ItemsData>();
 
    public void AddItems(ItemsData aItem)
    {
        m_ListItems.Add(aItem);
    }
}
