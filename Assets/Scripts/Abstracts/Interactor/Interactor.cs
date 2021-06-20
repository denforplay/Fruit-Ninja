using System;
using System.Collections;
using UnityEngine;

public abstract class Interactor : IInteractor
{
    public State State { get; private set; }
    public bool IsInitialized => this.State == State.Initialized;

    public event EventHandler OnInteractorInitializedEvent;
    public event EventHandler OnInteractorStartedEvent;
    public Coroutine InitializeAsync()
    {
        if (this.IsInitialized)
        {
            throw new ArgumentException($"Interactor is already initialized");
        }

        if (this.State == State.Initializing)
        {
            throw new ArgumentException($"Interactor is already initializing now");
        }

        return Coroutines.StartRoutine(InitializeRoutineBase());
    }

    protected IEnumerator InitializeRoutineBase()
    {
        this.State = State.Initializing;
        this.Initialize();
        yield return Coroutines.StartRoutine(this.InitializeRoutine());
        this.State = State.Initialized;
        this.OnInitialized();
        this.OnInteractorInitializedEvent?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void Initialize()
    { 
    }

    protected virtual void OnInitialized()
    {
    }

    protected virtual IEnumerator InitializeRoutine()
    {
        yield break;
    }

    public void OnCreate()
    {
        throw new System.NotImplementedException();
    }

    public void Start()
    {
        this.OnInteractorStartedEvent?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnStart()
    {
    }

}
