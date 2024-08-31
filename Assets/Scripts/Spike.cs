using UnityEngine;

public class Spike : MonoBehaviour
{
	[SerializeField] private int _damage;
	[SerializeField] private float _damageCooldown;

	private float _cooldownTimer;

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.TryGetComponent(out PlayerHealth playerHealth))
		{
			playerHealth.TakeDamage(_damage);
			_cooldownTimer = _damageCooldown;
		}
	}

	private void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.TryGetComponent(out PlayerHealth playerHealth))
		{
			if (_cooldownTimer <= 0)
			{
				playerHealth.TakeDamage(_damage);
				_cooldownTimer = _damageCooldown;
			}
			else
			{
				_cooldownTimer -= Time.deltaTime;
			}
		}
	}
}