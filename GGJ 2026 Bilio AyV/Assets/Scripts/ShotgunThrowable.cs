using UnityEngine;

public class ShotgunThrowable : Throwable
{
    public float thrownPower;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Attack();
    }

    public override void Attack() //This shotgun is YEETED
    {
        rb.AddForce(throwableOwner.transform.up*thrownPower, ForceMode2D.Impulse);
        transform.parent = null;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.GetMask("Contestant"))
        {
            //collision.HurtContestant //Take off mask
            GivePoints();
        }
        DestroyItself();
    }

    public override void DestroyItself()
    {
        Destroy(gameObject);
    }
}
