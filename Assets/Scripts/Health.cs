using System;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public event EventHandler OnTakeDamage;
    public event EventHandler OnTakeHealth;
    
    public int HealthValue { get; private set; }
    public int MaxHealth => _maxHealth;
    public bool IsDead { get; private set; }
    
    [SerializeField] private SpriteRenderer _gameObjectVisual;
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _hitColorChangingSpeed;
    
    private bool _isChangingToHitColor;
    private float _greenAndBlueComponent;

    protected virtual void Awake()
    {
        HealthValue = _maxHealth;
        _greenAndBlueComponent = 1f;
    }

    private void Update()
    {
        ChangeColor();
    }
    
    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;
        
        if (IsDead)
            return;

        _isChangingToHitColor = true;
        _greenAndBlueComponent = 1;

        HealthValue = Mathf.Clamp(HealthValue - damage, 0, _maxHealth);
		
        OnTakeDamage?.Invoke(this, EventArgs.Empty);

        if (HealthValue == 0)
        {
            IsDead = true;
            Die();
        }
    }
    
    public void TakeHealth(int health)
    {
        if (health <= 0)
            return;
        
        HealthValue = Mathf.Clamp(HealthValue + health, 0, _maxHealth);
		
        OnTakeHealth?.Invoke(this, EventArgs.Empty);
    }

    private void ChangeColor()
    {
        if (_isChangingToHitColor)
        {
            _greenAndBlueComponent -= Time.deltaTime * _hitColorChangingSpeed;

            if (_greenAndBlueComponent <= 0)
            {
                _isChangingToHitColor = false;
            }
        }
        else
        {
            _greenAndBlueComponent += Time.deltaTime * _hitColorChangingSpeed;
        }
		
        _gameObjectVisual.color = new Color(1, _greenAndBlueComponent, _greenAndBlueComponent);
    }

    protected abstract void Die();
}