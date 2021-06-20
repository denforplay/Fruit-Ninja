using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void SceneManagerHandler(ISceneConfig config);

public interface ISceneManager
{
    event SceneManagerHandler OnSceneLoadStartedEvent;
    event SceneManagerHandler OnSceneLoadCompletedEvent;

    IScene actualScene { get; }
    Dictionary<string, ISceneConfig> scenesConfigMap { get; }

    Coroutine LoadScene(string sceneName, UnityAction<ISceneConfig> sceneLoadedCallback = null);
    Coroutine InitializeCurrentScene(UnityAction<ISceneConfig> sceneLoadedCallback = null);

}
