using HideAndSeek.GameLoop.StateMachines;

namespace HideAndSeek.GameLoop.States;

public interface IRoundState
{
	public void Enter();
	public void Exit();
	public void Update();
}
