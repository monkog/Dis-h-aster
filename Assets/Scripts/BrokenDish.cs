using UnityEngine;

public class BrokenDish : MonoBehaviour
{
	private ParticleSystem _particleSystem;

	// Start is called before the first frame update
	void Start()
	{
		_particleSystem = GetComponent<ParticleSystem>();
	}

	// Update is called once per frame
	void Update()
	{
		if (_particleSystem.isStopped)
			Destroy(gameObject);
	}
}