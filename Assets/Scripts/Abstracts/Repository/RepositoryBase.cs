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

    public void CreateAllInteractors()
    {
        this._repositoriesMap = this._sceneConfig.CreateAllRepositories();
    }

    public Coroutine InitializeAllInteractors()
    {
        return Coroutines.StartRoutine(InitializeAllInteractorsRoutine());
    }

    private IEnumerator InitializeAllInteractorsRoutine()
    {
        IRepository[] allInteractors = this._repositoriesMap.Values.ToArray();

        foreach (IInteractor interactor in allInteractors)
        {
            if (!interactor.IsInitialized)
            {
                yield return interactor.InitializeAsync();
            }
        }
    }

    public void StartAllInteractor()
    {
        IRepository[] allInteractors = this._repositoriesMap.Values.ToArray();

        foreach (IInteractor interactor in allInteractors)
        {
            if (!interactor.IsInitialized)
            {
                interactor.Start();
            }
        }
    }

    public T GetInteractor<T>() where T : IInteractor
    {
        var type = typeof(T);
        var founded = _repositoriesMap.TryGetValue(type, out IRepository resultInteractor);
        if (founded)
        {
            return (T)resultInteractor;
        }

        foreach (IInteractor interactor in _repositoriesMap.Values)
        {
            if (interactor is T)
            {
                return (T)interactor;
            }
        }

        throw new ArgumentException();
    }

    public IEnumerable<T> GetInteractors<T>() where T : IInteractor
    {
        var allInteractors = this._repositoriesMap.Values;
        var requiredInteractors = new HashSet<T>();

        foreach (IRepository interactor in _repositoriesMap.Values)
        {
            if (interactor is T)
            {
                requiredInteractors.Add((T)interactor);
            }
        }

        return requiredInteractors;
    }
}
