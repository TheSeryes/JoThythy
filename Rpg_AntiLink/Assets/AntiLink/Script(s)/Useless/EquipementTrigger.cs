using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipementTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D aCol)
    {
        if(aCol.GetComponent<PlayerController>() != null)
        {
            aCol.GetComponent<PlayerController>().SetEquipement();
            Destroy(gameObject);
        }
    }
}
