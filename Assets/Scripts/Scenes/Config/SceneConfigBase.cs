using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneConfigBase : ISceneConfig
{
    public abstract string sceneName { get; }
    public abstract Dictionary<Type, IRepository> CreateAllRepositories();
    public abstract Dictionary<Type, IInteractor> CreateAllInteractors();

    protected T CreateRepository<T>(Dictionary<Type, IRepository> repositoriesMap) where T : IRepository, new()
    {
        T createdRepository = new T();
        var type = typeof(T);
        repositoriesMap[type] = createdRepository;
        createdRepository.OnCreate();
        return createdRepository;
    }

    protected T CreateInteractor<T>(Dictionary<Type, IInteractor> interactorsMap) where T: IInteractor, new()
    {
        T createdInteractor = new T();
        var type = typeof(T);
        interactorsMap[type] = createdInteractor;
        createdInteractor.OnCreate();
        return createdInteractor;
    }

}
