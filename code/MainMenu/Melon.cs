namespace HideAndSeek.MainMenu;

public sealed class Melon : Component, ISceneCollisionEvents
{
	[Property] public Prop PropComponent { get; set; }

	protected override void OnAwake()
	{
		base.OnAwake();
		PropComponent ??= GameObject.GetComponent<Prop>();
	}

	void ISceneCollisionEvents.OnCollisionStart( Collision collision )
	{
		if ( collision.Self.GameObject == GameObject )
		{
			DamageInfo hit = new();
			hit.Damage = collision.Self.Collider.Rigidbody.Velocity.Length * 0.0015f;
			//Log.Info( hit.Damage );
			PropComponent.OnDamage( hit );
		}
	}

	protected override void OnUpdate()
	{
	}
}
