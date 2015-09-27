using UnityEngine;
using System.Collections;

public class GamePlayer : BaseGameEntity 
{

    private StateMachine<GamePlayer> m_pStateMachine;
    public StateMachine<GamePlayer> mStateMachine
    {
        get
        {
            return m_pStateMachine;
        }
    }
	// Use this for initialization
    public GamePlayer(int playerID,GameObject go):base(playerID)
    {

        m_pStateMachine = new StateMachine<GamePlayer>(this);


    }
    public override void Update(float dt)
    {
        m_pStateMachine.Update(dt);
    }
}
