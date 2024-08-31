using System;
using UnityEngine;

[RequireComponent(typeof(PlayerRotation))]
public class PlayerShooting : MonoBehaviour
{
    public static PlayerShooting Instance { get; private set; }
	
    public EventHandler OnShoot;
    public EventHandler OnStartReloading;
    
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _reloadingArrow;
    [SerializeField] private int _bulletsAmountMax;
    [SerializeField] private float _shootCooldown;
    [SerializeField] private float _reloadingTime;

    private GameInput _gameInput;
    private PlayerRotation _playerRotation;
    private float _reloadingTimer;
    private float _shootCooldownTimer;
    private int _bulletsAmount;
    private bool _isReloading;
    private bool _isShooting;
	

    private void Awake()
    {
        Instance = this;
		
        _bulletsAmount = _bulletsAmountMax;
        _playerRotation = GetComponent<PlayerRotation>();
    }

    private void Start()
    {
        _gameInput = GameInput.Instance;
        _gameInput.OnLeftMouseButtonDown += GameInput_OnLeftMouseButtonDown;
        _gameInput.OnLeftMouseButtonUp += GameInput_OnLeftMouseButtonUp;
    }

    private void Update()
    {
        if (_isReloading)
        {
            _reloadingTimer -= Time.deltaTime;
			
            if (_reloadingTimer <= 0)
            {
                _isReloading = false;
                _bulletsAmount = _bulletsAmountMax;
                _reloadingArrow.gameObject.SetActive(false);
            }
        }
        else
        {
            if (_isShooting)
            {
                if (_shootCooldownTimer <= 0)
                {
                    Shoot();
                    _shootCooldownTimer = _shootCooldown;
                }
            }
        }

        _shootCooldownTimer -= Time.deltaTime;
    }

    private void Shoot()
    {
        if (_isReloading)
            return;
		
        Vector3 targetBulletRotation = _shootPoint.rotation.eulerAngles;

        if (_playerRotation.IsFlipped)
        {
            targetBulletRotation.z += 180;
        }
		
        Instantiate(_bulletPrefab, _shootPoint.position, Quaternion.Euler(targetBulletRotation));
		
        OnShoot?.Invoke(this, EventArgs.Empty);

        _bulletsAmount--;
        
        if (_bulletsAmount == 0)
        {
            _isReloading = true;
            _reloadingTimer = _reloadingTime;
            _reloadingArrow.gameObject.SetActive(true);
            OnStartReloading?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnLeftMouseButtonDown(object sender, EventArgs e)
    {
        _isShooting = true;
    }

    private void GameInput_OnLeftMouseButtonUp(object sender, EventArgs e)
    {
        _isShooting = false;
    }
}