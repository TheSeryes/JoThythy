using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum eAttackMode { None, Ranged, Melee };

public class PlayerController : MonoBehaviour
{

    #region Variable(s)
    [SerializeField]
    private Canvas m_Inventory;
    [SerializeField]
    private UI m_UIHpPlayer;

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
    [SerializeField]
    private int m_SwordDamage;
    private eAttackMode m_attackMode;
    private int m_ArrowAmount = 10;
    public int ArrowAmount
    {
        get { return m_ArrowAmount; }
    }
    [SerializeField]
    private LayerMask m_EnnemiLayer;

    [Header("Prefab(s)")]
    [SerializeField]
    private Projectile m_ArrowPrefab;

    [Header("Data")]
    [SerializeField]
    private PlayerData m_PlayerData;
    private int m_MaxHealth; //data variable
    private int m_CurrentHealth;
    private AudioClip m_WalkSFX;


    [SerializeField]
    private GameObject m_Canvas;

    private bool m_HaveEquipement = false;
    private float m_CurrentTime = 5f;

    private bool m_IsDead = false;
    public bool GetIsDead
    {
        get{return m_IsDead;}
    }


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

        if(m_IsDead)
        {
            m_CurrentTime -= Time.deltaTime;
            if(m_CurrentTime <= 0)
            {
                SceneManager.LoadScene("GameMainMenu");
            }
        }
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            SetupData();
        }

        DirectionInputs();
        if (Input.GetButtonDown("Attack"))
        {
            switch (m_attackMode)
            {
                case eAttackMode.Ranged:
                    {
                        if (m_ArrowAmount > 0)
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
        if (Input.GetButtonDown("Inventory"))
        {
            Debug.Log("Press i");
            m_Inventory.enabled = true;
        }

        if (m_HaveEquipement)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (m_attackMode == eAttackMode.Ranged)
                {
                    m_VisualController.UpdateBaseLook(eBaseLook.WithHat);
                    m_attackMode = eAttackMode.None;
                }
                else
                {
                    m_VisualController.UpdateBaseLook(eBaseLook.WithHat_Crossbow);
                    m_attackMode = eAttackMode.Ranged;
                }
                m_VisualController.UpdateDir(m_MoveDirection);
            }

            else if (Input.GetKeyDown(KeyCode.Q))
            {
                if (m_attackMode == eAttackMode.Melee)
                {
                    m_VisualController.UpdateBaseLook(eBaseLook.WithHat);
                    m_attackMode = eAttackMode.None;
                }
                else
                {
                    m_VisualController.UpdateBaseLook(eBaseLook.WithHat_Sword);
                    m_attackMode = eAttackMode.Melee;
                }
                m_VisualController.UpdateDir(m_MoveDirection);
            }
        }

    }

    public void SetEquipement()
    {
        m_HaveEquipement = true;
        m_VisualController.UpdateBaseLook(eBaseLook.WithHat);
        m_VisualController.UpdateDir(m_MoveDirection);
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
        if (m_Horizontal == 0)
        {
            m_Vertical = Input.GetAxisRaw("Vertical");
        }

        if (m_Horizontal > 0)
        {
            //AudioManager.Instance.Play2DSFX(m_WalkSFX,Vector2.zero,0.02f,1f,false);   Mettre un Meileur Song de Marche dans le Data !
            m_MoveDirection = eMoveDir.Right;
            m_VisualController.SetAnimType(true);
        }
        else if (m_Horizontal < 0)
        {
            m_MoveDirection = eMoveDir.Left;
            m_VisualController.SetAnimType(true);
        }
        else if (m_Vertical < 0)
        {
            m_MoveDirection = eMoveDir.Down;
            m_VisualController.SetAnimType(true);
        }
        else if (m_Vertical > 0)
        {
            m_MoveDirection = eMoveDir.Up;
            m_VisualController.SetAnimType(true);
        }
        else
        {
            m_VisualController.SetAnimType(false);
        }

        m_VisualController.UpdateDir(m_MoveDirection);
    }



    ///<summary> Call a raycast in the player direction to check if there's an ennemi in front of him and hit him if yes </summary>
    private void MeleeAttack()
    {
        RaycastHit2D m_Ray;
        if (Physics2D.CircleCast(transform.position, 1.5f, transform.up, 0f, m_EnnemiLayer))
        {
            m_Ray = Physics2D.CircleCast(transform.position, 1.5f, transform.up, 0f, m_EnnemiLayer);

            if (m_Ray.transform.GetComponent<EnemyAI>() != null)
            {
                m_Ray.transform.GetComponent<EnemyAI>().ReceiveDamage(m_SwordDamage);
            }
        }
    }

    ///<summary> Get an arrow from the PoolManager and call is UpdateDir() to change is direction, the Projectile.cs script on the arrow do the rest </summary>
    private void Shoot()
    {
        m_ArrowAmount--;
        Projectile arrow = PoolManager.Instance.AccesSpawnFromPool(m_ArrowPrefab.name, transform.position, Quaternion.identity).GetComponent<Projectile>();
        arrow.UpdateDir(m_MoveDirection);
    }

    public void ReceiveDamage(int aDamage)
    {
        m_CurrentHealth -= aDamage;
        m_UIHpPlayer.UpdateHp((float)m_CurrentHealth / (float)m_MaxHealth);
        if (m_CurrentHealth <= 0)
        {
            Death();
        }
    }

    public void AddArrow(int aAmount)
    {
        m_ArrowAmount += aAmount;
    }

    public void ReceiveHealt(int aHealth)
    {
        m_CurrentHealth += aHealth;

        if (m_CurrentHealth > m_MaxHealth)
        {
            m_CurrentHealth = m_MaxHealth;
        }
        m_UIHpPlayer.UpdateHp((float)m_CurrentHealth / (float)m_MaxHealth);
        Debug.Log("Reveice Heal : " + m_CurrentHealth);
    }
    #endregion

    private void Death()
    {
        m_IsDead = true;
        Destroy(gameObject);
    }
}
