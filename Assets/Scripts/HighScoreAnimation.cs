using UnityEngine;

namespace Assets.Scripts
{
	public class HighScoreAnimation : MonoBehaviour
	{
		private ParticleSystem _particleSystem;

		// Start is called before the first frame update
		void Start()
		{
			_particleSystem = GetComponentInChildren<ParticleSystem>();
		}

		// Update is called once per frame
		void Update()
		{
			if (_particleSystem.isStopped)
			{
				Time.timeScale = 1;
				Destroy(gameObject);
			}
		}
	}
}