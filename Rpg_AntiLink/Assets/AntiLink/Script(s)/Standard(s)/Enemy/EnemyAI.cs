using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : EnemyBase
{
    //public AIPath m_AiPath; // name off the script 

    [SerializeField]
    private Transform m_EnemyGFX;
    [SerializeField]
    private UI m_UI;

    private int m_CurrentWayPoint = 0;
    private bool m_ReachedEndOfPath = false;
    private Path m_Path;
    private Seeker m_Seeker;
    private Rigidbody2D m_Rb;
    private int m_CurrentHP;

    private float m_TimerBetweenAttack = 2f;
    private float m_CurrentTime;

    [SerializeField]
    private LayerMask m_layer;


    private void Start()
    {
        m_Target = GameManager.Instance.GetPlayerPosition();
        this.enabled = false;
        m_Seeker = GetComponent<Seeker>();
        m_Rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);

        m_CurrentHP = m_MaxHp;
    }

    private void UpdatePath()
    {
        if (m_Seeker.IsDone())
        {
            m_Seeker.StartPath(m_Rb.position, m_Target.position, OnPathComplete);
        }
    }

    private void Update()
    {
        RaycastHit2D m_Ray;
        if (Physics2D.CircleCast(transform.position, 1f, transform.up, 0f, m_layer))
        {
            m_Ray = Physics2D.CircleCast(transform.position, 1f, transform.up, 0f, m_layer);
            if (m_CurrentTime <= 0)
            {
                if (m_Ray.transform.GetComponent<PlayerController>() != null)
                {
                    m_Ray.transform.GetComponent<PlayerController>().ReceiveDamage(m_AttackDamage);
                    m_CurrentTime = m_TimerBetweenAttack;
                }
            }
        }

        m_CurrentTime -= Time.deltaTime;

    }

    private void FixedUpdate()
    {
        if (m_Path == null)
        {
            return;
        }

        if (m_CurrentWayPoint >= m_Path.vectorPath.Count)
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

        if (distance < m_NextWayPointDistance)
        {
            m_CurrentWayPoint++;
        }

        //Flip scale image On Axe X
        if (m_Rb.velocity.x >= 0.01f)
        {
            m_EnemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (m_Rb.velocity.x <= -0.01f)
        {
            m_EnemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void OnPathComplete(Path aPath)
    {
        if (!aPath.error)                    // pas sur de voir si cest correct de faire ca
        {
            m_Path = aPath;                 // current path est = au path entrer
            m_CurrentWayPoint = 0;          //reset le curent path , for start to ebgining
        }
    }

    public void ReceiveDamage(int aDamage)
    {
        m_CurrentHP -= aDamage;
        m_UI.UpdateHp((float)m_CurrentHP / (float)m_MaxHp);

        if (m_CurrentHP <= 0)
        {
            Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D aCol)
    {
        PlayerController refPlayer = aCol.gameObject.GetComponent<PlayerController>();
        if (refPlayer != null)
        {
            AudioManager.Instance.Play2DSFX(m_AudioSFX, Vector2.zero, 0.03f, 1f, false);
            this.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D aCol)
    {
        PlayerController refPlayer = aCol.gameObject.GetComponent<PlayerController>();

        if (refPlayer != null)
        {
            this.enabled = false;
        }
    }
}
