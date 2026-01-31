//TO BE USED WITH ENTITIES THAT CAN HAVE A STATE MACHINE SUCH AS PLAYER OR ENEMIES
public interface IState
{
    public void EnterState(BaseStateManager stateManager); //Receieve context

    public void UpdateState(){} //Per-frame logic of the state, not all states will implement

    public void ExitState(){} //Logic before transitioning to another state, not all states will implement
}