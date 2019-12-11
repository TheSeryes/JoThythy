using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlieHuman : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 2f;
    [SerializeField]
    private float m_DistanceWithTarget = 1f;
    [SerializeField]
    private Transform m_Target;
    [SerializeField]
    private GameObject m_Arrows;

    private float m_CurrentTime = 4f;


    private void Start()
    {
        m_Target = GameManager.Instance.GetPlayerPosition();
    }


    private void Update()
    {
        DropArrow();
        transform.position = Vector2.MoveTowards(transform.position,m_Target.transform.position + new Vector3(0f,0f,m_DistanceWithTarget), m_Speed * Time.deltaTime);
    }



    private void DropArrow()
    {
        if(GameManager.Instance.Player.ArrowAmount <= 0)
        {
            m_CurrentTime -= Time.deltaTime;

            if(m_CurrentTime <= 0)
            {
                Instantiate(m_Arrows,transform.position,Quaternion.identity);
                m_CurrentTime = 4f;
            }
        }
    }
}
