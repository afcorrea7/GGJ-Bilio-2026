using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class EnemyStunnedState : MonoBehaviour, IState
{
    public float stunnedTime;
    private Animator thisAnim;

    private EnemyStateManager stateManager;

    void Start()
    {
        thisAnim = GetComponent<Animator>();
    }
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
        thisAnim.SetBool("stunned", true);
        gameObject.layer = LayerMask.NameToLayer("StunnedContestant"); //Will not collide with mask
        yield return new WaitForSeconds(stunnedTime);
        thisAnim.SetBool("stunned", false);
        gameObject.layer = LayerMask.NameToLayer("Contestant"); //Will not collide with mask
        ExitState();
    }

    public void ExitState()
    {
        stateManager.SwitchState(stateManager.enemyChaseState);
    }
}
