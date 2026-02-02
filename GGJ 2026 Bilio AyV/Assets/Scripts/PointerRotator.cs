using UnityEngine;

public class PointerRotator : MonoBehaviour
{
    [HideInInspector] public Vector2 pointerposition;
    private SpriteRenderer thisSpriteRenderer;
    private Sprite originalPointerSprite;

    void Awake()
    {
        thisSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalPointerSprite = thisSpriteRenderer.sprite;
    }

    public void ChangePointerSprite(Sprite newSprite)
    {
        thisSpriteRenderer.sprite = newSprite;
    }

    public void ResetPointerSprite()
    {
        thisSpriteRenderer.sprite = originalPointerSprite;
    }

    void Update()
    {
        transform.up = (pointerposition - (Vector2)transform.position).normalized;
    }
}
