using UnityEngine;

public class PointerRotator : MonoBehaviour
{
    [HideInInspector] public Vector2 pointerposition;

    void Update()
    {
        transform.up = (pointerposition - (Vector2)transform.position).normalized;
    }
}
