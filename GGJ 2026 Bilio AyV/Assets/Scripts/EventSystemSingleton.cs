using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(EventSystem))]
public class EventSystemSingleton : SingletonHandler // this monobehaviour script makes sure this object stays a singleton
{
    public static EventSystemSingleton instance;
    [HideInInspector] public InputSystemUIInputModule UIModule;

    void Awake()
    {
        // If there's already an instance and it's not this one, destroy this one
        if (instance != null && instance != this){
            Destroy(gameObject);
            return;
        }
        instance = this;
        EventSystem.current = GetComponent<EventSystem>();
        UIModule = GetComponent<InputSystemUIInputModule>();
        ToggleUIModule(false);
    }

    public void ToggleUIModule(bool toggle){
        UIModule.enabled = toggle;
    }
}
