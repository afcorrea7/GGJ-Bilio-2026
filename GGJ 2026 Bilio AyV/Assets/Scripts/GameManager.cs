using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Manage events that can happen WHILE playing a level
    //Don't store player data such as highscore on this script, use a separate script instead
    public static  GameManager GMInstance;
    //Game Manager might stick around between levels, make sure to refresh these references
    [HideInInspector] public GameObject playerReference;
    
    //On pause whenever the match is not currently on play, like the starting countdown
    public static event Action OnMatchStart;
    public static event Action OnPause;
    public static event Action onMatchEnd;

    void Awake()
    {
        SetReferences();    
    }
    
    void Start(){ //Uncomment if match will start on very first frame
        Debug.Log("Executing Start on Game Manager");
        TriggerOnMatchStart();   
    }

    public void TriggerOnMatchStart(){
        OnMatchStart?.Invoke();
    }

    public void TriggerOnPause(){
        OnPause?.Invoke();
    }

    public void TriggerOnMatchEnd()
    {
        onMatchEnd?.Invoke();
    }

    void SetReferences(){
        Debug.Log("Getting References for Game Manager");
        playerReference = GameObject.FindWithTag("Player");
    }

    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
