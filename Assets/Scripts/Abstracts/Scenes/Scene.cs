using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scene : IScene
{
    public ISceneConfig sceneConfig { get; }

    public RepositoryBase repositoriesBase { get; }

    public InteractorBase interactorBase { get; }
    public Scene(ISceneConfig config)
    {
        this.sceneConfig = config;
        this.repositoriesBase = new RepositoryBase(config);
        this.interactorBase = new InteractorBase(config);
    }

    public void CreateInstances()
    {
        this.CreateAllRepositories();
        this.CreateAllInteractors();
    }

    private void CreateAllRepositories()
    {
        this.repositoriesBase.CreateAllRepositories();
    }

    private void CreateAllInteractors()
    {
        this.interactorBase.CreateAllInteractors();
    }

    public Coroutine InitializeAsync()
    {
        return Coroutines.StartRoutine(this.InitializeAsyncRoutine());
    }

    private IEnumerator InitializeAsyncRoutine()
    {
        yield return this.repositoriesBase.InitializeAllRepositories();
        yield return this.interactorBase.InitializeAllInteractors();
    }

    public void Start()
    {
        this.repositoriesBase.StartAllRepositories();
        this.interactorBase.StartAllInteractor();
    }

    public void Save()
    {

    }

    public Coroutine SaveAsync(UnityAction callBack)
    {
        throw new System.NotImplementedException();
    }


    public IEnumerable<T> GetInteractors<T>() where T : IInteractor
    {
        return this.interactorBase.GetInteractors<T>();
    }

    public T GetInteractor<T>() where T : IInteractor
    {
        return this.interactorBase.GetInteractor<T>();
    }

    public IEnumerable<T> GetRepositories<T>() where T : IRepository
    {
        return this.repositoriesBase.GetRepositories<T>();
    }

    public T GetRepository<T>() where T : IRepository
    {
        return this.repositoriesBase.GetRepository<T>();
    }
}
