using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
	public class BonusSpawner : MonoBehaviour
	{
		private const float Timeout = 5;
		public GameObject[] BonusPrefabs;

		void Start()
		{
			StartCoroutine(GenerateBonus());
		}

		private IEnumerator GenerateBonus()
		{
			var random = Random.Range(0, 10);
			Debug.Log(random);
			if (random != 5 || GetComponents<Bonus>().Any()) yield return new WaitForSeconds(Timeout);

			Instantiate(BonusPrefabs[Random.Range(0, BonusPrefabs.Length)]);
			yield return new WaitForSeconds(Timeout);
		}
	}
}