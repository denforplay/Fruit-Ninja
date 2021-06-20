using System;
using System.Collections;
using UnityEngine;

public abstract class Repository : IRepository
{
    public EventHandler OnRepositoryInitializedEvent;
    public EventHandler OnRepositoryStartedEvent;

    public State State { get; private set; }
    public bool IsInitialized => this.State == State.Initialized;

    public abstract string id { get; }
    public virtual int version { get; } = 1;

    public Repository()
    {
        State = State.NotInitialized;
    }

    public virtual void OnCreate()
    {
    }


    public Coroutine InitializeAsync()
    {
        if (this.IsInitialized)
        {
            throw new ArgumentException("Repository is already initialized");
        }
        if (State == State.Initializing)
        {
            throw new ArgumentException("Repository is now initializing");
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
        this.OnRepositoryInitializedEvent?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void Initialize()
    {
    }

    protected virtual IEnumerator InitializeRoutine()
    {
        yield break;
    }

    public virtual void OnInitialized()
    {
    }

    public void Start()
    {
        this.OnStart();
        this.OnRepositoryStartedEvent?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnStart()
    {
    }

    public virtual void Save()
    {
    }

    public Coroutine SaveAsync()
    {
        return Coroutines.StartRoutine(this.SaveAsyncRoutine());
    }
    protected virtual IEnumerator SaveAsyncRoutine()
    {
        this.Save();
        yield return null;
    }

    public abstract RepoData GetRepoData();

    public abstract RepoData GetRepoDataDefault();

    public abstract void UploadRepoData(RepoData repoData);

}
