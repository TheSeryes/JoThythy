using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerData", fileName = "new PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private int m_maxHealth;
    public int getMaxHealth()
    {
        return m_maxHealth;
    }

    [SerializeField]
    private float m_MoveSpeed;
    public float getMoveSpeed()
    {
        return m_MoveSpeed;
    }

    [SerializeField]
    private AudioClip m_WalkSFX;
    public AudioClip GetWalkSFX()
    {
        return m_WalkSFX;
    }
}
