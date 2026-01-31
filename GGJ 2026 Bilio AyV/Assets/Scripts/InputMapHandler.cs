using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputMaphandler : MonoBehaviour
{
    [HideInInspector] public PlayerInput playerInput;
    public string playerActionsMapName = "Player_Actions";
    public string UIMapName = "UI";

    public static InputMaphandler instance;

    //Singleton
    void Awake(){
        if (instance != null && instance != this){
            Destroy(gameObject);
        }
        else{
            instance = this;
            playerInput = GetComponent<PlayerInput>();
        }
    }

    void OnEnable(){
        Debug.Log("PlayerController entered OnEnable() function");
        SubscribeToEvents();
        EnableInputMap();
    }

    void OnDisable(){
        UnsubscribeFromEvents();
        DisableInputMap();
    }

    // void Update() //For debugging
    // {
    //     Debug.Log("Current Action Map: " + playerInput.currentActionMap.name);
    //     Debug.Log("Current Control Scheme: "+playerInput.currentControlScheme);
    // }

    public void SetMapToPlayerActions()
    {
        StartCoroutine(ReenableInputNextFrame());
        EventSystemSingleton.instance?.ToggleUIModule(false);
        playerInput.SwitchCurrentActionMap(playerActionsMapName);
    }

    public void SetMapToUI()
    {
        StartCoroutine(ReenableInputNextFrame());
        EventSystemSingleton.instance.ToggleUIModule(true);
        playerInput.SwitchCurrentActionMap(UIMapName);
    }

    public void EnableInputMap(){
        playerInput.currentActionMap.Enable();
    }

    public void DisableInputMap(){
        playerInput.currentActionMap?.Disable();
    }

    //Prevent double input when switching maps and actions are binded to the same key
    IEnumerator ReenableInputNextFrame()
    {
        DisableInputMap();
        foreach (InputAction action in playerInput.currentActionMap) //Reset leftover input values
        {
            action.Reset();
        } 
        yield return null;
        EnableInputMap();
    }

    void SubscribeToEvents()
    {
        GameManager.OnMatchStart += SetMapToPlayerActions;
        GameManager.OnPause += SetMapToUI;
    }

    void UnsubscribeFromEvents()
    {
        GameManager.OnMatchStart -= SetMapToPlayerActions;
        GameManager.OnPause -= SetMapToUI;
    }
}
