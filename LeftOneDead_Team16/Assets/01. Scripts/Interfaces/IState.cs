using System.Diagnostics;

public interface IState
{
    public void Enter();
    public void Exit();
    public void HandleInput();
    public void Uqdate();
    public void PhysicsUpdate();
}
public abstract class StateMachine
{
    protected IState currentState;

    public void ChageState(IState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    public void HandleInput()
    {
        currentState?.HandleInput();
    }

    public void Uqdate()
    {
        currentState?.Uqdate();
    }

    public void PhysicsUpdate()
    {
        currentState?.PhysicsUpdate();
    }
}