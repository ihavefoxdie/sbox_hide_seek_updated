using Sandbox;
using System;
using System.Threading.Tasks;

namespace HideAndSeek.GameLoop.Rules;

/// <summary>
/// A little system for managing user game settings.
/// </summary>
public class UserGameSettings
{
	/// <summary>
	/// Name of the map to start a session with.
	/// </summary>
	public string MapName { get; set; }
	/// <summary>
	/// Length of a round in seconds.
	/// </summary>
	public int RoundLength { get; set; }
	/// <summary>
	/// Time before seekers get enabled in seconds.
	/// </summary>
	public int PrepTime { get; set; }
	/// <summary>
	/// Time before the next round starts (after the previous has ended).
	/// </summary>
	public int TimeBeforeNextRound {  get; set; }
	/// <summary>
	/// Number of rounds in a match.
	/// </summary>
	public int Rounds {  get; set; }
	/// <summary>
	/// Seekers do not get seekers clothing and vice versa; everyone wears their default outfits.
	/// </summary>
	public bool ParanoiaMode { get; set; }

	public UserGameSettings()
	{
		Init();
	}

	/// <summary>
	/// Saves current settings (and write them to the file).
	/// </summary>
	public void SaveSettings()
	{
		WriteSettings( "UserSettings" );
	}

	public void LoadSettings()
	{
		ReadSettings( "UserSettings" );
	}

	/// <summary>
	/// Sets and creats default settings, then writes everything to a file.
	/// </summary>
	private void Init()
	{
		MapName = "facepunch.flatgrass";
		RoundLength = 300;
		PrepTime = 30;
		TimeBeforeNextRound = 10;
		Rounds = 5;
		ParanoiaMode = false;
		WriteSettings( "defaultUserSettings" );
	}

	/// <summary>
	/// Writes current settings to the specified .json file.
	/// </summary>
	/// <param name="settingsFileName">Name of the file to write the data to (no need to specify the extension).</param>
	private void WriteSettings( string settingsFileName )
	{
		FileSystem.Data.CreateDirectory( "Settings" );
		FileSystem.Data.WriteJson( "Settings/" + settingsFileName + ".json", this );
	}

	private void ReadSettings( string settingsFileName )
	{
		UserGameSettings settings;
		if ( FileSystem.Data.FileExists( $"Settings/{settingsFileName}.json" ) )
		{
			settings = FileSystem.Data.ReadJson<UserGameSettings>( $"Settings/{settingsFileName}.json" );

			MapName = settings.MapName;
			RoundLength = settings.RoundLength;
			PrepTime = settings.PrepTime;
			TimeBeforeNextRound = settings.TimeBeforeNextRound;
			Rounds = settings.Rounds;
			ParanoiaMode = settings.ParanoiaMode;
		}
	}
}
