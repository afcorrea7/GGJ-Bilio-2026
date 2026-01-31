using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Build state managers with this class as a blueprint
public abstract class BaseStateManager : MonoBehaviour
{
    public abstract void SwitchState(IState newState);
}
