using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class EnemyStunnedState : MonoBehaviour, IState
{
    public float stunnedTime;
    private Animator thisAnim;
    private AudioSource thisAS;

    private EnemyStateManager stateManager;
    public void EnterState(BaseStateManager incomingStateManager)
    {
        if (stateManager == null)
        {
            stateManager = incomingStateManager as EnemyStateManager;
        }


    }

    IEnumerator StunTime()
    {
        yield return new WaitForSeconds(stunnedTime);
        ExitState();
    }

    public void ExitState()
    {
        //stateManager.SwitchState();
    }
}
