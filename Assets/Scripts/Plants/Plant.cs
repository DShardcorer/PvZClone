using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Plant : MonoBehaviour
{
    private GridPosition gridPosition;
    protected int health;

    protected float timer;
    protected float actionCooldownTimer = 3f;
    protected float performingActionTimer = 1f;

    public enum State{
        Idle,
        PerformingAction
    }
    protected State state = State.Idle;

    public event EventHandler ActionInitiated;
    public event EventHandler ActionCompleted;
    protected virtual void Awake()
    {
        SetTimer(actionCooldownTimer);
    }

    protected void Update()
    {
        switch (state)
        {
            case State.Idle:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    PerformAction();
                    ChangeState(State.PerformingAction);
                    SetTimer(performingActionTimer);
                    ActionInitiated?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.PerformingAction:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    ChangeState(State.Idle);
                    SetTimer(actionCooldownTimer);
                    ActionCompleted?.Invoke(this, EventArgs.Empty);
                }
                break;
        }

    }
    protected abstract void PerformAction();

    protected void ChangeState(State newState)
    {
        state = newState;
    }

    protected void SetTimer(float time)
    {
        timer = time;
    }

    public void SetGridPosition(GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }


}
