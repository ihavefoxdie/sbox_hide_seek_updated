using HideAndSeek.UI;
using Sandbox.Utils.Interfaces;
using System;
using System.Threading.Tasks;

namespace HideAndSeek.GameLoop;

public class SceneFadingTransition : GameObjectSystem, IBeforeSceneChangeListener
{
	private bool _disposed = false;
	private BlackScreen _blackScreen;

	public SceneFadingTransition( Scene scene ) : base( scene )
	{
		Listen( Stage.SceneLoaded, 10, AddBlackScreen, "blackScreen" );
	}

	public async Task BeforeChange()
	{
		if ( _blackScreen != null )
		{
			Log.Info( "Test" );
			_blackScreen.Opacity = 1;
			await Task.Delay( 1000 );
			GameObject blackScreenObject = _blackScreen.GameObject;
			blackScreenObject.Destroy();
			//_blackScreen.Destroy();
		}
	}

	//public override void Dispose()
	//{
	//	if ( _disposed )
	//	{
	//		return;
	//	}

	//	_disposed = true;
	//	GC.SuppressFinalize( this );

	//	base.Dispose();
	//}

	private void AddBlackScreen()
	{
		if ( Scene.GetAll<BlackScreen>().Any() || Scene.IsEditor )
		{
			return;
		}
		Log.Info( "Added black screen" );
		GameObject gameObject = Scene.CreateObject();
		gameObject.Name = "FadingTransition";
		ScreenPanel screenPanel = gameObject.AddComponent<ScreenPanel>();
		screenPanel.ZIndex = 110;
		_blackScreen = gameObject.AddComponent<BlackScreen>();
		//instance.MapName = MapIdent;
		//Scene.AddComponent<BlackScreen>();
	}
}
