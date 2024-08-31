using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	public float XVelocity { get; private set; }
    
	[SerializeField] private float _movementSpeed;
	
	private Rigidbody2D _rb;
	private GameInput _gameInput;

	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		_gameInput = GameInput.Instance;
	}

	private void Update()
	{
		XVelocity = _gameInput.GetHorizontalMovementValue() * _movementSpeed;
		_rb.velocity = new Vector2(XVelocity, _rb.velocity.y);
	}
}