using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    private Image m_IconVisual;

    [SerializeField]
    private Button m_Button;

    [SerializeField]
    private GameObject m_SeleectedGO;

    private int m_SlotIndex;
    private BaseItem m_BaseItem;

    public void Setup(int aSlotIndex)
    {

    }

/*  public void SetItem(BaseItems aItem)
    {

    }*/

    public void Select(bool aSelect)
    {

    }

    private void OnClick()
    {
        if(m_BaseItem != null)
        {
            //Inventory.Instance.SelectItem(m_SlotIndex);
        }
    }
}
