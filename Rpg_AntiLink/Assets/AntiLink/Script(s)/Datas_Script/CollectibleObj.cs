using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Collectible", fileName = "new Collectible")]
public class CollectibleObj : ScriptableObject
{
    [SerializeField]
    private GameObject m_Prefab;

    public GameObject GetCollectible()
    {
        return m_Prefab;
    }
}
