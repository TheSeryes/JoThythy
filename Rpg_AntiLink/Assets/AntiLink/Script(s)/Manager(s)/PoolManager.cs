using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    private Dictionary<string, Queue<GameObject>> m_PoolDictionnary;

    public void InitialiseNewPools(PoolObjData aPoolData)
    {
        ErasePool();
        InitialisePools(aPoolData.GetPoolList());
    }

    private void InitialisePools(List<Pool> aPoolList)
    {

        for (int i = 0; i < aPoolList.Count; i++)
        {
            Queue<GameObject> ObjectPool = new Queue<GameObject>();

            for (int k = 0; k < aPoolList[i].amount; k++)
            {
                GameObject tempObj = Instantiate(aPoolList[i].prefab, transform);
                tempObj.SetActive(false);
                ObjectPool.Enqueue(tempObj);
            }

            m_PoolDictionnary.Add(aPoolList[i].prefab.name, ObjectPool);
        }
    }

    ///<summary> Spawn an object from a pools that auto desactivate it-self (You won't get any acces to it). </summary>
    public void FreeSpawnFromPool(string aTag, Vector3 aPosition, Quaternion aRotation)
    {
        if (!m_PoolDictionnary.ContainsKey(aTag))
        {
            Debug.LogWarning("This Tag doesn't exist!");
        }
        else
        {
            GameObject tempObj = GetObject(aTag);
            tempObj.transform.position = aPosition;
            tempObj.transform.rotation = aRotation;
            tempObj.SetActive(true);
            m_PoolDictionnary[aTag].Enqueue(tempObj);
        }
    }

    ///<summary> Spawn an object from a pools that need to be manually desactivate and that you need acces to. </summary>
    public GameObject AccesSpawnFromPool(string aTag, Vector3 aPosition, Quaternion aRotation)
    {
        if (!m_PoolDictionnary.ContainsKey(aTag))
        {
            Debug.LogWarning("This Tag doesn't exist!");
        }
        else
        {
            GameObject tempObj = GetObject(aTag);
            tempObj.transform.position = aPosition;
            tempObj.transform.rotation = aRotation;
            tempObj.SetActive(true);
            m_PoolDictionnary[aTag].Enqueue(tempObj);
            return tempObj;
        }
        return null;
    }

    private GameObject GetObject(string aTag)
    {
        GameObject tempObj = null;

        //Check if there's an available Object
        int maxAmount = m_PoolDictionnary[aTag].Count;
        foreach (GameObject ObjectPool in m_PoolDictionnary[aTag])
        {
            if (!ObjectPool.activeSelf)
            {
                tempObj = m_PoolDictionnary[aTag].Dequeue();
                break;
            }
            maxAmount--;
        }
        //If not, create a new one.
        if(maxAmount == 0)
        {
            GameObject tempCopie = m_PoolDictionnary[aTag].Dequeue();
            tempObj = Instantiate(tempCopie, transform);
            m_PoolDictionnary[aTag].Enqueue(tempCopie);
        }
        return tempObj;
    }

    public void ErasePool()
    {
        m_PoolDictionnary = new Dictionary<string, Queue<GameObject>>();

        if(m_PoolDictionnary.Count > 0)
        {
            m_PoolDictionnary.Clear();
        }
        for(int i = transform.childCount; i > 0; i--)
        {
            Destroy(transform.GetChild(i-1).gameObject);
        }
    }
}
