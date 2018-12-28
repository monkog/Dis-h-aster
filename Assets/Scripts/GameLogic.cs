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

	public void RemovePoints()
	{
		_points--;
		Points.text = _points.ToString();
		Life.value--;
	}

	private void GameOver()
	{
		_points = 0;
		SceneManager.LoadScene("Menu");
	}
}