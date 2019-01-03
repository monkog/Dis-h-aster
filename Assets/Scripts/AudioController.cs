using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class AudioController : MonoBehaviour
	{
		private Image _backgroundImage;

		public Sprite[] VolumeSprites;
		public GameObject MusicalNotes;
		private Camera _camera;

		void Start()
		{
			_backgroundImage = GetComponent<Image>();
			_camera = FindObjectOfType<Camera>();
		}

		public void MuteUnMuteAudio()
		{
			int volume = (int)((AudioListener.volume + 1) % 2);
			AudioListener.volume = volume;
			_backgroundImage.sprite = VolumeSprites[volume];

			if (volume == 1)
			{
				var x = MusicalNotes.transform.position.x;
				var y = MusicalNotes.transform.position.y + _camera.transform.position.y;
				Instantiate(MusicalNotes, new Vector3(x, y), MusicalNotes.transform.rotation);
			}
		}
	}
}