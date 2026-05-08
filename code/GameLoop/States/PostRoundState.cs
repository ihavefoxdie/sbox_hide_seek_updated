using HideAndSeek.GameLoop.StateMachines;

namespace HideAndSeek.GameLoop.States
{
	public class PostRoundState( RoundStateMachine roundStateMachine ) : RoundState( roundStateMachine )
	{
		public override	void Enter()
		{
		}

		public override	void Exit()
		{
		}

		public override	void Update()
		{
			if ( RoundStateMachine.GameLoop.Round.ShouldTheGameEnd )
				return;

			if ( RoundStateMachine.GameLoop.Round.HasPostRoundEnded )
			{
				RoundStateMachine.ChangeState( this );
			}
		}
	}
}
