using Unity.Mathematics;
using UnityEngine;

public class ThrowableHolder : MonoBehaviour
{
    public PointerRotator contestantPointer;
    public GameObject currentThrowableObject;
    public MaskHolder thisMaskHolder;

    [Header("Audio")]
    public AudioSource contestantAudioSource; //The audio source of this character
    public AudioClip onPickUpSound;

    public void ObtainThrowable(GameObject newThrowable)
    {
        if(currentThrowableObject == null)
        {
            currentThrowableObject = newThrowable;
            ChangePointerSprite(currentThrowableObject.GetComponentInChildren<SpriteRenderer>().sprite);
            TryPlaySound();
        }
    }

    void TryPlaySound()
    {
        if(contestantAudioSource && onPickUpSound)
        {
            contestantAudioSource.PlayOneShot(onPickUpSound);
        }
    }

    public void UseThrowable()
    {
        if(currentThrowableObject != null)
        {
            //Attack logic will be executed in each object's Throwable class, probably on Start()
            //Remember to unattach parent in Throwable logic if necessary.
            GameObject usedThrowable = Instantiate(currentThrowableObject, transform.position, quaternion.identity, transform);
            usedThrowable.GetComponent<Throwable>().throwableOwner = this;
            contestantPointer.ResetPointerSprite();
            currentThrowableObject = null;
        }
    }

    void ChangePointerSprite(Sprite newSprite)
    {
        contestantPointer.ChangePointerSprite(newSprite);
    }

    public void ObtainPointsFromThrowable(int points)
    {
        if (thisMaskHolder.hasMask)
        {
            thisMaskHolder.UpdatePoints(points);
        }
    }
}
