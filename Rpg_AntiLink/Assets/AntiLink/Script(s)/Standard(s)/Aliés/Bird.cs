﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 1f;
    [SerializeField]
    private float m_DistanceWithTarget = 1f;
    [SerializeField]
    private Transform m_Target;
    [SerializeField]
    private Transform m_TargetLoot;

    private bool m_GoToLoot = false;
    public bool GetGotoLoot
    {
        get { return m_GoToLoot; }
        set { m_GoToLoot = value; }
    }


    private void Start()
    {
        m_Target = GameManager.Instance.GetPlayerPosition();
    }
         

    private void Update()
    {
        if(m_GoToLoot)
        {
            transform.position = Vector2.MoveTowards(transform.position,m_TargetLoot.transform.position, m_Speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position,m_Target.transform.position + new Vector3(0f,m_DistanceWithTarget,0f), m_Speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D aCol)
    {
        Collectible refCollec = aCol.gameObject.GetComponent<Collectible>();

        if(refCollec != null)
        {
            m_GoToLoot = true;
        }
    }
}
