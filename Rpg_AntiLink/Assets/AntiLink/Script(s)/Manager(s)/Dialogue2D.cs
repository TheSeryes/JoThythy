using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Dialogue2D : MonoBehaviour
{
    public TextData m_Data;
    private int m_CurrentText = 0;

    [SerializeField]
    private GameObject m_Canvas;
    [SerializeField]
    private GameObject m_Player;
    [SerializeField]
    private TextMeshProUGUI m_VisualTextHim;
    [SerializeField]
    private TextMeshProUGUI m_ButtonTextA;
    [SerializeField]
    private TextMeshProUGUI m_ButtonTextB;
    [SerializeField]
    private float m_Time = 0.05f;
    private Coroutine m_Coroutine;

    private void Start()
    {
       m_Coroutine = StartCoroutine(TypeWriter());
    }
    
    public void OnTriggerEnter2D(Collider2D a_Col)
    {
        if(a_Col != null )
        {
            m_Canvas.SetActive(true);
            Debug.Log("Collider");
            m_Coroutine = StartCoroutine(TypeWriter());
        }
       
    }

    public void OnTriggerExit2D(Collider2D a_Col)
    { 
            m_Canvas.SetActive(false);
            Debug.Log("Bye Bye text");
            m_CurrentText = 0;
            if(m_Coroutine != null)
            {
                StopCoroutine(m_Coroutine);
            }
    }

    public void ContinueTalking(int a_Choice)
    {
        if(m_Coroutine == null)
        {
            m_VisualTextHim.text = String.Empty;
            m_ButtonTextA.text = String.Empty;
            m_CurrentText = m_Data.GetTalkingStructs[m_CurrentText].m_NextDialogue[a_Choice];
            m_Coroutine = StartCoroutine(TypeWriter());
        }
        
    }

    public void Attack(int a_Choice)
    {
        if(m_Coroutine == null)
        {
            m_VisualTextHim.text = String.Empty;
            m_ButtonTextB.text = String.Empty;
            m_CurrentText = m_Data.GetTalkingStructs[m_CurrentText].m_NextDialogue[a_Choice];
            m_Coroutine = StartCoroutine(TypeWriter());
            Debug.Log("Attackl");
        }
       
    }

    private IEnumerator TypeWriter()
    {
        m_VisualTextHim.text = String.Empty;
//        m_ButtonTextB.text = String.Empty;
        m_ButtonTextA.text = String.Empty;
        for (int i =0; i < m_Data.GetTalkingStructs[m_CurrentText].m_TextToShow.Length; i++)
        {
            yield return new WaitForSeconds(m_Time);
            m_VisualTextHim.text += m_Data.GetTalkingStructs[m_CurrentText].m_TextToShow[i];
        }

        for (int i =0; i < m_Data.GetTalkingStructs[m_CurrentText].m_ButtonA.Length; i++)
        {
            yield return new WaitForSeconds(m_Time);
            m_ButtonTextB.text += m_Data.GetTalkingStructs[m_CurrentText].m_ButtonA[i];
        }

        for (int i =0; i < m_Data.GetTalkingStructs[m_CurrentText].m_ButtonB.Length; i++)
        {
            yield return new WaitForSeconds(m_Time);
            m_ButtonTextA.text += m_Data.GetTalkingStructs[m_CurrentText].m_ButtonB[i];
        }
        m_Coroutine = null;
    }
  
}
