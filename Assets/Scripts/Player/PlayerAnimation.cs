using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerJump))]
public class PlayerAnimation : MonoBehaviour
{
	private Animator _animator;
	private PlayerMovement _playerMovement;
	private PlayerJump _playerJump;
	
	private static readonly int AbsoluteHorizontalVelocity = Animator.StringToHash("AbsoluteHorizontalVelocity");
	private static readonly int OnGround = Animator.StringToHash("OnGround");

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_playerMovement = GetComponent<PlayerMovement>();
		_playerJump = GetComponent<PlayerJump>();
	}

	private void Update()
	{
		_animator.SetFloat(AbsoluteHorizontalVelocity, Mathf.Abs(_playerMovement.XVelocity));
		_animator.SetBool(OnGround, _playerJump.OnGround);
	}
}