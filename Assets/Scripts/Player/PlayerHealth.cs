public class PlayerHealth : Health
{
	public static PlayerHealth Instance { get; private set; }

	protected override void Awake()
	{
		base.Awake();
		Instance = this;
	}

	protected override void Die()	
	{
		LevelUI.Instance.LoseGame();
		gameObject.SetActive(false);
	}
}