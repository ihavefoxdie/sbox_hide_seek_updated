using HideAndSeek.GameLoop.StateMachines;

namespace HideAndSeek.GameLoop.States;

public class WaitingRoundState( RoundStateMachine roundStateMachine ) : RoundState( roundStateMachine )
{
	public override void Enter()
	{
	}
	public override	void Exit()
	{
	}
	public override void Update()
	{
		if( Connection.All.Count(x => x.IsActive) > 1)
		{
			RoundStateMachine.ChangeState( this );
		}
	}
}
