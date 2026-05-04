using System;

namespace HideAndSeek.GameLoop.Rules;

public struct Round
{
	#region Properties
	/// <summary>
	/// Time since the start of the round.
	/// </summary>
	public TimeSince TimeSinceStart { get; private set; }
	/// <summary>
	/// Round length, in seconds.
	/// </summary>
	/// <remarks>Cannot be less than 10 seconds.</remarks>
	public int RoundLength
	{
		get;
		private set
		{
			if ( value >= 10 )
			{
				field = value;
			}
		}
	}
	/// <summary>
	/// Gets the maximum number of rounds allowed.
	/// </summary>
	/// <remarks>Cannot be less than 1.</remarks>
	public int RoundLimit
	{
		get;
		private set
		{
			if ( value > 0 )
			{
				field = value;
			}
		}
	}
	/// <summary>
	/// Is round in progress?
	/// </summary>
	public bool IsStarted { get; private set; } = false;
	/// <summary>
	/// Should the round end?
	/// </summary>
	public readonly bool ShouldRoundEnd
	{
		get
		{
			if ( TimeSinceStart.Relative >= RoundLength )
			{
				return true;
			}
			return false;
		}
	}
	#endregion



	#region Events
	/// <summary>
	/// Gets invoked when the end event occurs.
	/// </summary>
	public event Action OnEnd;
	/// <summary>
	/// Gets invoked when the start event occurs.
	/// </summary>
	/// <remarks>Assign a delegate to perform custom initialization or startup logic. If not set, no action is
	/// performed when the start event occurs.</remarks>
	public event Action OnStart;
	#endregion



	#region Variables
	#endregion



	public Round()
	{
		RoundLength = 300;
		RoundLimit = 5;
	}

	public Round( int roundLength, int roundsLimit )
	{
		RoundLength = roundLength;
		RoundLimit = roundsLimit;
	}



	#region Methods
	/// <summary>
	/// Start the round.
	/// </summary>
	public void StartTheRound()
	{
		if ( IsStarted ) return;

		TimeSinceStart = 0;
		OnStart?.Invoke();
		IsStarted = true;
	}

	/// <summary>
	/// End the round.
	/// </summary>
	public void EndTheRound()
	{
		OnEnd?.Invoke();
		IsStarted = false;
	}
	#endregion
}
