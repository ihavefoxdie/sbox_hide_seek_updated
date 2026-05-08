using HideAndSeek.GameLoop.StateMachines;

namespace HideAndSeek.GameLoop.States;

public interface IState
{
	public void Enter();
	public void Exit();
	public void Update();
}
