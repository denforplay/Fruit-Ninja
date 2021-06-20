using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRepository
{
    bool IsInitialized { get; }
    string id { get; }
    int version { get; }

    void OnCreate();
    Coroutine InitializeAsync();
    void Start();

    RepoData GetRepoData();
    RepoData GetRepoDataDefault();
    void UploadRepoData(RepoData repoData);
}
