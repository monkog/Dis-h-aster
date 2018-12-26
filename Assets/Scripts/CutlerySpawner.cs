using UnityEngine;

public class CutlerySpawner : MonoBehaviour
{
	public GameObject[] CutleryPrefabs;
	private int _cutleryCount;
	private GameObject _lastCutlery;
	private GameObject _nextCutlery;

	// Use this for initialization
	void Start()
	{
		_nextCutlery = SelectNextCutlery();
	}

	// Update is called once per frame
	void Update()
	{
		if (_lastCutlery != null && _lastCutlery.GetComponent<Cutlery>().CanMove) return;

		_lastCutlery = SpawnCutlery();
		_nextCutlery = SelectNextCutlery();
	}

	private GameObject SpawnCutlery()
	{
		_cutleryCount++;
		var position = new Vector3(Random.Range(-4.0f, 4.0f), transform.position.y, 0);
		var cutlery = Instantiate(_nextCutlery, position, Quaternion.identity);
		cutlery.GetComponent<SpriteRenderer>().sortingOrder = _cutleryCount;
		return cutlery;
	}

	private GameObject SelectNextCutlery()
	{
		var next = CutleryPrefabs[Random.Range(0, CutleryPrefabs.Length)];
		GameLogic.Instance.NextSprite.sprite = next.GetComponent<SpriteRenderer>().sprite;
		return next;
	}
}