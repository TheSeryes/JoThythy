using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D m_Rb;
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private int m_Damage = 10;

    [SerializeField]
    private float m_LifeTime = 2.0f;
    private float m_CurrentTime = 0.0f;

    [SerializeField]
    private SpriteRenderer m_Renderer;
    
    private void OnEnable()
    {
        m_CurrentTime = 0.0f;
    }

    private void Update()
    {
        if(m_CurrentTime >= m_LifeTime && gameObject.activeSelf)
        {
            DisableArrow();
        }
        else
        {
            m_CurrentTime += Time.deltaTime;
        }
    }

    public void UpdateDir(eMoveDir aDir)
    {
        Vector2 dir = new Vector2();
        switch(aDir)
        {
            case eMoveDir.Down:
            {
                m_Renderer.flipY = false;
                dir = Vector2.down;
                break;
            }
            case eMoveDir.Up:
            {
                m_Renderer.flipY = true;
                dir = Vector2.up;
                break;
            }
            case eMoveDir.Right:
            {
                m_Renderer.flipY = false;
                transform.Rotate(transform.forward * 90f);
                dir = Vector2.right;
                break;
            }
            case eMoveDir.Left:
            {
                m_Renderer.flipY = true;
                transform.Rotate(transform.forward * 90f);
                dir = Vector2.left;
                break;
            }
        }
        m_Rb.velocity = dir * m_Speed;
    }

    private void DisableArrow()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D aCol)
    {
        EnemyAI eRef = aCol.gameObject.GetComponent<EnemyAI>();
        if(eRef != null)
        {
            eRef.ReceiveDamage(m_Damage);
        }

        PhaseBoss refBoss = aCol.gameObject.GetComponent<PhaseBoss>();
        if(refBoss != null)
        {
            refBoss.BossReceiveDamage(m_Damage);
            Debug.Log("Boss ReceiveDamage Arrow : " + m_Damage);
        }
    }
}
