using UnityEngine;

public class MiniDorito : MonoBehaviour
{
    private DoritoThrowable parentDorito;
    private Rigidbody2D rb;

    void Awake()
    {
        parentDorito = GetComponentInParent<DoritoThrowable>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(float thrownPower, float rotationAngles)
    {
        transform.rotation = transform.rotation * Quaternion.Euler(0, 0, rotationAngles); //So that we can just shoot in the direction of vector3.up
        rb.AddForce(transform.up*thrownPower, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        parentDorito.HandleDoritoCollision(collision);
        gameObject.SetActive(false);
    }
}
