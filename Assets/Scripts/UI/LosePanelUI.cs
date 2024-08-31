using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LosePanelUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _scoreText;
	[SerializeField] private Button _menuButton;

	private void Awake()
	{
		_menuButton.onClick.AddListener(() =>
		{
			Loader.Load(Loader.Scene.StartMenuScene);
		});
	}

	private void OnEnable()
	{
		_scoreText.text = $"Score: {PlayerScore.Instance.Score}";
		_menuButton.Select();
	}
}