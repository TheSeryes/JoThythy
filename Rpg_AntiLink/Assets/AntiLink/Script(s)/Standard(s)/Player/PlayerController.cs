using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eAttackMode { Ranged, Melee };

public class PlayerController : MonoBehaviour
{

    #region Variable(s)
    [SerializeField]
    private Canvas m_Inventory;

    [Header("Component(s)")]
    [SerializeField]
    private BoxCollider2D m_BoxCollider;
    [SerializeField]
    private Rigidbody2D m_Rb;
    [SerializeField]
    private PlayerVisualController m_VisualController;

    [Header("For Movement")]
    private Vector2 m_MoveDir = new Vector2();
    private float m_Horizontal;
    private float m_Vertical;
    private float m_MoveSpeed = 2; //data variable
    private eMoveDir m_MoveDirection;

    [Header("Attack")]
    private Vector2 m_AttackDir = new Vector2();
    private eAttackMode m_attackMode;

    [Header("Prefab(s)")]
    [SerializeField]
    private Projectile m_ArrowPrefab;

    [Header("Data")]
    [SerializeField]
    private PlayerData m_PlayerData;
    private int m_MaxHealth; //data variable
    private int m_CurrentHealth;
    private AudioClip m_WalkSFX;

    

    #endregion

    #region MonoBehaviour(s)
    private void Awake()
    {
        SetupData();
        m_CurrentHealth = m_MaxHealth;
    }

    private void Update()
    {
        CheckInputs();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    #endregion

    #region Behaviour(s)

    ///<summary> Update the movement of the player with the input ask by him </summary> 
    private void MovePlayer()
    {
        m_MoveDir.x = m_Horizontal;
        m_MoveDir.y = m_Vertical;
        m_Rb.velocity = m_MoveDir * m_MoveSpeed;
    }

    ///<summary> Check all the input the player can make </summary>
    private void CheckInputs()
    {
        //for testing
        if(Input.GetKeyDown(KeyCode.P))
        {
            SetupData();
        }

        DirectionInputs();
        if(Input.GetButtonDown("Attack"))
        {
            switch(m_attackMode)
            {
                case eAttackMode.Ranged:
                {
                    Shoot();
                    break;
                }
                case eAttackMode.Melee:
                {
                    MeleeAttack();
                    break;
                }
            }
        }

        InputInventory();
    }

    private void InputInventory()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            Debug.Log("Press i");
            m_Inventory.enabled = true;
        }
    }

    /// <summary>
    /// Initialize our variables to the data.
    /// </summary>
    private void SetupData()
    {
        m_MaxHealth = m_PlayerData.getMaxHealth();
        m_MoveSpeed = m_PlayerData.getMoveSpeed();
        m_WalkSFX = m_PlayerData.GetWalkSFX();
    }
    ///<summary> Get the direction input of the player [UPDATE] and change variable value to move the player and change is direction in [FIXED UPDATE] </summary>
    private void DirectionInputs()
    {
        m_Horizontal = 0;
        m_Vertical = 0;
        
        m_Horizontal = Input.GetAxisRaw("Horizontal");
        if(m_Horizontal == 0)
        {
            m_Vertical = Input.GetAxisRaw("Vertical");
        }
        
        if(m_Horizontal > 0)
        {
            //AudioManager.Instance.Play2DSFX(m_WalkSFX,Vector2.zero,0.02f,1f,false);   Mettre un Meileur Song de Marche dans le Data !
            m_MoveDirection = eMoveDir.Right;
            m_AttackDir = Vector2.right;
        }
        else if(m_Horizontal < 0)
        {
            m_MoveDirection = eMoveDir.Left;
            m_AttackDir = Vector2.left;
        }
        else if(m_Vertical < 0)
        {
            m_MoveDirection = eMoveDir.Down;
            m_AttackDir = Vector2.down;
        }
        else if(m_Vertical > 0)
        {
            m_MoveDirection = eMoveDir.Up;
            m_AttackDir = Vector2.up;
        }

        m_VisualController.UpdateDir(m_MoveDirection);
    }



    ///<summary> Call a raycast in the player direction to check if there's an ennemi in front of him and hit him if yes </summary>
    private void MeleeAttack()
    {

    }

    ///<summary> Get an arrow from the PoolManager and call is UpdateDir() to change is direction, the Projectile.cs script on the arrow do the rest </summary>
    private void Shoot()
    {
        Projectile arrow = PoolManager.Instance.AccesSpawnFromPool(m_ArrowPrefab.name, transform.position, Quaternion.identity).GetComponent<Projectile>();
        arrow.UpdateDir(m_AttackDir);
    }

    public void ReceiveDamage(int aDamage)
    {
        m_CurrentHealth -= aDamage;
        if (m_CurrentHealth <= 0)
        {    
            Death();
        }    
    }

    public void ReceiveHealt(int aHealth)
    { 
        m_CurrentHealth += aHealth;

        if(m_CurrentHealth > m_MaxHealth)
        {
            m_CurrentHealth = m_MaxHealth;
        }
        Debug.Log("Reveice Heal : " + m_CurrentHealth);
    }
    #endregion

    private void Death()
    {
        Destroy(gameObject);
    }
}
