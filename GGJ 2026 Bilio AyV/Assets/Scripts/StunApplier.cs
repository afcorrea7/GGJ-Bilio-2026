using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class StunApplier : MonoBehaviour, IStun
{
    public float stunnedTime;
    private PlayerController playerController;
    private Animator anim;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
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
        anim.SetBool("stunned", false);
        gameObject.layer = LayerMask.NameToLayer("StunnedContestant"); //Will not collide with mask
        yield return new WaitForSeconds(stunnedTime);
        playerController.canMove = true;
        anim.SetBool("stunned", true);
        gameObject.layer = LayerMask.NameToLayer("Contestant");
    }
    
}
