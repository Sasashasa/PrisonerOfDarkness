using UnityEngine;

public class WinTrigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<PlayerHealth>())
		{
			LevelUI.Instance.WinGame();
			other.gameObject.SetActive(false);
		}
	}
}