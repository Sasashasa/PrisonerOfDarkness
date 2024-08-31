using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
	public static LevelUI Instance { get; private set; }
	
	[SerializeField] private TextMeshProUGUI _scoreText;

	[SerializeField] private Image _pausePanel;
	[SerializeField] private Image _winPanel;
	[SerializeField] private Image _losePanel;
	
	[SerializeField] private Button _pauseButton;
    
	private bool _isGameOver;

	private void Awake()
	{
		Instance = this;
		
		Time.timeScale = 1f;
		
		_scoreText.text = "Score: 0";
		
		_pauseButton.onClick.AddListener(SwitchGamePause);
	}

	private void Start()
	{
		GameInput.Instance.OnPauseButtonDown += GameInput_OnPauseButtonDown;
		PlayerScore.Instance.OnAddScore += PlayerScore_OnAddScore;
	}

	public void WinGame()
	{
		_isGameOver = true;
		_winPanel.gameObject.SetActive(true);
		_pauseButton.gameObject.SetActive(false);
		Time.timeScale = 0f;
	}

	public void LoseGame()
	{
		_isGameOver = true;
		_losePanel.gameObject.SetActive(true);
		_pauseButton.gameObject.SetActive(false);
		Time.timeScale = 0f;
	}

	private void SwitchGamePause()
	{
		if (_pausePanel.gameObject.activeSelf)
		{
			_pausePanel.gameObject.SetActive(false);
			_pauseButton.gameObject.SetActive(true);
			Time.timeScale = 1f;
		}
		else
		{
			_pausePanel.gameObject.SetActive(true);
			_pauseButton.gameObject.SetActive(false);
			Time.timeScale = 0f;
		}
	}

	private void GameInput_OnPauseButtonDown(object sender, EventArgs e)
	{
		if (_isGameOver)
			return;
		
		SwitchGamePause();
	}

	private void PlayerScore_OnAddScore(object sender, EventArgs e)
	{
		_scoreText.text = $"Score: {PlayerScore.Instance.Score}";
	}
}