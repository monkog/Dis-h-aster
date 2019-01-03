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

		if (_dishes.Count > 1)
			AdjustGameArea(_dishes.Take(_dishes.Count - 1).ToList());

		var lastSpawnedDish = _dishes.LastOrDefault();
		if (lastSpawnedDish != null && lastSpawnedDish.Instance.GetComponent<Dish>().CanMove) return;

		_dishes.Add(new DishInstance(SpawnDish(), _nextDishType));
		_nextDish = SelectNextDish();
	}

	private void AdjustGameArea(List<DishInstance> dishes)
	{
		var visibleDishes = dishes.Where(dish => dish.Bounds.max.y >= Floor.transform.position.y - 0.5f).ToList();

		var highestDish = visibleDishes.Where(dish => !dish.IsFalling).Select(dish => dish.Bounds.center.y).Max();

		float delta;
		if (Camera.transform.position.y - highestDish < 3) delta = 0.03f;
		else if (Camera.transform.position.y - highestDish > 3.5) delta = -0.05f;
		else return;

		Camera.transform.Translate(0, delta, 0);
		Floor.transform.Translate(0, delta, 0);
		transform.Translate(0, delta, 0);

		_floor.AdaptFloorCollider(visibleDishes.Where(dish => !dish.IsFalling).ToList());

		var invisibleDishes = dishes.Where(dish => !dish.IsStatic && dish.Instance.transform.position.y < Floor.transform.position.y).ToList();
		invisibleDishes.ForEach(dish => dish.MakeStatic());
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