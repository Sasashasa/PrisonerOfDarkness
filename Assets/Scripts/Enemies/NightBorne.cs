using System;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(Animator))]
public class NightBorne : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _nightBorneVisual;
	[SerializeField] private Transform _patrolPoint1;
    [SerializeField] private Transform _patrolPoint2;
	[SerializeField] private float _movementSpeed;
	[SerializeField] private float _idleTime;
	[SerializeField] private float _playerDetectionDistance;
	[SerializeField] private int _damage;
	[SerializeField] private float _attackDistance;
	[SerializeField] private float _attackCooldown;
	[SerializeField] private LayerMask _playerLayerMask;

	private EnemyHealth _enemyHealth;
	private Animator _animator;
	private PlayerHealth _playerHealth;
	private Transform _targetPatrolPoint;
	private State _state;
	private float _idleTimer;
	private float _attackCooldownTimer;
    
	private static readonly int StartAttack = Animator.StringToHash("StartAttack");
	private static readonly int IsIdle = Animator.StringToHash("IsIdle");
	private static readonly int IsMoving = Animator.StringToHash("IsMoving");

	private const float DistanceDelta = 0.01f;

	private enum State
	{
		Idle,
		Moving,
		Following,
		Attacking
	}

	private void Awake()
	{
		_enemyHealth = GetComponent<EnemyHealth>();
		_animator = GetComponent<Animator>();
        
		_targetPatrolPoint = _patrolPoint2;
		_idleTimer = _idleTime;
		_state = State.Idle;
	}

	private void Start()
	{
		_playerHealth = PlayerHealth.Instance;
	}

	private void Update()
	{
		if (_enemyHealth.IsDead)
			return;
		
		switch (_state)
		{
			case State.Idle:
				
				if (PlayerInDetectionArea())
				{
					_state = State.Following;
				}
				else
				{
					_idleTimer -= Time.deltaTime;

					if (_idleTimer <= 0)
					{
						_targetPatrolPoint = _targetPatrolPoint == _patrolPoint1 ? _patrolPoint2 : _patrolPoint1;
						_nightBorneVisual.flipX = _targetPatrolPoint.position.x - transform.position.x < 0;
						EnableMovingAnimation();
						_state = State.Moving;
					}
				}
				
				break;
			case State.Moving:
				
				if (PlayerInDetectionArea())
				{
					_state = State.Following;
				}
				else
				{
					MoveToTargetPatrolPoint();

					if (InTargetPatrolPoint())
					{
						SwitchToIdleState();
					}
				}
				
				break;
			case State.Following:
				
				if (!PlayerInDetectionArea())
				{
					SwitchToIdleState();
				}
				else if (PlayerInStartAttackArea())
				{
					_state = State.Attacking;
				}
				else
				{
					_nightBorneVisual.flipX = _playerHealth.gameObject.transform.position.x - transform.position.x < 0;
					TryMoveToPlayer();
				
					if (!InPatrolPoint() && !InTargetPlayerPosition())
					{
						EnableMovingAnimation();
					}
					else
					{
						EnableIdleAnimation();
					}
				}
				
				break;
			case State.Attacking:

				if (!PlayerInStartAttackArea())
				{
					_state = State.Following;
				}
				else
				{
					_nightBorneVisual.flipX = _playerHealth.gameObject.transform.position.x - transform.position.x < 0;
				
					if (_attackCooldownTimer <= 0)
					{
						_animator.SetTrigger(StartAttack);
						_attackCooldownTimer = _attackCooldown;
					}
					else
					{
						_attackCooldownTimer -= Time.deltaTime;
						EnableIdleAnimation();
					}
				}
				
				break;
		}
	}

	public void Attack()
	{
		bool isHitPlayer = Physics2D.OverlapCircle(transform.position, _attackDistance, _playerLayerMask);

		if (isHitPlayer)
		{
			_playerHealth.TakeDamage(_damage);
		}
	}

	private void EnableIdleAnimation()
	{
		_animator.SetBool(IsMoving, false);
		_animator.SetBool(IsIdle, true);
	}

	private void EnableMovingAnimation()
	{
		_animator.SetBool(IsIdle, false);
		_animator.SetBool(IsMoving, true);
	}

	private void SwitchToIdleState()
	{
		_idleTimer = _idleTime;
		EnableIdleAnimation();
		_state = State.Idle;
	}

	private void MoveToTargetPatrolPoint()
	{
		Vector2 targetPosition = transform.position;
		targetPosition.x = _targetPatrolPoint.position.x;
		
		float maxDistanceDelta = _movementSpeed * Time.deltaTime;
		Vector2 nextPosition = Vector2.MoveTowards(transform.position, targetPosition, maxDistanceDelta);

		transform.position = nextPosition;
	}
	
	private void TryMoveToPlayer()
	{
		Vector2 targetPosition = transform.position;
		float minX = _patrolPoint1.position.x;
		float maxX = _patrolPoint2.position.x;
		targetPosition.x = Math.Clamp(_playerHealth.gameObject.transform.position.x, minX, maxX);
		
		float maxDistanceDelta = _movementSpeed * Time.deltaTime;
		Vector2 nextPosition = Vector2.MoveTowards(transform.position, targetPosition, maxDistanceDelta);

		transform.position = nextPosition;
	}

	private bool PlayerInDetectionArea()
	{
		Vector2 playerPosition = _playerHealth.gameObject.transform.position;
		
		float distance = Vector2.Distance(transform.position, playerPosition);
		
		return distance < _playerDetectionDistance;
	}

	private bool PlayerInStartAttackArea()
	{
		Vector2 playerPosition = _playerHealth.gameObject.transform.position;
		
		float distance = Vector2.Distance(transform.position, playerPosition);
		
		return distance < _attackDistance;
	}

	private bool InTargetPatrolPoint()
	{
		return Math.Abs(transform.position.x - _targetPatrolPoint.position.x) < DistanceDelta;
	}

	private bool InTargetPlayerPosition()
	{
		return Math.Abs(transform.position.x - _playerHealth.gameObject.transform.position.x) < DistanceDelta;
	}

	private bool InPatrolPoint()
	{
		bool inPoint1 = Math.Abs(transform.position.x - _patrolPoint1.position.x) < DistanceDelta;
		bool inPoint2 = Math.Abs(transform.position.x - _patrolPoint2.position.x) < DistanceDelta;
		
		return inPoint1 || inPoint2;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, _playerDetectionDistance);
		
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(transform.position, _attackDistance);
	}
}