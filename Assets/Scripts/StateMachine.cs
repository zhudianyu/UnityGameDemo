using UnityEngine;
using System.Collections;

public class StateMachine<T>
{
    T m_pOwner;
    IState<T> m_pCurrentState
    {
        set;
        get;
    }
    IState<T> m_pPreviosState
    {
        set;
        get;
    }
    IState<T> m_pNextState
    {
        set;
        get;
    }

    IState<T> m_pGlobalState
    {
        set;
        get;
    }

    public StateMachine(T owner)
    {
        m_pOwner = owner;
        m_pCurrentState = m_pGlobalState = m_pNextState = m_pPreviosState = null;

    }
   public void Update(float dt)
    {
        if(m_pGlobalState != null)
        {
            m_pGlobalState.Extuce(m_pOwner,dt);
        }
        if(m_pCurrentState != null)
        {
            m_pCurrentState.Extuce(m_pOwner,dt);
        }
    }
    void ChangeState(IState<T> newState)
    {
        if(newState == null)
        {
            Debug.Log(" new state is null");
            return;
        }
        m_pPreviosState = m_pCurrentState;
        m_pCurrentState.Exit(m_pOwner);
        m_pCurrentState = newState;
        m_pCurrentState.Enter(m_pOwner);
    }

    void RevertToPreviosState()
    {
        ChangeState(m_pPreviosState);
    }
    bool IsInState(IState<T> state)
    {
        return m_pCurrentState.ToString().Equals(state.ToString());
    }

}
