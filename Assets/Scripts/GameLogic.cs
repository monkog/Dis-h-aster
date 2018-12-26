using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
	private int _points;

	public static GameLogic Instance;

	public Image NextSprite;
	public Text Points;

	public void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

	public void AddPoints()
	{
		_points++;
		Points.text = _points.ToString();
	}

	public void GameOver()
	{
		_points = 0;
		SceneManager.LoadScene("Menu");
	}
}