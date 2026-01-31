using UnityEngine;

public abstract class Throwable : MonoBehaviour
{
    public ThrowableHolder throwableOwner;
    public int pointAmount = 1;
    public abstract void Attack();
    public void GivePoints()
    {
        throwableOwner.ObtainPointsFromThrowable(pointAmount);
    }
    public abstract void DestroyItself(); //Don't forget to return the accumulated points to the throwable Holder
}
