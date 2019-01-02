using UnityEngine;

namespace Assets.Scripts
{
	public class Bonus : MonoBehaviour
	{
		private const float LifeTime = 5f;

		public GameObject SparksParticles;

		void Start()
		{
			var text = GetComponentInChildren<TextMesh>();
			if (text != null) text.text = Random.Range(1, 3).ToString();

			Destroy(gameObject, LifeTime);
		}

		void OnCollisionEnter2D(Collision2D collision)
		{
			Instantiate(SparksParticles, transform.position, Quaternion.identity);
		}
	}
}