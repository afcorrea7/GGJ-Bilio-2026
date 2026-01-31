using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class StunApplier : MonoBehaviour, IStun
{
    public float stunnedTime;
    private PlayerController playerController;

    public void Stun()
    {
        StartCoroutine(StunTime());
    }

    IEnumerator StunTime()
    {
        playerController.canMove = false;
        yield return new WaitForSeconds(stunnedTime);
        playerController.canMove = true;
    }
    
}
