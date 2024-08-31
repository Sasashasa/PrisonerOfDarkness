using UnityEngine;

public class PreviousRoomTeleport : MonoBehaviour
{
	[SerializeField] private Room _currentRoom;
	[SerializeField] private Room _targetRoom;
	[SerializeField] private bool _isNextRoomDirection;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<PlayerMovement>())
		{
			_targetRoom.Activate(_isNextRoomDirection);
			_currentRoom.Deactivate();
		}
	}
}