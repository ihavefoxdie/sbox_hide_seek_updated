

using Sandbox.Menu;
using Sandbox.Utils.Interfaces;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
