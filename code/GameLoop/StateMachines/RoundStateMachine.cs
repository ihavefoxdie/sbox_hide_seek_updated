using HideAndSeek.GameLoop.Rules;
using HideAndSeek.GameLoop.States;
using System;
using System.Collections.Generic;
using System.Text;

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
			_currentState = state;
		}

		protected void SomethingSomething()
		{
			if ( _currentState != null )
			{
				_currentState.Update();
			}
		}
	}
}
