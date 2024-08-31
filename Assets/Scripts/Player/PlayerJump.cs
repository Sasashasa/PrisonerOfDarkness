using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJump : MonoBehaviour
{
	public bool OnGround { get; private set; }
    
	[SerializeField] private Transform _groundChecker;
	[SerializeField] private LayerMask _groundLayer;
	[SerializeField] private LayerMask _platformLayer;
	[SerializeField] private float _groundCheckerRadius;
	[SerializeField] private float _jumpForce;
	[SerializeField] private float _jumpTimeMax;
	
	private Rigidbody2D _rb;
	private GameInput _gameInput;
	private float _jumpTime;
	private bool _isJumping;

	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		_gameInput = GameInput.Instance;
		
		_gameInput.OnJumpKeyDown += GameInput_OnJumpKeyDown;
		_gameInput.OnJumpKeyUp += GameInput_OnJumpKeyUp;
		_gameInput.OnPlatformKeyDown += GameInput_OnPlatformKeyDown;
	}

	private void Update()
	{
		OnGround = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckerRadius, _groundLayer);
		
		if (_isJumping)
		{
			_jumpTime += Time.deltaTime;
			_rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
		}
		
		if (_jumpTime >= _jumpTimeMax)
		{
			StopJump();
		}
	}

	private void StartJump()
	{
		if (!OnGround)
			return;
		
		_isJumping = true;
		_jumpTime = 0;
	}

	private void StopJump()
	{
		_isJumping = false;
	}

	private void GameInput_OnJumpKeyDown(object sender, EventArgs e)
	{
		StartJump();
	}

	private void GameInput_OnJumpKeyUp(object sender, EventArgs e)
	{
		StopJump();
	}

	private void GameInput_OnPlatformKeyDown(object sender, EventArgs e)
	{
		Collider2D platformCollider = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckerRadius, _platformLayer);

		if (!platformCollider)
			return;
		
		if (platformCollider.TryGetComponent(out Platform platform))
		{
			platform.DisableCollisionWithPlayer();
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(_groundChecker.position, _groundCheckerRadius);
	}
}