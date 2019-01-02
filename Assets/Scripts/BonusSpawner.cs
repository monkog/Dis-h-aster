using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
	public class BonusSpawner : MonoBehaviour
	{
		private const float Timeout = 5;

		public GameObject Camera;
		public GameObject[] BonusPrefabs;

		void Start()
		{
			StartCoroutine(GenerateBonus());
		}

		private IEnumerator GenerateBonus()
		{
			while (!GameLogic.Instance.IsGameOver)
			{
				var random = Random.Range(0, 10);
				if (random != 0 || GetComponents<Bonus>().Any()) yield return new WaitForSeconds(Timeout);

				var bonus = BonusPrefabs[Random.Range(0, BonusPrefabs.Length)];
				var x = Random.Range(-5.0f, 5.0f);
				var y = Camera.transform.position.y + Random.Range(1.5f, 4.0f);
				Instantiate(bonus, new Vector3(x, y), Quaternion.identity);
				yield return new WaitForSeconds(Timeout);
			}
		}
	}
}