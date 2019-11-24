using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Behaviour
{
    Attack,

    Idle,

    natation,

    Count
}


public class Boss : MonoBehaviour
{
    private StateMachine m_State;

    private bool m_CanAttack = true;
    private bool m_CanSwim = true;


    private void Start()
    {
        m_State = new StateMachine(Behaviour.Count.ToInt()); // Creation d<une nouvelle StateMachine vide pour lui mettre a l <interieur notre enum de Behaviour
  
        m_State.AddState(Behaviour.Attack.ToInt(), null, AttackUpdate, null); // set le update du state, pas d entrer ni de sortie special , 
       
        m_State.AddState(Behaviour.Idle.ToInt(),IdleStateEnter,IdleUpdate,null); 

        m_State.AddState(Behaviour.natation.ToInt(),natationEnter,natationUpdate,nationExit); // set son state d<entrer , d<update et de fin
    
        m_State.ChangeState(Behaviour.Attack.ToInt());  // On Met le Monstre dans l' etat que nous voulons au debut du jeu
    }

    private void Update()
    {  
        m_State.Update();  // Call l update pour que nos state se fasse updater
    }


//-----------------------------------------------------------------------------------

    // Le code ( fonction ) qui est appler et utiliser pour toute les states , va se faire appeler dans l update de base
    // Si un state doit faire quelques chose de precis , la fonction va etre call dans se state
    // example : le joueur saute et ne peu plus tirer, mais il peut donner un coup de pied
    // le state : Air : va avoir une fonction Kick(); avoir de quoi qui block le tire et quand il va toucher le ground, tu appel le changement de state qui retournerait au state du ground


    // STATE ATTACK - 1 State
    private void AttackUpdate()
    {
        if(m_CanAttack)
        {
            Debug.Log("Attack");
            m_CanAttack = false;
        }
        else  // si tu n attack pu , change de state et retourn au state suivant ) natation
        {
            m_State.ChangeState(Behaviour.natation.ToInt());  // Si l'attack est nul , Change de state et va faire le state de la fonction du IdoleUpdate
        }
    }
//-----------------------------------------------------------------------------------
    // STATE IDLE - 2 States
    private void IdleStateEnter()
    {
        Debug.Log("Hause epaule");
    }

    private void IdleUpdate()
    {
        Debug.Log("Idole Chill debout");  
    }


//-----------------------------------------------------------------------------------
    // STATE NATATION - 3 States
    private void natationEnter()
    {
        Debug.Log("allonge les bras");
    }

    private void natationUpdate()
    {
        if(m_CanSwim)
        {
            Debug.Log("swim");  
            m_CanSwim = false;
        }
        else
        {
            m_State.ChangeState(Behaviour.Idle.ToInt()); // Permet de finir le state de nation pour passer a un autre state qui est le idle , Chaque fin de state doit changer pour aller a un autre
        }
    }

    private void nationExit()
    {
        Debug.Log("get out");  
    }   
}
