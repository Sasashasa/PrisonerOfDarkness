using UnityEngine;

public class Platform : MonoBehaviour
{
	[SerializeField] private float _switchTime;

	private float _switchTimer;
	private bool _isSwitching;

	private const string PlayerLayerName = "Player";
	private const string PlatformLayerName = "Platform";

	private void Update()
	{
		if (_isSwitching)
		{
			if (_switchTimer <= 0)
			{
				_isSwitching = false;
				Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(PlayerLayerName), LayerMask.NameToLayer(PlatformLayerName), false);
			}
			else
			{
				_switchTimer -= Time.deltaTime;
			}
		}
	}

	public void DisableCollisionWithPlayer()
	{
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(PlayerLayerName), LayerMask.NameToLayer(PlatformLayerName), true);
		_switchTimer = _switchTime;
		_isSwitching = true;
	}
}