using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RepositoryBase : MonoBehaviour
{
    private Dictionary<Type, IRepository> _repositoriesMap;
    private ISceneConfig _sceneConfig;

    public RepositoryBase(ISceneConfig sceneConfig)
    {
        this._repositoriesMap = new Dictionary<Type, IRepository>();
        this._sceneConfig = sceneConfig;
    }

    public void CreateAllRepositories()
    {
        this._repositoriesMap = this._sceneConfig.CreateAllRepositories();
    }

    public Coroutine InitializeAllRepositories()
    {
        return Coroutines.StartRoutine(InitializeAllrepositoriesRoutine());
    }

    private IEnumerator InitializeAllrepositoriesRoutine()
    {
        IRepository[] allRepositories = this._repositoriesMap.Values.ToArray();

        foreach (IInteractor repository in allRepositories)
        {
            if (!repository.IsInitialized)
            {
                yield return repository.InitializeAsync();
            }
        }
    }

    public void StartAllRepositories()
    {
        IRepository[] allRepositories = this._repositoriesMap.Values.ToArray();

        foreach (IRepository repository in allRepositories)
        {
            if (!repository.IsInitialized)
            {
                repository.Start();
            }
        }
    }

    public T GetRepository<T>() where T : IRepository
    {
        var type = typeof(T);
        var founded = _repositoriesMap.TryGetValue(type, out IRepository resultInteractor);
        if (founded)
        {
            return (T)resultInteractor;
        }

        foreach (IRepository repository in _repositoriesMap.Values)
        {
            if (repository is T)
            {
                return (T)repository;
            }
        }

        throw new ArgumentException();
    }

    public IEnumerable<T> GetRepositories<T>() where T : IRepository
    {
        var allRepositories = this._repositoriesMap.Values;
        var requiredRepositories = new HashSet<T>();

        foreach (IRepository repository in allRepositories)
        {
            if (repository is T)
            {
                requiredRepositories.Add((T)repository);
            }
        }

        return requiredRepositories;
    }
}