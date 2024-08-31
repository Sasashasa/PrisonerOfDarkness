using UnityEngine;
using UnityEngine.UI;

public class PausePanelUI : MonoBehaviour
{
	[SerializeField] private Button _pauseButton;
	[SerializeField] private Button _continueButton;
	[SerializeField] private Button _restartButton;
	[SerializeField] private Button _menuButton;

	private void Awake()
	{
		_continueButton.onClick.AddListener(() =>
		{
			gameObject.SetActive(false);
			_pauseButton.gameObject.SetActive(true);
			Time.timeScale = 1f;
		});
		
		_restartButton.onClick.AddListener(() =>
		{
			Loader.Load(Loader.Scene.LevelScene);
		});
		
		_menuButton.onClick.AddListener(() =>
		{
			Loader.Load(Loader.Scene.StartMenuScene);
		});
	}

	private void OnEnable()
	{
		_continueButton.Select();
	}
}