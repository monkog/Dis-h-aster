using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CutlerySpawner : MonoBehaviour
{
	public GameObject[] CutleryPrefabs;
	public GameObject Camera;

	private int _cutleryCount;
	private List<GameObject> _cutlery;
	private GameObject _nextCutlery;

	// Use this for initialization
	void Start()
	{
		_nextCutlery = SelectNextCutlery();
		_cutlery = new List<GameObject>();
	}

	// Update is called once per frame
	void Update()
	{
		var lastCutlery = _cutlery.LastOrDefault();
		var lastCutlery1 = _cutlery.LastOrDefault(c => !c.GetComponent<Cutlery>().CanMove);

		if (_cutlery.Count > 1)
			AdjustCamera(lastCutlery1.gameObject.transform.position);

		if (lastCutlery != null && lastCutlery.GetComponent<Cutlery>().CanMove) return;

		_cutlery.Add(SpawnCutlery());
		_nextCutlery = SelectNextCutlery();
	}

	private void AdjustCamera(Vector3 center)
	{
		if (Camera.transform.position.y - center.y > 3) return;
		var delta = 0.01f;

		Camera.transform.Translate(0, delta, 0);
		transform.Translate(0, delta, 0);
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