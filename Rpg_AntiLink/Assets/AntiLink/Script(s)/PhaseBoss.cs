using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseBoss : EnemyBase
{
    [SerializeField]
    private Rigidbody2D m_Rb;
    [SerializeField]
    private UI m_UI;

    private int m_CountRage = 0;
    private int m_CountTeleport = 0;
    private int m_CountHealing = 0;

    private float m_TimeModeRage = 10f;
    private bool m_CanRage = false;
    private int m_CurrentHp;

    private void Start()
    {
        m_Target = GameManager.Instance.GetPlayerPosition();

        m_CurrentHp = m_MaxHp;
    }

    private void Update()
    {
        if (m_CanRage)
        {
            ChronoRage();
        }

        ConditionsAbility();
    }

    private void FixedUpdate()
    {
        BossMove();
    }

    private void BossMove()
    {
        if (m_Target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, m_Target.position, m_Speed * Time.deltaTime);
        }
    }

    public void BossReceiveDamage(int aDamage)
    {
        m_CurrentHp -= aDamage;
        m_UI.UpdateHp((float)m_CurrentHp/(float)m_MaxHp);

        if (m_CurrentHp < 0)
        {
            Death();
        }
    }

    private void ConditionsAbility()
    {
        if (m_MaxHp < 150 && m_MaxHp > 100)
        {
            Teleport();
        }
        if (m_MaxHp < 80 && m_MaxHp > 30)
        {
            HealingSelf();
        }
        else if (m_MaxHp > 1 && m_MaxHp < 20)
        {
            m_CanRage = true;
            RageInvinsible();
        }
    }
     
    #region Abiltys
    private void Teleport()
    {
        Debug.Log("Teleport Fonction");

        if (m_CountTeleport < 1)
        {
            transform.position = m_Target.transform.position;
            GameManager.Instance.Player.ReceiveDamage(20);
            m_CountTeleport++;
        }
    }

    private void HealingSelf()
    {
        Debug.Log("HealingSelf Fonction");

        if (m_CountHealing < 2)
        {
            m_MaxHp += 50;
            m_CountHealing++;
        }
    }

    private void RageInvinsible()
    {
        Debug.Log("RageInvinsible Fonction");

        if (m_TimeModeRage > 0)
        {
            m_MaxHp = 10000;
            m_Speed = 1.2f;
            m_AttackDamage = 20;
        }
    }

    private void WeakBoss()
    {
        Debug.Log("WeakBoss Fonction");

        m_MaxHp = 1;
        m_Speed = 0.6f;
        m_AttackDamage = 5;
        m_CountRage++;
    }

    private void ChronoRage()
    {
        Debug.Log("ChronoRage Fonction");

        if (m_CanRage)
        {
            m_TimeModeRage -= Time.deltaTime;

            if (m_TimeModeRage <= 0)
            {
                m_CanRage = false;
                WeakBoss();
            }
        }
    }
    #endregion


    private void OnCollisionEnter2D(Collision2D aCol)
    {
        Debug.Log("EnterPlayer" + aCol.gameObject.name);
        PlayerController refPlayer = aCol.gameObject.GetComponent<PlayerController>();
        
        if(refPlayer != null)
        {
            refPlayer.ReceiveDamage(m_AttackDamage);
        }
    }
}
