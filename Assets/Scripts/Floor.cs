using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Floor : MonoBehaviour
{
	public void AdaptFloorCollider(List<DishInstance> visibleDishes)
	{
		var dishes = visibleDishes.ToArray();
		var colliders = GetComponents<EdgeCollider2D>();
		var leftCollider = colliders.First();
		var rightCollider = colliders.Last();

		var leftBound = leftCollider.points.Last();
		var rightBound = rightCollider.points.First();

		var minX = dishes.Select(dish => dish.Bounds.min.x).Min();
		var maxX = dishes.Select(dish => dish.Bounds.max.x).Max();

		leftCollider.points = leftCollider.points.Take(leftCollider.pointCount - 1).Concat(new[] { new Vector2(minX - 0.3f, leftBound.y) }).ToArray();
		rightCollider.points = (new[] { new Vector2(maxX + 0.3f, rightBound.y) }).Concat(rightCollider.points.Skip(1)).ToArray();
	}
}