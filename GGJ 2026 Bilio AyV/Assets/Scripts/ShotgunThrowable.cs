using UnityEngine;

public class ShotgunThrowable : Throwable
{
    public float thrownPower;
    public AudioClip throwSound;
    private Rigidbody2D rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Attack();
    }

    public override void Attack() //This shotgun is YEETED
    {
        rb.AddForce(throwableOwner.transform.up*thrownPower, ForceMode2D.Impulse);
        throwableOwner.contestantAudioSource?.PlayOneShot(throwSound);
        transform.parent = null;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 3) //3 is the contestant layer
        {
            collision.gameObject.GetComponentInChildren<MaskHolder>().LoseMask();
            collision.gameObject.GetComponentInChildren<IStun>().Stun();
            GivePoints();
        }
        
        DestroyItself();
    }

    public override void DestroyItself()
    {
        Destroy(gameObject);
    }
}
