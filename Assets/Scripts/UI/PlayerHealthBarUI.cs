using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarUI : MonoBehaviour
{
	[SerializeField] private Image _playerHealthBarFill;
	[SerializeField] private TextMeshProUGUI _playerHealthText;

	private PlayerHealth _playerHealth;

	private void Start()
	{
		_playerHealth = PlayerHealth.Instance;
		_playerHealth.OnTakeDamage += PlayerHealth_OnTakeDamage;
		_playerHealth.OnTakeHealth += PlayerHealth_OnTakeHealth;

		UpdatePlayerHealthBar();
	}

	private void PlayerHealth_OnTakeDamage(object sender, EventArgs e)
	{
		UpdatePlayerHealthBar();
	}

	private void PlayerHealth_OnTakeHealth(object sender, EventArgs e)
	{
		UpdatePlayerHealthBar();
	}

	private void UpdatePlayerHealthBar()
	{
		float fillAmount = (float)_playerHealth.HealthValue / _playerHealth.MaxHealth;
		_playerHealthBarFill.fillAmount = fillAmount;
		
		_playerHealthText.text = $"{_playerHealth.HealthValue}/{_playerHealth.MaxHealth}".ToString();
	}
}