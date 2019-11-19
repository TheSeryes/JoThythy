using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCAI : MonoBehaviour
{
    [SerializeField]
    private Transform[] m_MovePoint;
    [SerializeField]
    private Canvas m_Canvas;
    [SerializeField]
    private float m_Speed = 0f;
    [SerializeField]
    private float m_StartWaitTime = 0f;
 
    
    private int m_RandomSpot = 0;
    private float m_WaitTime = 0f;
    private float m_SpeedBegin;

    private bool m_PlayerContact = true;  

    private void Start()
    {
        m_Canvas.enabled = false;

        m_PlayerContact = true;
        m_SpeedBegin = m_Speed;
        m_WaitTime = m_StartWaitTime;
        m_RandomSpot = Random.Range(0, m_MovePoint.Length);
    }

    private void Update()
    {
        if(m_PlayerContact)
        {
            transform.position = Vector2.MoveTowards(transform.position, m_MovePoint[m_RandomSpot].position, m_Speed * Time.deltaTime);

            if( Vector2.Distance(transform.position, m_MovePoint[m_RandomSpot].position) < 0.2f)
            {
                if(m_WaitTime <= 0)
                {
                    m_RandomSpot = Random.Range(0, m_MovePoint.Length);
                    m_WaitTime = m_StartWaitTime;
                }
                else
                {
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
        else
        {
            // Pop du dialogue ici ? 
        }
    }

    private void OnTriggerEnter2D(Collider2D aCol)
    {
        PlayerController refPlayer = aCol.gameObject.GetComponent<PlayerController>();
        Debug.Log("Player Trigger");
        if(refPlayer != null)
        {
            m_Canvas.enabled = true;
            m_PlayerContact = false;
           
        }
    }

    private void OnTriggerExit2D(Collider2D aCol)
    {
        PlayerController refPlayer = aCol.gameObject.GetComponent<PlayerController>();

        if(refPlayer !=null )
        {
            m_Canvas.enabled = false;
            m_RandomSpot = Random.Range(0, m_MovePoint.Length);
            m_Speed = m_SpeedBegin;
            m_PlayerContact = true;
        }
    }




   
}
