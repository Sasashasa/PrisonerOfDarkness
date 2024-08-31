using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private GameObject _bulletHit;
	[SerializeField] private int _damage;
	[SerializeField] private float _movementSpeed;

	private void Awake()
	{
		Destroy(gameObject, 10f);
	}

	private void Update()
	{
        transform.Translate(Vector3.right * (Time.deltaTime * _movementSpeed));
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.TryGetComponent(out Health _health))
		{
			_health.TakeDamage(_damage);
		}

		Instantiate(_bulletHit, transform.position, Quaternion.identity);
		SoundManager.Instance.PlayBulletHitSound(transform.position);
		Destroy(gameObject);
	}
}