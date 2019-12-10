using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollecteItem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D aCol)
    {
        BaseItems refItem = aCol.gameObject.GetComponent<BaseItems>();
        if(refItem != null)
        {
            //InventoryManager.Instance.AddItems(aCol.gameObject);
        }
    }
}
