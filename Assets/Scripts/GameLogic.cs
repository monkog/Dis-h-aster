using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
	private int _points;

	public static GameLogic Instance;

	public Image NextSprite;
	public Text Points;
	public Slider Life;
	public GameObject PauseCanvas;

	public void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
		{
			Time.timeScale = (Time.timeScale + 1) % 2;
			PauseCanvas.SetActive(Time.timeScale == 0);
		}
	}

	public void AddPoints()
	{
		_points++;
		Points.text = _points.ToString();
	}

	public void RemovePoints()
	{
		_points--;
		Points.text = _points.ToString();

		if (Life.value == 0)
		{
			GameOver();
		}

		Life.value--;
	}

	private void GameOver()
	{
		_points = 0;
		SceneManager.LoadScene("Menu");
	}
}