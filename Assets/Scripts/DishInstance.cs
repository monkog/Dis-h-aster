using UnityEngine;

public class DishInstance
{
	private readonly Renderer _renderer;
	private readonly Rigidbody2D _rigidBody;

	public GameObject Instance { get; private set; }

	public DishType Type { get; private set; }

	public Bounds Bounds { get { return _renderer.bounds; } }

	public bool IsStatic { get; private set; }

	public float Velocity { get { return _rigidBody.velocity.y; } }

	public DishInstance(GameObject instance, DishType type)
	{
		Instance = instance;
		Type = type;
		_renderer = instance.GetComponent<Renderer>();
		_rigidBody = instance.GetComponent<Rigidbody2D>();
	}

	public void MakeStatic()
	{
		IsStatic = true;
		_rigidBody.bodyType = RigidbodyType2D.Static;
	}
}