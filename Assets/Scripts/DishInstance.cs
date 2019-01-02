using UnityEngine;

public class DishInstance
{
	private readonly Renderer _renderer;

	public GameObject Instance { get; private set; }

	public DishType Type { get; private set; }

	public Bounds Bounds { get { return _renderer.bounds; } }

	public DishInstance(GameObject instance, DishType type)
	{
		Instance = instance;
		Type = type;
		_renderer = instance.GetComponent<Renderer>();
	}
}