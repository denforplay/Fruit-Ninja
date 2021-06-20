using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractorBase 
{
    private Dictionary<Type, IInteractor> _interactorsMap;
    private ISceneConfig _sceneConfig;

    public InteractorBase(ISceneConfig sceneConfig)
    {
        this._interactorsMap = new Dictionary<Type, IInteractor>();
        this._sceneConfig = sceneConfig;
    }

    public void CreateAllInteractors()
    {
        this._interactorsMap = this._sceneConfig.CreateAllInteractors();
    }

    public Coroutine InitializeAllInteractors()
    {
        return Coroutines.StartRoutine(InitializeAllInteractorsRoutine());
    }

    private IEnumerator InitializeAllInteractorsRoutine()
    {
        IInteractor[] allInteractors = this._interactorsMap.Values.ToArray();

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
        IInteractor[] allInteractors = this._interactorsMap.Values.ToArray();

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
        var founded = _interactorsMap.TryGetValue(type, out IInteractor resultInteractor);
        if (founded)
        {
            return (T)resultInteractor;
        }

        foreach (IInteractor interactor in _interactorsMap.Values)
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
        var allInteractors = this._interactorsMap.Values;
        var requiredInteractors = new HashSet<T>();
        foreach (IInteractor interactor in allInteractors)
        {
            if (interactor is T)
            {
                requiredInteractors.Add((T)interactor);
            }
        }

        return requiredInteractors;
    }
}
