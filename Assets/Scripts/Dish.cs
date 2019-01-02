using UnityEngine;
using UnityEngine.UI;

public class Dish : MonoBehaviour
{
	private const string FloorTag = "Floor";
	private const string PointsBonusTag = "PointsBonus";
	private const string LifeBonusTag = "LifeBonus";

	private const float Speed = 0.1f;

	private bool _canMove = true;

	private bool _isBroken;

	private bool _pointsClaimed;

	private Animator _cameraAnimator;

	private AudioSource _clingSound;

	public bool CanMove { get { return _canMove; } }

	public bool IsBroken { get { return _isBroken; } }

	// Use this for initialization
	void Start()
	{
		_canMove = true;
		_clingSound = GetComponent<AudioSource>();
		_cameraAnimator = GameObject.FindWithTag("ScreenShaker").GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		if (GameLogic.Instance.IsGameOver || !_canMove || Time.timeScale == 0) return;

		if (Input.GetKey(KeyCode.LeftArrow))
			transform.Translate(-Speed, 0, 0);
		if (Input.GetKey(KeyCode.RightArrow))
			transform.Translate(Speed, 0, 0);
		if (Input.GetKey(KeyCode.DownArrow))
			transform.Translate(0, -Speed, 0);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (GameLogic.Instance.IsGameOver) return;

		if (collision.gameObject.tag == LifeBonusTag)
		{
			GameLogic.Instance.AddLife();
			Destroy(collision.gameObject);
			return;
		}

		if (collision.gameObject.tag == PointsBonusTag)
		{
			var points = int.Parse(collision.gameObject.GetComponentInChildren<TextMesh>().text);
			GameLogic.Instance.AddPoints(points);
			Destroy(collision.gameObject);
			return;
		}

		_canMove = false;

		if (collision.gameObject.tag == FloorTag)
		{
			GameLogic.Instance.RemovePoints();
			_cameraAnimator.Play("CameraShake");
			_isBroken = true;
			return;
		}

		if (!_pointsClaimed)
		{
			GameLogic.Instance.AddPoints();
			_pointsClaimed = true;
			_clingSound.Play();
		}
	}
}