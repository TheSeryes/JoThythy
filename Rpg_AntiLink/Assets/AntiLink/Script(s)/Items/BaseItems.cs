﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BaseItems : MonoBehaviour 
{     
    [SerializeField]
    [DrawSO]
    private ItemsData m_ItemsDatas;

    private void OnTriggerEnter2D(Collider2D aCol)
    {
        PlayerController refPlayer = aCol.gameObject.GetComponent<PlayerController>();

        if(refPlayer != null)
        {
            InventoryManager.Instance.AddItems(m_ItemsDatas);
            GameManager.Instance.Bird.GetGotoLoot = false;
            Destroy(gameObject);
        }

        CollecteItem refColelcte = aCol.gameObject.GetComponent<CollecteItem>();
        if(refColelcte != null)
        {
            GameManager.Instance.Player.AddArrow(5);
            GameManager.Instance.Bird.GetGotoLoot = false;

            Destroy(gameObject);
        }
    }
}
