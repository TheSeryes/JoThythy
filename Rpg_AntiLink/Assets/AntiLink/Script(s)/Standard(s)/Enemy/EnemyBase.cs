using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    protected EnemyData m_Data;

    // Variables Move with Path
    protected Transform m_Target;
    protected float m_NextWayPointDistance;

    // Variables Stats de base
    protected float m_Speed;
    protected float m_RotationSpeed;
    protected int m_MaxHp;
    protected int m_Armor;
    protected int m_AttackDamage;

    // Variables Audio SFX
    protected AudioClip m_AudioSFX;
    
    virtual protected void Awake()
    {
        SetupData();
    }


    virtual public void DealDamage(int aDamage)
    {
        
    }

    protected void Death()
    {
        Destroy(gameObject);
    }

    private void SetupData()
    {
        //Set Variables Move with Path
        m_Target = m_Data.GetTarget();
        m_NextWayPointDistance = m_Data.GetPointDistance();

        //Set Variables Stats Base
        m_Speed = m_Data.GetSpeed();
        m_RotationSpeed = m_Data.GetRotationSpeed();
        m_MaxHp = m_Data.GetHp();
        m_Armor = m_Data.GetArmor();
        m_AttackDamage = m_Data.GetAttackDamage();

        //Set Variables Audio
        m_AudioSFX = m_Data.GetAudioSFX();
    }
}
