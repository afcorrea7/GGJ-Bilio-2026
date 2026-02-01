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
        StartCoroutine(StunTime());
    }

    IEnumerator StunTime()
    {
        gameObject.layer = LayerMask.NameToLayer("StunnedContestant"); //Will not collide with mask
        yield return new WaitForSeconds(stunnedTime);
        gameObject.layer = LayerMask.NameToLayer("Contestant"); //Will not collide with mask
        ExitState();
    }

    public void ExitState()
    {
        stateManager.SwitchState(stateManager.enemyChaseState);
    }
}
