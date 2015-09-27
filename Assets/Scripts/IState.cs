using UnityEngine;
using System.Collections;

public interface IState<T>
{
    void Enter(T entity_type);

    void Extuce(T entity_type,float dt);

    void Exit(T entity_type);
}
