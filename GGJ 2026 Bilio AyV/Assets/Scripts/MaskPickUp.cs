using UnityEngine;

public class MaskPickUp : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3) //3 is the Contestant Layer
        {
            collision.gameObject.GetComponentInChildren<MaskHolder>().ObtainMask();
            gameObject.SetActive(false);
        }
    }
}
