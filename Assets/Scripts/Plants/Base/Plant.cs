using System;

using UnityEngine;


public abstract class Plant : IController
{
    private GridManager _gridManager;
    protected PlantSO _plantSO;
    protected PlantManager _parent;
    protected GridPosition _gridPosition;
    protected PlantProperties _properties;
    protected PlantView _view;

    protected float _timer;


    public enum State
    {
        Idle,
        PerformingAction
    }
    protected State state = State.Idle;

    public event EventHandler ActionInitiated;
    public event EventHandler ActionCompleted;

    public Plant(PlantManager parent, PlantProperties properties, PlantView view)
    {
        _parent = parent;
        _properties = properties;
        _view = view;
    }

    public PlantProperties GetProperties()
    {
        return _properties;
    }
    public PlantView GetView()
    {
        return _view;
    }

    public virtual void Initialize()
    {

        _view.Initialize(this);
        _gridManager = GameManager.Instance.GetGridManager();
        SetGridPosition(_gridManager.GetGridPosition(_view.transform.position));
        Debug.Log(_gridPosition);
        _gridManager.SetPlantAtGridPosition(_gridPosition, this);
        SetTimer(_properties.ActionCooldownTimer);
    }



    public void Update()
    {
        switch (state)
        {
            case State.Idle:
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    if (!CanPerformAction())
                    {
                        SetTimer(_properties.ActionCooldownTimer);
                        return;
                    }
                    PerformAction();
                    ChangeState(State.PerformingAction);
                    SetTimer(_properties.PerformActionTimer);
                    ActionInitiated?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.PerformingAction:
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    ChangeState(State.Idle);
                    SetTimer(_properties.ActionCooldownTimer);
                    ActionCompleted?.Invoke(this, EventArgs.Empty);
                }
                break;
        }

    }
    protected abstract void PerformAction();

    protected abstract bool CanPerformAction();

    protected void ChangeState(State newState)
    {
        state = newState;
    }

    protected void SetTimer(float time)
    {
        _timer = time;
    }

    public void SetGridPosition(GridPosition gridPosition)
    {
        _gridPosition = gridPosition;
    }

    public GridPosition GetGridPosition()
    {
        return _gridPosition;
    }

    public void TakeDamage(int damage)
    {

        _properties.SetHealth(_properties.Health - damage);
        if (_properties.Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _parent.ReturnObject(this);
    }

    public virtual void Dispose()
    {
        _parent = null;
        _properties = null;
        _view = null;
    }
}
