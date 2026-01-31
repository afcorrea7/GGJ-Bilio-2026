using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SingletonHandler : MonoBehaviour
{
    protected void RemoveDuplicateSingletons<T>()where T:MonoBehaviour{
        // Find all objects of T (Generic) type in all loaded scenes
        T[] singletonsFound = FindObjectsByType<T>(FindObjectsSortMode.None);
        // Destroy all U that are not part of the GameObject that called this method
        //This means this object will prevail when loaded, essentially refreshing the values the previous singleton had
        foreach (T singleton in singletonsFound){
            if (singleton.gameObject != gameObject){
                Debug.Log(singleton.gameObject.name +" Destroyed");
                Destroy(singleton.gameObject);
            }
        }
    }
}
