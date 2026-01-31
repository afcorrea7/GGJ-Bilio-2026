using UnityEngine;

public class EnemyChaseState : MonoBehaviour, IState
{
    private EnemyStateManager stateManager;
    public void EnterState(BaseStateManager incomingStateManager)
    {
        if (stateManager == null)
        {
            stateManager = incomingStateManager as EnemyStateManager;
        }    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
