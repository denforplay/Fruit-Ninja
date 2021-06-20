using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class SceneManagerBase : ISceneManager
{
    public event SceneManagerHandler OnSceneLoadStartedEvent;
    public event SceneManagerHandler OnSceneLoadCompletedEvent;

    public IScene actualScene { get; private set; }
    public Dictionary<string, ISceneConfig> scenesConfigMap { get; }
    public bool isLoading { get; private set; }

    public SceneManagerBase()
    {
        this.scenesConfigMap = new Dictionary<string, ISceneConfig>();
        this.InitializeSceneConfigs();
    }

    public Coroutine InitializeCurrentScene(UnityAction<ISceneConfig> sceneLoadedCallback = null)
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        return this.LoadAndInitializeScene(sceneName, sceneLoadedCallback, false);
    }

    protected Coroutine LoadAndInitializeScene(string sceneName, UnityAction<ISceneConfig> sceneLoadedCallback, bool loadNewScene)
    {
        this.scenesConfigMap.TryGetValue(sceneName, out ISceneConfig config);
        if (config == null)
        {
            throw new ArgumentException();
        }

        return Coroutines.StartRoutine(this.LoadSceneRoutine(config, sceneLoadedCallback, loadNewScene));
    }

    protected virtual IEnumerator LoadSceneRoutine(ISceneConfig config, UnityAction<ISceneConfig> sceneLoadedCallback, bool loadNewScene = true)
    {
        this.isLoading = true;
        this.OnSceneLoadStartedEvent?.Invoke(config);

        if (loadNewScene)
        {
            yield return Coroutines.StartRoutine(this.LoadSceneAsyncRoutine(config));
        }

        yield return Coroutines.StartRoutine(this.InitializeSceneRoutine(config, sceneLoadedCallback));
    }

    protected IEnumerator LoadSceneAsyncRoutine(ISceneConfig config)
    {
        var asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(config.sceneName);
        float progressDivider = 0.9f;
        var progress = asyncOperation.progress / progressDivider;
        while (progress < 1f)
        {
            yield return null;
            progress = asyncOperation.progress / progressDivider;
        }

        asyncOperation.allowSceneActivation = true;
    }

    protected virtual IEnumerator InitializeSceneRoutine(ISceneConfig config, UnityAction<ISceneConfig> sceneLoadedCallback)
    {
        this.actualScene = new Scene(config);
        this.actualScene.CreateInstances();

        yield return this.actualScene.InitializeAsync();

        this.actualScene.Start();
    }

    public Coroutine LoadScene(string sceneName, UnityAction<ISceneConfig> sceneLoadedCallback = null)
    {
        throw new System.NotImplementedException();
    }

    protected abstract void InitializeSceneConfigs();
}
