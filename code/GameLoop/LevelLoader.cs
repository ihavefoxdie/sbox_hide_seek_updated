using Sandbox.Utils.Interfaces;
using System.Threading.Tasks;

namespace HideAndSeek.GameLoop;

public class LevelLoader
{
	public static void ChangeScene(string scenePath, bool isAdditive)
	{
		SceneLoadOptions options = new();
		options.SetScene(scenePath);
		options.IsAdditive = isAdditive;

		Game.ActiveScene.Load(options);
	}

	/// <summary>
	/// Change the current scene to a selected one.
	/// </summary>
	/// <param name="scenePath">A new scene to load.</param>
	/// <param name="isAdditive">Loads as additive.</param>
	/// <param name="scene">Current scene.</param>
	/// <returns></returns>
	public static async Task ChangeScene( string scenePath, bool isAdditive, Scene scene )
	{
		SceneLoadOptions options = new();
		options.SetScene( scenePath );
		options.IsAdditive = isAdditive;

		List<Task> tasks = [];
		scene.RunEvent( delegate ( IBeforeSceneChangeListener x )
		{
			tasks.Add(x.BeforeChange());
		} );
		foreach ( var task in tasks )
		{
			await task;
		}

		Game.ActiveScene.Load( options );
	}
}
