public class EnemyStateMachine
{
    public EnemyState currentState { get; private set; }

    public void Initialize(EnemyState _StareState)
    {
        currentState = _StareState;
        currentState.Enter();
    }

    public void ChangeMachine(EnemyState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
