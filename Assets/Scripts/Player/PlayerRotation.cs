using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
	public bool IsFlipped { get; private set; }
	
	[SerializeField] private Transform _arm;
	
	private GameInput _gameInput;

	private void Start()
	{
		_gameInput = GameInput.Instance;
	}

	private void Update()
	{
		Vector3 difference = Camera.main.ScreenToWorldPoint(_gameInput.GetMousePosition()) - transform.position;
		float zAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

		Vector3 targetLocalScale = Vector3.one;
		float targetZAngle;

		if (zAngle > 90 || zAngle < -90)
		{
			targetLocalScale.x = -1;
			targetZAngle = zAngle + 180;
			IsFlipped = true;
		}
		else
		{
			targetLocalScale.x = 1;
			targetZAngle = zAngle;
			IsFlipped = false;
		}

		transform.localScale = targetLocalScale;
		_arm.rotation = Quaternion.Euler(0f, 0f, targetZAngle);
	}
}