using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private eSpawnMapPos m_SpawnDir;

    private void Awake()
    {
        switch(m_SpawnDir)
        {
            case eSpawnMapPos.Base:
            {
                GameManager.Instance.SpawnPos_Base = transform.position;
                break;
            }
            case eSpawnMapPos.Right:
            {
                GameManager.Instance.SpawnPos_Right = transform.position;
                break;
            }
            case eSpawnMapPos.Left:
            {
                GameManager.Instance.SpawnPos_Left = transform.position;
                break;
            }
            case eSpawnMapPos.Up:
            {
                GameManager.Instance.SpawnPos_Up = transform.position;
                break;
            }
            case eSpawnMapPos.Down:
            {
                GameManager.Instance.SpawnPos_Down = transform.position;
                break;
            }
        }
        
    }
}
