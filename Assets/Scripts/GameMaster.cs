using System;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
	public static GameMaster Instance;
	public int BestScore { get; private set; }

	private void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);

		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

	public void SaveLatestScore(int score)
	{
		BestScore = Math.Max(BestScore, score);
	}
}