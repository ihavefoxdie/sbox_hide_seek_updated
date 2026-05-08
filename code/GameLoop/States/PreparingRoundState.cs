using HideAndSeek.GameLoop.StateMachines;

namespace HideAndSeek.GameLoop.States
{
	public class PreparingRoundState( RoundStateMachine roundStateMachine ) : RoundState( roundStateMachine )
	{
		public override void Enter()
		{
			foreach ( var team in RoundStateMachine.GameLoop.Teams )
			{
				team.FlushPlayers();
			}

			RoundStateMachine.GameLoop.AssignPlayers();
			RoundStateMachine.GameLoop.Round.EndRound();
			RoundStateMachine.GameLoop.Round.StartRound();
		}

		public override void Exit()
		{
		}

		public override void Update()
		{
			if ( RoundStateMachine.GameLoop.Round.ShouldSeekersBeEnabled )
			{
				RoundStateMachine.ChangeState( this );
			}
		}
	}
}
