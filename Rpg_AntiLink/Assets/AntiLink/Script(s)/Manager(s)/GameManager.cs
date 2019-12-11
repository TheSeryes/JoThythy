using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private GameObject m_PlayerPrefab;  //The prefab of the player that we spawn when the game start
   

    private Vector2 m_SpawnPos_Base;
    public Vector2 SpawnPos_Base
    {
        set {m_SpawnPos_Base = value;}
    }
    private Vector2 m_SpawnPos_Up;
    public Vector2 SpawnPos_Up
    {
        set {m_SpawnPos_Up = value;}
    }
    private Vector2 m_SpawnPos_Right;
    public Vector2 SpawnPos_Right
    {
        set {m_SpawnPos_Right = value;}
    }
    private Vector2 m_SpawnPos_Left;
    public Vector2 SpawnPos_Left
    {
        set {m_SpawnPos_Left = value;}
    }
    private Vector2 m_SpawnPos_Down;
    public Vector2 SpawnPos_Down
    {
        set {m_SpawnPos_Down = value;}
    }

    private PlayerController m_Player;  //The PlayerController on the actual Player Obj
    public PlayerController Player
    {
        get{  return m_Player; }
    }

    private Bird m_Bird;  //The PlayerController on the actual Player Obj
    public Bird Bird
    {
        get{  return m_Bird; }
        set{m_Bird = value;}
    }

    ///<summary> Return the actual transform of the player.
    /// Can be usefull to set a target to allies or ennemis </summary>
    public Transform GetPlayerPosition()
    {
        return m_Player.gameObject.transform;
    }

    [SerializeField]
    private GameObject m_Canvas;

    private float m_CurrentTime = 5f;



    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        m_Player = Instantiate(m_PlayerPrefab, transform).GetComponent<PlayerController>();
        m_Player.transform.position = m_SpawnPos_Base;
    }

    ///<summary> Change the player position for a new scene (map) [Do not respawn for real] </summary>
    public void RespawnPlayer(eSpawnMapPos aSpawnMap)
    {
        switch(aSpawnMap)
        {
            case eSpawnMapPos.Base:
            {
                m_Player.gameObject.transform.position = m_SpawnPos_Base;
                break;
            }
            case eSpawnMapPos.Down:
            {
                m_Player.gameObject.transform.position = m_SpawnPos_Up;
                break;
            }
            case eSpawnMapPos.Up:
            {
                m_Player.gameObject.transform.position = m_SpawnPos_Down;
                break;
            }
            case eSpawnMapPos.Right:
            {
                m_Player.gameObject.transform.position = m_SpawnPos_Left;
                break;
            }
            case eSpawnMapPos.Left:
            {
                m_Player.gameObject.transform.position = m_SpawnPos_Right;
                break;
            }
        }        
    }

    private void GameOver()
    {
        if(m_Player.GetIsDead == true)
        {
            m_Canvas.SetActive(true);
        }
    }
}
