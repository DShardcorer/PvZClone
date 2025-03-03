using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    private EnemyManager _parent;
    private EnemyProperties _properties;
    private EnemyView _view;

    private Plant _targetPlant;
    private bool _isAttacking;
    private Coroutine _attackCoroutine;

    public Enemy(EnemyManager parent, EnemyProperties properties, EnemyView view)
    {
        _parent = parent;
        _properties = properties;
        _view = view;
        _view.Initialize(this);
        //spaw obj SpawnEnemy(enemyName, gridPosition); gans view = doi tuong spaw
    }
    public long GetId()
    {
        return _properties.Id;
    }

    public EnemyView GetView()
    {
        return _view;
    }

    public EnemyProperties GetProperties()
    {
        return _properties;
    }

    public void TakeDamage(int damage)
    {
        _properties.Health -= damage;
        if (_properties.Health <= 0)
        {
            Die();
        }
    }
    public void DamagePlant()
    {
        _targetPlant.TakeDamage(_properties.Damage);
    }

    private void Die()
    {
        _parent.RemoveEnemy(this);
    }

    private IEnumerator AttackCoroutine()
    {
        while (_targetPlant != null)
        {
            _targetPlant.TakeDamage(_properties.Damage);
            yield return new WaitForSeconds(1f / _properties.AttackSpeed);
        }

        _isAttacking = false;
        _view.ResetSpeed();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & LayerHelper.Plants) != 0)
        {
            _targetPlant = other.GetComponent<Plant>();
            if (!_isAttacking)
            {
                _isAttacking = true;
                _view.SetSpeedToZero();
                _attackCoroutine = _view.StartCoroutine(AttackCoroutine());
            }
        }
        else if (((1 << other.gameObject.layer) & LayerHelper.GameOver) != 0)
        {
            _parent.GameOver();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & LayerHelper.Plants) != 0 && _targetPlant != null)
        {
            if (_attackCoroutine != null)
            {
                _view.StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
            _isAttacking = false;
            _targetPlant = null;
            _view.ResetSpeed();
        }
    }

    public void UpdateHp(int hp)
    {
        _properties.Health = hp;
    }

    public void Dispose()
    {
        if (_attackCoroutine != null)
        {
            _view.StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }

        // Remove references
        _targetPlant = null;
        _view.Dispose(); // Ensure EnemyView cleans up resources
        _view = null;
        _properties = null;
        _parent = null;
    }


}
