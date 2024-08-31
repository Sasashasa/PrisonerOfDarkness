using UnityEngine;

public class Room : MonoBehaviour
{
	[SerializeField] private Transform _entryPoint;
	[SerializeField] private Transform _exitPoint;
	[SerializeField] private GameObject _virtualCamera;
	[SerializeField] private Transform _enemiesGameObject;

	private Transform _player;

	private void Awake()
	{
		DeactivateEnemies();
	}

	private void Start()
	{
		_player = PlayerHealth.Instance.gameObject.transform;
	}

	public void Activate(bool _isNextRoomDirection)
	{
		Vector3 targetPlayerPosition = _isNextRoomDirection ? _entryPoint.position : _exitPoint.position;
		targetPlayerPosition.z = _player.transform.position.z;
		_player.transform.position = targetPlayerPosition;
		
		_virtualCamera.gameObject.SetActive(true);
        
		foreach (Transform enemy in _enemiesGameObject)
		{
			if (enemy.gameObject.GetComponent<EnemyHealth>().IsDead == false)
			{
				enemy.gameObject.SetActive(true);
			}
		}
	}

	public void Deactivate()
	{
		_virtualCamera.gameObject.SetActive(false);
		
		DeactivateEnemies();
	}

	private void DeactivateEnemies()
	{
		foreach (Transform enemy in _enemiesGameObject)
		{
			enemy.gameObject.SetActive(false);
		}
	}
}