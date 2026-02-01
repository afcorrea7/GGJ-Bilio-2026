using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class StunApplier : MonoBehaviour, IStun
{
    public float stunnedTime;
    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void Stun()
    {
        if (playerController.canMove) //if not stunned already
        {
            StartCoroutine(StunTime());
        }
    }

    IEnumerator StunTime()
    {
        playerController.canMove = false;
        gameObject.layer = LayerMask.NameToLayer("StunnedContestant"); //Will not collide with mask
        yield return new WaitForSeconds(stunnedTime);
        playerController.canMove = true;
        gameObject.layer = LayerMask.NameToLayer("Contestant");
    }
    
}
