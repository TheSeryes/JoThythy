using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Raccon : EnemyBase
{
    //public AIPath m_AiPath; // name off the script 
   
    [SerializeField]
    private Transform m_RacconGFX;

    private int m_CurrentWayPoint = 0;
    private bool m_ReachedEndOfPath = false;
    private Path m_Path;
    private Seeker m_Seeker;
    private Rigidbody2D m_Rb;

  

    private void Start()
    {
        m_Target = GameManager.Instance.GetPlayerPosition();
        this.enabled = false;
        m_Seeker = GetComponent<Seeker>();
        m_Rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void UpdatePath()
    {
        if(m_Seeker.IsDone())
        {
            m_Seeker.StartPath(m_Rb.position, m_Target.position, OnPathComplete);
        }
    }

    private void FixedUpdate()
    {
        if(m_Path == null)
        {
            return;
        }

        if(m_CurrentWayPoint >= m_Path.vectorPath.Count)
        {
            m_ReachedEndOfPath = true;
            return;
        }
        else
        {
            m_ReachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)m_Path.vectorPath[m_CurrentWayPoint] - m_Rb.position).normalized;
        Vector2 force = direction * m_Speed * Time.deltaTime;

        m_Rb.AddForce(force);

        float distance = Vector2.Distance(m_Rb.position, m_Path.vectorPath[m_CurrentWayPoint]);

        if(distance < m_NextWayPointDistance)
        {
            m_CurrentWayPoint ++;
        }

        //Flip scale image On Axe X
        if(m_Rb.velocity.x >= 0.01f)   
        {
            m_RacconGFX.localScale = new Vector3(-1f,1f,1f);
        }
        else if(m_Rb.velocity.x <= -0.01f)
        {
            m_RacconGFX.localScale = new Vector3(1f,1f,1f);
        }
    }

    private void OnPathComplete(Path aPath)
    {
        if(!aPath.error)                    // pas sur de voir si cest correct de faire ca
        {
            m_Path = aPath;                 // current path est = au path entrer
            m_CurrentWayPoint = 0;          //reset le curent path , for start to ebgining
        }
    }

    private void OnTriggerEnter2D(Collider2D aCol)
    {
        PlayerController refPlayer = aCol.gameObject.GetComponent<PlayerController>();
        if(refPlayer != null)
        {
            AudioManager.Instance.Play2DSFX(m_AudioSFX,Vector2.zero,0.03f,1f,false);
            this.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D aCol)
    {
        PlayerController refPlayer = aCol.gameObject.GetComponent<PlayerController>();

        if(refPlayer != null)
        {           
            this.enabled = false;
        }
    }   
}
