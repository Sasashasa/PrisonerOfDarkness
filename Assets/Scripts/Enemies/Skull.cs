using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(Animator))]
public class Skull : MonoBehaviour
{
	[SerializeField] private GameObject _bulletPrefab;
	[SerializeField] private float _attackCooldown;
	[SerializeField] private float _shootingDistance;
	[SerializeField] private SpriteRenderer _skullVisual;

	private EnemyHealth _enemyHealth;
	private Animator _animator;
	private PlayerHealth _playerHealth;
	private float _cooldownTimer;
	
	private static readonly int Attack = Animator.StringToHash("Attack");

	private void Awake()
	{
		_enemyHealth = GetComponent<EnemyHealth>();
		_animator = GetComponent<Animator>();
		
		_cooldownTimer = _attackCooldown;
	}

	private void Start()
	{
		_playerHealth = PlayerHealth.Instance;
	}

	private void Update()
	{
		if (_enemyHealth.IsDead)
			return;
		
		LookAtPlayer();

		if (_cooldownTimer <= 0 && PlayerInShootingDistance())
		{
			Shoot();
			_cooldownTimer = _attackCooldown;
		}

		_cooldownTimer -= Time.deltaTime;
	}

	private void LookAtPlayer()
	{
		Vector3 difference = _playerHealth.gameObject.transform.position - transform.position;
		
		float zAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        
		transform.rotation = Quaternion.Euler(0f, 0f, zAngle);

		_skullVisual.flipY = _playerHealth.transform.position.x < transform.position.x;
	}

	private void Shoot()
	{
		_animator.SetTrigger(Attack);
		
		Instantiate(_bulletPrefab, transform.position, transform.rotation);
	}

	private bool PlayerInShootingDistance()
	{
		float distance = Vector2.Distance(transform.position, _playerHealth.gameObject.transform.position);
		
		return distance < _shootingDistance;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, _shootingDistance);
	}
}