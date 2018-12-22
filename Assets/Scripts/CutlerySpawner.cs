using System.Collections;
using UnityEngine;

public class CutlerySpawner : MonoBehaviour
{
	public GameObject[] CutleryPrefabs;
	private const float Delay = 4f;
	private int _cutleryCount;

	// Use this for initialization
	void Start()
	{
		StartCoroutine(SpawnCutleryCoroutine());
	}

	// Update is called once per frame
	void Update()
	{

	}

	private IEnumerator SpawnCutleryCoroutine()
	{
		while (true)
		{
			_cutleryCount++;
			var position = new Vector3(Random.Range(-0.6f, 0.85f), transform.position.y, 0);
			var cutlery = Instantiate(CutleryPrefabs[Random.Range(0, CutleryPrefabs.Length)], position, Quaternion.identity);
			cutlery.GetComponent<SpriteRenderer>().sortingOrder = _cutleryCount;
			yield return new WaitForSeconds(Delay);
		}
	}
}