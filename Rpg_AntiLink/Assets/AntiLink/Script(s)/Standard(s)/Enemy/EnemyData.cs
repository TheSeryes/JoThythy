using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/EnemyData",fileName = "Enemy")]
public class EnemyData : ScriptableObject
{
    //Déplacement  
    [SerializeField]
    private float m_NextWayPointDistance = 3f;
    [SerializeField]
    private float m_Speed = 10f;
    [SerializeField]
    private float m_RotationSpeed = 5f;
    [SerializeField]
    private Transform m_Target ;

    //Combat
    [SerializeField]
    private int m_MaxHp = 10;
    [SerializeField]
    private int m_Armor = 10;
    [SerializeField]
    private int m_AttackDamage = 10;

    //Audio Enemy
    [SerializeField]
    private AudioClip m_AudioSFX;
   

    #region Getter: Audio
    public AudioClip GetAudioSFX()
    {
        return m_AudioSFX;
    }

    #endregion

    #region Getter: Move with Path 
    public Transform GetTarget()
    {
        return m_Target;
    }

    public float GetPointDistance()
    {
        return m_NextWayPointDistance;
    }
    #endregion

    #region Getter: Stats de base
    public float GetSpeed()
    {
        return m_Speed;
    }

    public float GetRotationSpeed()
    {
        return m_RotationSpeed;
    }

    public int GetHp()
    {
        return m_MaxHp;
    }

    public int GetArmor()
    {
        return m_Armor;
    }

    public int GetAttackDamage()
    {
        return m_AttackDamage;
    }
    #endregion
}
