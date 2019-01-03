using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class AudioController: MonoBehaviour
	{
		private Image _backgroundImage;

		public Sprite[] VolumeSprites;

		void Start()
		{
			_backgroundImage = GetComponent<Image>();
		}

		public void MuteUnMuteAudio()
		{
			AudioListener.volume = (AudioListener.volume + 1) % 2;
			_backgroundImage.sprite = VolumeSprites[(int)AudioListener.volume];
		}
	}
}