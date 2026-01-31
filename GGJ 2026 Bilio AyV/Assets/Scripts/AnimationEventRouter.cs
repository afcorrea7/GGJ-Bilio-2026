using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach this to any object that requires animation events besides triggering a sound. Use SoundsInAnimationEvent for that
public class AnimationEventRouter : MonoBehaviour
{
    //Animation events can only call methods on scripts in the same object as the Animator Controller.
    //What if we want to call a method in a different object? Shit out of luck.

    //So we use this. BroadcastMessage() will look up the method by name (passed string), inside of
    //every script in the object (rootObject). It's VERY slow so don't abuse it.
    //Also make sure to supply the string with the EXACT name of the method you want to call.

    [SerializeField] GameObject rootObject;

    public void CallMethodInGivenObject(AnimationEvent animEventFields)
    {
        rootObject.BroadcastMessage(animEventFields.stringParameter, SendMessageOptions.RequireReceiver);
    }

    //For methods expecting an int parameter
    public void CallMethodInGivenObjectWithInt(AnimationEvent animEventFields)
    {
        rootObject.BroadcastMessage(animEventFields.stringParameter, animEventFields.intParameter, SendMessageOptions.RequireReceiver);
    }

    //For methods expecting a float parameter
    public void CallMethodInGivenObjectWithFloat(AnimationEvent animEventFields)
    {
        rootObject.BroadcastMessage(animEventFields.stringParameter, animEventFields.floatParameter, SendMessageOptions.RequireReceiver);
    }

    //For methods expecting a bool parameter, use an int with a 0 or 1 value
    public void CallMethodInGivenObjectWithBool(AnimationEvent animEventFields)
    {
        bool passedIntToBool = animEventFields.intParameter != 0; //If different from zero true
        rootObject.BroadcastMessage(animEventFields.stringParameter, passedIntToBool, SendMessageOptions.RequireReceiver);
    }
}
