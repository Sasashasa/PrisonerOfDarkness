using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyHealth : Health
{
	private Animator _animator;
	
	private static readonly int Dead = Animator.StringToHash("Dead");

	protected override void Awake()
	{
		base.Awake();
		_animator = GetComponent<Animator>();
	}

	protected override void Die()
	{
		SoundManager.Instance.PlayEnemyDeadSound(transform.position);
		PlayerScore.Instance.AddScore();
		_animator.SetBool(Dead, true);
	}

	public void DisableEnemy()
	{
		gameObject.SetActive(false);
	}
}