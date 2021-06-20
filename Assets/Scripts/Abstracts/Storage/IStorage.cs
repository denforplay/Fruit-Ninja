using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IStorage
{
    Dictionary<string, RepoData> repoDataMap { get; }
    bool isInitialized { get; }
    bool isOnProcess { get; }

    void Load(Scene scene);
    Coroutine LoadAsync(Scene scene);

    bool HasObject(string key);
    void ClearKey(string key);
    void ClearAll();

    void SaveAllRepositories(Scene scene);
    Coroutine SaveAllRepositoriesAsync(Scene scene, UnityAction callback = null);

    void SetFloat(string key, float value);
    void SetInt32(string key, int value);
    void SetBool(string key, bool value);
    void SetString(string key, string value);
    void SetEnum(string key, Enum value);
    void SetCustom<T>(string key, T value);
    void SetRepoData(string key, RepoData value);


    void GetFloat(string key, float defaultValue = 0f);
    void GetInt32(string key, int defaultValue = 0);
    void GetBool(string key, bool defaultValue = false);
    void GetString(string key, string defaultValue = "");
    void GetEnum<T>(string key, T defaultValue) where T : Enum;
    void GetCustom<T>(string key, T defaultValue);
    void GetRepoData(string key, RepoData defaultValue);
}
