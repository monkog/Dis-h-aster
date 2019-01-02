using UnityEngine;

namespace Assets.Scripts
{
	public class Bonus : MonoBehaviour
	{
		private const float LifeTime = 5f;

		void Start()
		{
			Destroy(gameObject, LifeTime);
		}
	}
}