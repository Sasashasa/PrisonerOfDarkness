using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class FireSpirit : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _fireSpiritVisual;
	[SerializeField] private int _damage;
	[SerializeField] private float _idleTime;
	[SerializeField] private float _playerDetectionDistance;
	[SerializeField] private float _movementSpeed;

	private EnemyHealth _enemyHealth;
	private PlayerHealth _playerHealth;
	private Vector2 _targetPosition;
	private float _idleTimer;
	private State _state;
	
	private const float DistanceDelta = 0.01f;
	
	private enum State
	{
		Idle,
		Moving
	}

	private void Awake()
	{
		_enemyHealth = GetComponent<EnemyHealth>();
		
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
		
		
		_fireSpiritVisual.flipX = _playerHealth.gameObject.transform.position.x - transform.position.x < 0;
		
		switch (_state)
		{
			case State.Idle:

				if (_idleTimer <= 0 && PlayerInDetectionArea())
				{
					Vector2 playerPosition = _playerHealth.gameObject.transform.position;
					_targetPosition = playerPosition;
					_state = State.Moving;
				}
				
				_idleTimer -= Time.deltaTime;
				
				break;
			case State.Moving:

				if (!InTargetPosition())
				{
					MoveToTargetPosition();
				}
				else
				{
					_idleTimer = _idleTime;
					_state = State.Idle;
				}
				
				break;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (_enemyHealth.IsDead)
			return;
		
		if (other.GetComponent<PlayerHealth>())
		{
			_playerHealth.TakeDamage(_damage);
		}
	}

	private bool PlayerInDetectionArea()
	{
		Vector2 playerPosition = _playerHealth.gameObject.transform.position;
		
		float distance = Vector2.Distance(transform.position, playerPosition);
		
		return distance < _playerDetectionDistance;
	}

	private bool InTargetPosition()
	{
		return Vector2.Distance(transform.position, _targetPosition) < DistanceDelta;
	}

	private void MoveToTargetPosition()
	{
		float maxDistanceDelta = _movementSpeed * Time.deltaTime;
		
		Vector2 nextPosition = Vector2.MoveTowards(transform.position, _targetPosition, maxDistanceDelta);

		transform.position = nextPosition;
	}
	
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, _playerDetectionDistance);
	}
}