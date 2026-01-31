using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "GameEvent", menuName = "Utilities/GameEvent")]
public class GameEvent : ScriptableObject
{
    public event Action OnGameEvent;

    public void TriggerEvent()
    {
        OnGameEvent?.Invoke();
    }
}