using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IScene
{
    ISceneConfig sceneConfig { get; }
    RepositoryBase repositoriesBase { get; }
    InteractorBase interactorBase { get; }

    void CreateInstances();
    Coroutine InitializeAsync();
    void Start();
    void Save();
    Coroutine SaveAsync(UnityAction callBack);

    T GetRepository<T>() where T : IRepository;
    IEnumerable<T> GetRepositories<T>() where T : IRepository;
    T GetInteractor<T>() where T : IInteractor;
    IEnumerable<T> GetInteractors<T>() where T : IInteractor;
}
