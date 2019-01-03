using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
	private int _points;
	private bool _isBestScore;
	private Camera _camera;

	public static GameLogic Instance;

	public Image NextSprite;
	public Text Points;
	public Text BestScore;
	public Slider Life;
	public bool IsGameOver;

	public GameObject PauseCanvas;
	public GameObject GameOverCanvas;
	public GameObject HighScoreAnimation;

	public void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

	void Start()
	{
		_camera = FindObjectOfType<Camera>();
	}

	public void Update()
	{
		if (IsGameOver) return;

		if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
		{
			Time.timeScale = (Time.timeScale + 1) % 2;
			PauseCanvas.SetActive(Time.timeScale == 0);
		}
	}

	public void AddPoints(int points = 1)
	{
		_points += points;
		Points.text = _points.ToString();

		if (GameMaster.Instance.BestScore > 0 && GameMaster.Instance.BestScore < _points && !_isBestScore)
		{
			Time.timeScale = 0;
			_isBestScore = true;
			Instantiate(HighScoreAnimation, _camera.transform.position, Quaternion.identity);
		}
	}

	public void AddLife()
	{
		if (Life.value != Life.maxValue)
			Life.value++;
	}

	public void RemovePoints()
	{
		if (Life.value == 0)
		{
			GameOver();
		}

		Life.value--;
	}

	private void GameOver()
	{
		IsGameOver = true;
		GameOverCanvas.SetActive(true);

		GameMaster.Instance.SaveLatestScore(_points);
		BestScore.text = "Best score: " + GameMaster.Instance.BestScore;

		Invoke("LoadMainMenu", 5f);
	}

	private void LoadMainMenu()
	{
		SceneManager.LoadScene("Menu");
		IsGameOver = false;
		_isBestScore = false;
	}
}