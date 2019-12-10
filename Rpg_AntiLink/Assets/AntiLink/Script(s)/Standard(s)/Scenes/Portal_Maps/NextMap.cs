using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMap : MonoBehaviour
{
    [DrawSO]
    [SerializeField]
    private SceneData m_SceneData;

    [SerializeField]
    private eSpawnMapPos m_NextSpawnPos;
    

    private void OnTriggerEnter2D(Collider2D aCol)
    {
        LevelManager.Instance.LoadScene(m_SceneData, m_NextSpawnPos);
    }


    
}
