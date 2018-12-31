using UnityEngine;

public class DishInstance
{
	public GameObject Instance { get; private set; }

	public DishType Type { get; private set; }

	public DishInstance(GameObject instance, DishType type)
	{
		Instance = instance;
		Type = type;
	}
}