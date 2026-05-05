using HideAndSeek.GameLoop.Rules;
using HideAndSeek.GameLoop.States;
using Sandbox.GameLoop.States;
using System;

namespace HideAndSeek.GameLoop.StateMachines
{
	public class RoundStateMachine : Component
	{
		public Round Round { get; private set; }
		private IRoundState _currentState;
		public event Action<IRoundState> OnStateChanged;

		[Rpc.Broadcast]
		public void ChangeState( IRoundState state )
		{
			_currentState = RoundStateResolver.ResolveState( state, this );
		}

		protected override void OnAwake()
		{
		}

		protected override void OnUpdate()
		{
			if ( _currentState != null )
			{
				_currentState.Update();
			}
		}
	}
}
