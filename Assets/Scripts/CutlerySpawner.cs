using System.Collections;
using System.Threading;
using UnityEngine;

public class CutlerySpawner : MonoBehaviour
{
	public GameObject[] CutleryPrefabs;
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
			var cutleryScript = cutlery.GetComponent<Cutlery>();
			var check = 0;
			//while (cutleryScript.CanMove)
			//{
			//	//Debug.Log("Check " + check + " value for cutlery " + _cutleryCount);
			//	//Thread.Sleep(100);
			//}

			yield return new WaitForSeconds(1.0f);
		}
	}
}