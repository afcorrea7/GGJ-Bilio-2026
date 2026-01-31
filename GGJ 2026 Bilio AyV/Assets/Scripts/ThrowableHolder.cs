using Unity.Mathematics;
using UnityEngine;

public class ThrowableHolder : MonoBehaviour
{
    public GameObject currentThrowableObject;

    public void ObtainThrowable(GameObject newThrowable)
    {
        if(currentThrowableObject == null)
        {
            currentThrowableObject = newThrowable;
        }
    }

    public void UseThrowable()
    {
        if(currentThrowableObject != null)
        {
            //Attack logic will be executed in each object's Throwable class, probably on Start()
            //Remember to unattach parent in Throwable logic if necessary.
            GameObject usedThrowable = Instantiate(currentThrowableObject, transform.position, quaternion.identity, transform);
            usedThrowable.GetComponent<Throwable>().throwableOwner = this;
            currentThrowableObject = null;
        }
    }

    public void ObtainPointsFromThrowable(float points)
    {
        // if (gotMask)
        // {
            
        // }
    }
}
