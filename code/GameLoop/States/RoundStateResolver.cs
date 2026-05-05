using HideAndSeek.GameLoop.StateMachines;
using HideAndSeek.GameLoop.States;

namespace Sandbox.GameLoop.States;

public class RoundStateResolver
{
	public static IRoundState ResolveState( IRoundState state, RoundStateMachine stateMachine )
	{
		switch ( state )
		{
			case PreparingRoundState:
				return new ActiveRoundState( stateMachine );
			case ActiveRoundState:
				return new PostRoundState( stateMachine );
			case PostRoundState:
				return new PreparingRoundState( stateMachine );
			case WaitingRoundState:
				return new PreparingRoundState( stateMachine );
			default:
				return new PreparingRoundState( stateMachine );
		}
	}
}
