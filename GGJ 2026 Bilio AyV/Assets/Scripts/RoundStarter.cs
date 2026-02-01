using UnityEngine;

public class RoundStarter : MonoBehaviour
{
    //call on Animation Event
    public void StartRound()
    {
        GameManager.GMInstance.TriggerOnMatchStart();
        transform.parent.gameObject.SetActive(false); //Holy cancer
    }
}
