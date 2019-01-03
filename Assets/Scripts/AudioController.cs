using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class AudioController: MonoBehaviour
	{
		private Image _backgroundImage;

		public Sprite[] VolumeSprites;
		public GameObject MusicalNotes;

		void Start()
		{
			_backgroundImage = GetComponent<Image>();
		}

		public void MuteUnMuteAudio()
		{
			int volume = (int)((AudioListener.volume + 1) % 2);
			AudioListener.volume = volume;
			_backgroundImage.sprite = VolumeSprites[volume];

			if (volume == 1)
				Instantiate(MusicalNotes);
		}
	}
}