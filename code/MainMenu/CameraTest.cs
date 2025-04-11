namespace HideAndSeek.MainMenu;

[Icon("Camera")]
public sealed class CameraTest : Component
{
	[Property] GameObject ObjectToFollow { get; set; }

	private Rotation _oldRotation;
	protected override void OnUpdate()
	{
		if ( !ObjectToFollow.IsDestroyed)
		{
			_oldRotation = GameObject.LocalRotation;
			GameObject.LocalRotation = GameObject.LocalRotation.LerpTo( Rotation.LookAt( ObjectToFollow.WorldPosition - GameObject.WorldPosition ), Time.Delta * 15 );
		}
		else
		{
			GameObject.LocalRotation = GameObject.LocalRotation.LerpTo( _oldRotation * 0.05f, Time.Delta * 15 );
		}
	}
}
