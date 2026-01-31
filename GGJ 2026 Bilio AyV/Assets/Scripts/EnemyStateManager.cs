using System.Diagnostics;
using UnityEngine;

public class EnemyStateManager : BaseStateManager, IStun
{
    public EnemyStunnedState enemyStunnedState;
    private IState currentState;
    void Start()
    {
        //currentState = chasingState;
    }

    void Update()
    {
        
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
