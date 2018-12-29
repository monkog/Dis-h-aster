using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DishSpawner : MonoBehaviour
{
	public GameObject[] DishPrefabs;
	public GameObject Camera;

	private int _dishCount;
	private List<GameObject> _dishes;
	private GameObject _nextDish;

	// Use this for initialization
	void Start()
	{
		_nextDish = SelectNextDish();
		_dishes = new List<GameObject>();
	}

	// Update is called once per frame
	void Update()
	{
		var lastSpawnedDish = _dishes.LastOrDefault();
		var lastPlacedDish = _dishes.LastOrDefault(c => !c.GetComponent<Dish>().CanMove);

		if (_dishes.Count > 1)
			AdjustCamera(lastPlacedDish.gameObject.transform.position);

		if (lastSpawnedDish != null && lastSpawnedDish.GetComponent<Dish>().CanMove) return;

		_dishes.Add(SpawnDish());
		_nextDish = SelectNextDish();
	}

	private void AdjustCamera(Vector3 center)
	{
		if (Camera.transform.position.y - center.y > 3) return;
		var delta = 0.01f;

		Camera.transform.Translate(0, delta, 0);
		transform.Translate(0, delta, 0);
	}

	private GameObject SpawnDish()
	{
		_dishCount++;
		var position = new Vector3(Random.Range(-4.0f, 4.0f), transform.position.y, 0);
		var dish = Instantiate(_nextDish, position, Quaternion.identity);
		dish.GetComponent<SpriteRenderer>().sortingOrder = _dishCount;
		return dish;
	}

	private GameObject SelectNextDish()
	{
		var next = DishPrefabs[Random.Range(0, DishPrefabs.Length)];
		GameLogic.Instance.NextSprite.sprite = next.GetComponent<SpriteRenderer>().sprite;
		return next;
	}
}