using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DishSpawner : MonoBehaviour
{
	public GameObject[] DishPrefabs;
	public GameObject Camera;
	public GameObject Floor;
	public GameObject[] BrokenDishes;

	private int _dishCount;
	private List<DishInstance> _dishes;
	private GameObject _nextDish;
	private DishType _nextDishType;
	private Floor _floor;

	// Use this for initialization
	void Start()
	{
		_nextDish = SelectNextDish();
		_dishes = new List<DishInstance>();
		_floor = Floor.GetComponent<Floor>();
	}

	// Update is called once per frame
	void Update()
	{
		if (GameLogic.Instance.IsGameOver) return;

		var brokenDishes = _dishes.Where(d => d.Instance.GetComponent<Dish>().IsBroken).ToList();
		foreach (var dish in brokenDishes)
		{
			Instantiate(BrokenDishes.ElementAt((int)dish.Type), dish.Instance.transform.position, Quaternion.identity);
			Destroy(dish.Instance.gameObject);
			_dishes.Remove(dish);
		}

		var lastSpawnedDish = _dishes.LastOrDefault();
		var lastPlacedDish = _dishes.LastOrDefault(c => !c.Instance.GetComponent<Dish>().CanMove);

		if (_dishes.Count > 1)
			AdjustGameArea(lastPlacedDish.Instance.gameObject.transform.position);

		if (lastSpawnedDish != null && lastSpawnedDish.Instance.GetComponent<Dish>().CanMove) return;

		_dishes.Add(new DishInstance(SpawnDish(), _nextDishType));
		_nextDish = SelectNextDish();
	}

	private void AdjustGameArea(Vector3 center)
	{
		if (Camera.transform.position.y - center.y > 3) return;
		var delta = 0.03f;

		Camera.transform.Translate(0, delta, 0);
		Floor.transform.Translate(0, delta, 0);
		transform.Translate(0, delta, 0);

		var visibleDishes = _dishes.Where(dish => dish.Instance.transform.position.y >= Floor.transform.position.y).ToList();
		var invisibleDishes = _dishes.Where(dish => dish.Instance.transform.position.y < Floor.transform.position.y).ToList();
		invisibleDishes.ForEach(dish => dish.Instance.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static);

		_floor.AdaptFloorCollider(visibleDishes);
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
		var nextIndex = Random.Range(0, DishPrefabs.Length);
		var next = DishPrefabs[nextIndex];
		_nextDishType = (DishType)Enum.Parse(typeof(DishType), nextIndex.ToString());
		GameLogic.Instance.NextSprite.sprite = next.GetComponent<SpriteRenderer>().sprite;
		return next;
	}
}