using UnityEngine;

public class Particles : MonoBehaviour
{
	private ParticleSystem _particleSystem;
	private AudioSource _audio;

	// Start is called before the first frame update
	void Start()
	{
		_particleSystem = GetComponent<ParticleSystem>();
		_audio = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		if (_particleSystem.isStopped && !_audio.isPlaying)
			Destroy(gameObject);
	}
}