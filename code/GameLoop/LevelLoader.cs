using Sandbox.Utils.Interfaces;
using System;
using System.Threading.Tasks;

namespace HideAndSeek.GameLoop;

public static class LevelLoader
{
	/// <summary>
	/// Changes the active scene to the specified one.
	/// </summary>
	/// <param name="scenePath">The path of the scene to load.</param>
	/// <param name="isAdditive">Loads as additive.</param>
	/// <c>true</c> if the scene load was initiated successfully; otherwise, <c>false</c>.
	public static bool ChangeScene(string scenePath, bool isAdditive)
	{
		try
		{
			SceneLoadOptions options = new();
			options.SetScene( scenePath );
			options.IsAdditive = isAdditive;
			Game.ChangeScene( options );

			return true;
		}
		catch ( Exception ex )
		{
			Log.Error( ex, $"Failed to load the {scenePath} scene." );
			return false;
		}
	}

	/// <summary>
	/// Asynchronously changes the active scene to the specified one.
	/// Before loading, it notifies all components implementing <see cref="IBeforeSceneChangeListener"/>
	/// and awaits their completion.
	/// </summary>
	/// <param name="scenePath">The path of the scene to load.</param>
	/// <param name="isAdditive">Loads as additive.</param>
	/// <param name="scene">The current scene used to resolve listeners.</param>
	/// <returns>
	/// <c>true</c> if the scene load was initiated successfully; otherwise, <c>false</c>.
	/// </returns>
	public static async Task<bool> TransitionToSceneAsync( string scenePath, bool isAdditive, Scene scene )
	{
		try
		{
			if ( string.IsNullOrEmpty( scenePath ) )
			{
				return false;
			}
			SceneLoadOptions options = new();
			options.SetScene( scenePath );
			options.IsAdditive = isAdditive;
			options.ShowLoadingScreen = false;

			await InvokeBeforeSceneChangeAsync(scene);

			Game.ChangeScene( options );
			return true;
		}
		catch ( Exception ex )
		{
			Log.Error( ex, $"Failed to load the {scenePath} scene." );
			return false;
		}
	}

	private static async Task InvokeBeforeSceneChangeAsync(Scene scene)
	{
		List<Task> tasks = [];
		scene?.RunEvent( delegate ( IBeforeSceneChangeListener x )
		{
			tasks.Add( x.BeforeChange() );
		} );
		foreach ( var task in tasks )
		{
			await task;
		}
	}
}
