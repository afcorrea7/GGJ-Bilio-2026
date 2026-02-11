using UnityEngine;

public class SwordThrowable : Throwable
{
    //You don't really throw the sword but whatever
    public float slashPower;
    public override void Attack()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3) //3 is the contestant layer
        {
            if(collision.GetComponentInChildren<ThrowableHolder>() != throwableOwner) //dont hit yourself idiot
            {
                collision.gameObject.GetComponentInChildren<MaskHolder>().LoseMask();
                collision.gameObject.GetComponentInChildren<IStun>().Stun();
                collision.GetComponent<Rigidbody2D>().AddForce(transform.up*slashPower, ForceMode2D.Impulse);
                GivePoints();
            }
        }
    }

    public override void DestroyItself() //called by animator
    {
        transform.parent = null;
        Destroy(gameObject);
    }
}
