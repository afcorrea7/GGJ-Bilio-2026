using UnityEngine;

public class MaskPickUp : MonoBehaviour
{
    bool alreadyTouched; //only allow one contestant to collect at any given time

    void OnEnable()
    {
        alreadyTouched = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(alreadyTouched) return;
        if(collision.gameObject.layer == 3) //3 is the Contestant Layer
        {
            alreadyTouched = true;
            collision.gameObject.GetComponentInChildren<MaskHolder>().ObtainMask();
            gameObject.SetActive(false);
        }
    }
}
