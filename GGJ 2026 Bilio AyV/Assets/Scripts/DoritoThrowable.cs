using UnityEngine;

public class DoritoThrowable : Throwable
{
    public float thrownPower;
    public int coneSpread; //How wide will the cone attack be, in degrees.
    public AudioClip throwSound;
    public MiniDorito[] miniDoritos;

    void Start()
    {
        Attack();
    }

    public override void Attack() //This shotgun is YEETED
    {
        throwableOwner.contestantAudioSource?.PlayOneShot(throwSound);
        transform.up = throwableOwner.transform.up;
        //left to right cone corners and middle
        int miniDoritoCount = 0;
        for (int fireangle = -coneSpread; fireangle <= coneSpread; fireangle += coneSpread) 
        {
            miniDoritos[miniDoritoCount].Launch(thrownPower, fireangle);
            miniDoritoCount++;
        }

        transform.parent = null;
    }

    void Update()
    {
        //Constantly check if all 3 dorito projectiles collided with something (i.e became disabled)
        foreach(MiniDorito mini in miniDoritos)
        {
            if (mini.gameObject.activeInHierarchy)
            {
                return;
            }
        }
        DestroyItself();
    }


    public void HandleDoritoCollision(Collider2D collision)
    {
        if(collision.gameObject.layer == 3) //3 is the contestant layer
        {
            collision.gameObject.GetComponentInChildren<MaskHolder>().LoseMask();
            collision.gameObject.GetComponentInChildren<IStun>().Stun();
            GivePoints();
        }
    }

    public override void DestroyItself()
    {
        Destroy(gameObject);
    }
}
