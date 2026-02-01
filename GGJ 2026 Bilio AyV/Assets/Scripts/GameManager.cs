using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Manage events that can happen WHILE playing a level
    public static GameManager GMInstance;
    
    //On pause whenever the match is not currently on play, like the starting countdown
    public static event Action OnMatchStart;
    public static event Action OnPause;
    public static event Action onMatchEnd;

    void Awake()
    {
        if(GMInstance == null)
        {
            GMInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    
    void Start(){ //Uncomment if match will start on very first frame
        //Debug.Log("Executing Start on Game Manager");
        //TriggerOnMatchStart();
        Time.timeScale = 0;   
    }

    public void TriggerOnMatchStart(){
        OnMatchStart?.Invoke();
        Time.timeScale = 1;   
    }

    public void TriggerOnPause(){
        OnPause?.Invoke();
    }

    public void TriggerOnMatchEnd()
    {
        onMatchEnd?.Invoke();
        Time.timeScale = 0;
    }

    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
