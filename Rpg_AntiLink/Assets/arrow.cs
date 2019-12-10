using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D aCol)
    {
        CollecteItem refColelcte = aCol.gameObject.GetComponent<CollecteItem>();

        if(refColelcte != null)
        {
            GameManager.Instance.Player.AddArrow(5);
            GameManager.Instance.Bird.GetGotoLoot = false;

            Destroy(gameObject);
        }
    }
}
