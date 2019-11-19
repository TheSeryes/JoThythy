using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void UpdateDir(Vector2 aDir)
    {
        m_Rb.velocity = aDir * m_Speed;
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
            Debug.Log("Dmage Arrwo" + m_Damage);
        }
    }
}
