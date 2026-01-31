using System.Diagnostics;
using UnityEngine;

public class EnemyStateManager : BaseStateManager, IStun
{
    public EnemyStunnedState enemyStunnedState;
    public EnemyChaseState enemyChaseState;
    private IState currentState;
    void Start()
    {
        currentState = enemyChaseState;
    }

    void Update()
    {
        currentState.UpdateState();
    }

    public override void SwitchState(IState newState)
    {
        if(currentState == newState) return;
        
        currentState = newState;
        newState.EnterState(this);
    }

    public void Stun()
    {
        SwitchState(enemyStunnedState);
    }
}
