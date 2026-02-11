using UnityEngine;

public class ThrowableGiver : MonoBehaviour
{
    [SerializeField] GameObject throwablePrefab;
    [SerializeField] ParticleSystem pickUpParticles;
    [SerializeField] AudioClip soundOnPickUp; //Only the player will be sent this sound

    public bool alreadyTouched; //only allow one contestant to collect at any given time

    void OnEnable()
    {
        alreadyTouched = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (alreadyTouched)
        {
          gameObject.SetActive(false);  
          return;
        } 
        
        if(collision.gameObject.layer == 3) //Layer 3 is contestant
        {
            alreadyTouched = true;
            ThrowableHolder collisionThrowableHolder = collision.gameObject.GetComponentInChildren<ThrowableHolder>();
            if(collisionThrowableHolder != null && collisionThrowableHolder.currentThrowableObject == null)
            {
                collisionThrowableHolder.ObtainThrowable(throwablePrefab);
                //PlayPickUpSound(collision.gameObject);
                //Instantiate(pickUpParticles, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
            }
        }
    }

    void PlayPickUpSound(GameObject recievedContestant)
    {
        if(recievedContestant.name == "Player") //Dirty ass way to do it, just right for a jam
        {
            recievedContestant.GetComponentInChildren<AudioSource>().PlayOneShot(soundOnPickUp);
        }
    }
}
