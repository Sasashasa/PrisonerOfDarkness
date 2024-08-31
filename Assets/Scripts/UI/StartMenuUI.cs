using UnityEngine;
using UnityEngine.UI;

public class StartMenuUI : MonoBehaviour
{
	[SerializeField] private Button _startButton;
	[SerializeField] private Button _settingsButton;
	[SerializeField] private Button _quitButton;
	[SerializeField] private Button _backButton;

	[SerializeField] private Image _startPanel;
	[SerializeField] private Image _settingsPanel;

	[SerializeField] private Slider _masterSlider;
	[SerializeField] private Slider _musicSlider;
	[SerializeField] private Slider _soundSlider;

	private void Awake()
	{
		_masterSlider.value = 100 * PlayerPrefs.GetFloat(PlayerPrefsKeys.MasterVolume, 1);
		_musicSlider.value = 100 * PlayerPrefs.GetFloat(PlayerPrefsKeys.MusicVolume, 1);
		_soundSlider.value = 100 * PlayerPrefs.GetFloat(PlayerPrefsKeys.SoundEffectsVolume, 1);
		
		_startButton.onClick.AddListener(() =>
		{
			Loader.Load(Loader.Scene.LevelScene);
		});
		
		_settingsButton.onClick.AddListener(() =>
		{
			_backButton.Select();
			_settingsPanel.gameObject.SetActive(true);
			_startPanel.gameObject.SetActive(false);
		});
		
		_quitButton.onClick.AddListener(Application.Quit);
		
		_backButton.onClick.AddListener(() =>
		{
			_startButton.Select();
			_startPanel.gameObject.SetActive(true);
			_settingsPanel.gameObject.SetActive(false);
		});
		
		_masterSlider.onValueChanged.AddListener(value =>
		{
			PlayerPrefs.SetFloat(PlayerPrefsKeys.MasterVolume, value / 100);
			MusicManager.Instance.ChangeMusicVolume();
		});
		
		_musicSlider.onValueChanged.AddListener(value =>
		{
			PlayerPrefs.SetFloat(PlayerPrefsKeys.MusicVolume, value / 100);
			MusicManager.Instance.ChangeMusicVolume();
		});
		
		_soundSlider.onValueChanged.AddListener(value =>
		{
			PlayerPrefs.SetFloat(PlayerPrefsKeys.SoundEffectsVolume, value / 100);
		});
	}
}