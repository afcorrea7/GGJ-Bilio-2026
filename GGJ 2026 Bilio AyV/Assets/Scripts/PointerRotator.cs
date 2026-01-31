using UnityEngine;

public class PointerRotator : MonoBehaviour
{
    [HideInInspector] public Vector2 pointerposition;
    [HideInInspector] public SpriteRenderer pointerRenderer;


    void Start()
    {
        pointerRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        transform.up = (pointerposition - (Vector2)transform.position).normalized;
    }
}
