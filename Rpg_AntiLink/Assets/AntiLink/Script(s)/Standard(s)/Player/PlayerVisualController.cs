using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eMoveDir { Up, Left, Right, Down };
public enum eBaseLook { Nohat, WithHat, WithHat_Sword, WithHat_Crossbow };

public class PlayerVisualController : MonoBehaviour
{

    [SerializeField]
    private Animator m_Animator;
    private eBaseLook m_CurrentLook;
    private eMoveDir m_CurrentDir;
    private bool m_IsWalking;

    private void Awake()
    {
        UpdateBaseLook(eBaseLook.Nohat);
    }

    public void UpdateDir(eMoveDir aDir)
    {
        if (!HaveSameDir(aDir))
        {
            SetAnimDir(aDir);
        }
    }

    private bool HaveSameDir(eMoveDir aDir)
    {
        if (aDir == m_CurrentDir)
        {
            return true;
        }
        return false;
    }

    private void SetAnimDir(eMoveDir aDir)
    {
        m_CurrentDir = aDir;
        ResetAnimator();
        switch (aDir)
        {
            case eMoveDir.Down:
                {
                    m_Animator.SetBool("Down", true);
                    break;
                }
            case eMoveDir.Right:
                {
                    m_Animator.SetBool("Right", true);
                    break;
                }
            case eMoveDir.Left:
                {
                    m_Animator.SetBool("Left", true);
                    break;
                }
            case eMoveDir.Up:
                {
                    m_Animator.SetBool("Up", true);
                    break;
                }
        }
        UpdateBaseLook(m_CurrentLook);
    }

    public void SetAnimType(bool aIsWalking)
    {
        if(m_IsWalking != aIsWalking)
        {
            m_IsWalking = aIsWalking;
            m_Animator.SetBool("Walk", m_IsWalking);
            SetAnimDir(m_CurrentDir);
        }
    }

    private void ResetAnimator()
    {
        m_Animator.SetBool("Left", false);
        m_Animator.SetBool("Right", false);
        m_Animator.SetBool("Up", false);
        m_Animator.SetBool("Down", false);
    }

    public void UpdateBaseLook(eBaseLook aBaseLook)
    {
        switch (aBaseLook)
        {
            case eBaseLook.Nohat:
                {
                    m_Animator.SetTrigger("SetBasic");
                    break;
                }
            case eBaseLook.WithHat:
                {
                    m_Animator.SetTrigger("SetHat");
                    break;
                }
            case eBaseLook.WithHat_Crossbow:
                {
                    m_Animator.SetTrigger("SetCrossbow");
                    break;
                }
            case eBaseLook.WithHat_Sword:
                {
                    m_Animator.SetTrigger("SetSword");
                    break;
                }
        }
        m_CurrentLook = aBaseLook;
    }

}
