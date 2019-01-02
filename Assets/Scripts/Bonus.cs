using UnityEngine;

namespace Assets.Scripts
{
	public class Bonus : MonoBehaviour
	{
		private const float LifeTime = 5f;

		void Start()
		{
			var text = GetComponentInChildren<TextMesh>();
			if (text != null) text.text = Random.Range(1, 3).ToString();

			Destroy(gameObject, LifeTime);
		}
	}
}