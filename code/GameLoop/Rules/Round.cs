using System;

namespace HideAndSeek.GameLoop.Rules;

public class Round
{
	#region Properties
	/// <summary>
	/// Time since the start of the round.
	/// </summary>
	public TimeSince TimeSinceStart { get; private set; }

	/// <summary>
	/// Gets the number of rounds completed in the current session.
	/// </summary>
	public int CurrentRoundCount { get; private set; } = 0;

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
	public bool ShouldRoundEnd
	{
		get
		{
			if ( TimeSinceStart.Relative >= RoundLength + PrepTime && IsStarted )
			{
				return true;
			}
			return false;
		}
	}

	public bool ShouldSeekersBeEnabled
	{
		get
		{
			if ( TimeSinceStart.Relative >= PrepTime && IsStarted )
			{
				return true;
			}
			return false;
		}
	}

	public bool ShouldTheGameEnd
	{
		get
		{
			return CurrentRoundCount >= RoundLimit;
		}
	}

	public bool HasPostRoundEnded
	{
		get
		{
			return TimeSinceStart.Relative >= RoundLength + PrepTime + TimeBeforeNextRound;
		}
	}

	/// <summary>
	/// Time before seekers get enabled in seconds.
	/// </summary>
	/// <remarks>Cannot be less than 5 seconds.</remarks>
	public int PrepTime
	{
		get;
		private set
		{
			if ( value >= 5 )
			{
				field = value;
			}
		}
	}
	/// <summary>
	/// Time before the next round starts (after the previous has ended).
	/// </summary>
	/// <remarks>Cannot be less than 5 seconds.</remarks>
	public int TimeBeforeNextRound
	{
		get;
		private set
		{
			if ( value >= 5 )
			{
				field = value;
			}
		}
	}
	#endregion



	#region Variables
	private const int DefaultRoundLength = 300;
	private const int DefaultRoundLimit = 5;
	private const int DefaultTimeBeforeNextRound = 5;
	private const int DefaultPrepTime = 5;
	#endregion



	public Round()
	{
		RoundLength = DefaultRoundLength;
		RoundLimit = DefaultRoundLimit;
		PrepTime = DefaultPrepTime;
		TimeBeforeNextRound = DefaultTimeBeforeNextRound;
	}

	public Round( int roundLength, int roundsLimit, int timeBeforeNextRound, int prepTime)
	{
		RoundLength = roundLength;
		RoundLimit = roundsLimit;
		TimeBeforeNextRound = timeBeforeNextRound;
		PrepTime = prepTime;
	}

	#region Methods
	/// <summary>
	/// Starts a round when not already started and when CanRoundStart is true.
	/// </summary>
	/// <remarks>On success, sets IsStarted to true, resets TimeSinceStart to 0, and increments CurrentRoundCount.
	/// No state changes occur if the round is not started.</remarks>
	/// <returns><c>true</c> if the round was started; otherwise <c>false</c> (for example, if already started or CanRoundStart is <c>false</c>).</returns>
	public bool StartRound()
	{
		if ( !IsStarted && !ShouldTheGameEnd )
		{
			IsStarted = true;
			TimeSinceStart = 0;
			CurrentRoundCount++;
			return true;
		}

		return false;
	}

	/// <summary>
	/// Ends the current round.
	/// </summary>
	/// <remarks>Sets IsStarted to false when a round is ended.</remarks>
	/// <returns><c>true</c> if a round was ended; otherwise, <c>false</c>.</returns>
	public bool EndRound()
	{
		if ( IsStarted )
		{
			IsStarted = false;
			return true;
		}
		return false;
	}
	#endregion
}
