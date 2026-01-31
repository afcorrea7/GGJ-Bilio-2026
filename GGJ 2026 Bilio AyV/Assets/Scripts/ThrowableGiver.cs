using UnityEngine;

public class ThrowableGiver : MonoBehaviour
{
    [SerializeField] GameObject throwablePrefab;
    [SerializeField] ParticleSystem pickUpParticles;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3) //Layer 3 is contestant
        {
            ThrowableHolder collisionThrowableHolder = collision.gameObject.GetComponentInChildren<ThrowableHolder>();
            if(collisionThrowableHolder != null)
            {
                collisionThrowableHolder.ObtainThrowable(throwablePrefab);
                //Instantiate(pickUpParticles, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
            }
        }
    }
}
