using UnityEngine;

public class Heart : MonoBehaviour
{
	[SerializeField] private int _restoringHealthAmount;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent(out PlayerHealth playerHealth))
		{
			playerHealth.TakeHealth(_restoringHealthAmount);
			Destroy(gameObject);
		}
	}
}