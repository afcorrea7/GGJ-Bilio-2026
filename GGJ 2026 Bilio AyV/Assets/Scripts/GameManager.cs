using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Manage events that can happen WHILE playing a level
    //Don't store player data such as highscore on this script, use a separate script instead
    public static  GameManager GMInstance;
    
    //On pause whenever the match is not currently on play, like the starting countdown
    public static event Action OnMatchStart;
    public static event Action OnPause;
    public static event Action onMatchEnd;

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

    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
