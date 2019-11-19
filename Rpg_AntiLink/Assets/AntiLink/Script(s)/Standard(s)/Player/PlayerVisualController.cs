using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eMoveDir {Up, Left, Right, Down};
public enum eBaseLook {Nohat, WithHat, WithHat_Sword, WithHat_Crossbow};

public class PlayerVisualController : MonoBehaviour
{

    [SerializeField]
    private Animator[] m_Animator;
    private int m_CurrentLookId = 0;

    private void Awake()
    {
        UpdateBaseLook(eBaseLook.Nohat);
    }

    public void UpdateDir(eMoveDir aDir)
    {
        switch(aDir)
        {
            case eMoveDir.Down:
            {
                Debug.Log("Down");
               
                break;
            }
            case eMoveDir.Right:
            {
                Debug.Log("Right");
                
                break;
            }
            case eMoveDir.Left:
            {
                Debug.Log("Left");
                
                break;
            }
            case eMoveDir.Up:
            {
                Debug.Log("Up");
                
                break;
            }
        }
    }

    public void UpdateBaseLook(eBaseLook aBaseLook)
    {
        DisableEverything();
        m_Animator[(int)aBaseLook].transform.gameObject.SetActive(true);
        m_CurrentLookId = (int)aBaseLook;
    }

    private void DisableEverything()
    {
        for(int i = 0; i < m_Animator.Length; i++)
        {
            m_Animator[i].transform.gameObject.SetActive(false);
        }
    }

    
}
