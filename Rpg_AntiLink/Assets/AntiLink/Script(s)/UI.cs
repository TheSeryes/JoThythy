using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    private Image m_HpEnemy;

    
    private void Start()
    {
        m_HpEnemy.fillAmount = 1f;
    }

    public void UpdateHp(float aHp)
    {
        m_HpEnemy.fillAmount = aHp;           
    }
}
