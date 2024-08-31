using System;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
	public static PlayerScore Instance { get; private set; }
	
	public event EventHandler OnAddScore;
	
	public int Score { get; private set; }

	private void Awake()
	{
		Instance = this;
	}

	public void AddScore()
	{
		Score++;
		OnAddScore?.Invoke(this, EventArgs.Empty);
	}
}