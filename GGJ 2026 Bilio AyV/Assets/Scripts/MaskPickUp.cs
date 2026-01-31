using UnityEngine;

public class MaskPickUp : MonoBehaviour
{

    void OnEnable()
    {
        //Some sort of invicibility frames, like disable collision with contestants
    }

    void ToggleCollisionWithContestants(bool toggle)
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3) //3 is the Contestant Layer
        {
            collision.gameObject.GetComponentInChildren<MaskHolder>().ObtainMask();
            gameObject.SetActive(false);
        }
    }
}
