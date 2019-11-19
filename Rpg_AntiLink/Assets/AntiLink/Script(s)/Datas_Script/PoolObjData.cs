using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct Pool
{
    public GameObject prefab;
    public int amount;
}

[CreateAssetMenu(menuName="Data/PoolObjList", fileName="new PoolObjs")]
public class PoolObjData : ScriptableObject
{
    [SerializeField]
    private List<Pool> m_Pools = new List<Pool>();

    public List<Pool> GetPoolList()
    {
        return m_Pools;
    }
}
