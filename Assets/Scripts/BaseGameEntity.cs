using UnityEngine;
using System.Collections;

public abstract class BaseGameEntity 
{
    private int m_ID;
    public BaseGameEntity(int ID)
    {
        SetID(ID);
    }
	public virtual int GetID()
    {
        return m_ID;
    }
    public virtual void Update(float dt)
    {
        ;
    }

    void SetID(int ID)
    {
        m_ID = ID;
    }
}
