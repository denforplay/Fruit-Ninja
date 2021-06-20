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
        throw new System.NotImplementedException();
    }

    public T GetInteractor<T>() where T : IRepository
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<T> GetInteractors<T>() where T : IInteractor
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<T> GetRepositories<T>() where T : IRepository
    {
        throw new System.NotImplementedException();
    }

    public T GetRepository<T>() where T : IRepository
    {
        throw new System.NotImplementedException();
    }

    public Coroutine InitializeAsync()
    {
        throw new System.NotImplementedException();
    }

    public void Save()
    {
        throw new System.NotImplementedException();
    }

    public Coroutine SaveAsync(UnityAction callBack)
    {
        throw new System.NotImplementedException();
    }

    public void Start()
    {
        throw new System.NotImplementedException();
    }
}
