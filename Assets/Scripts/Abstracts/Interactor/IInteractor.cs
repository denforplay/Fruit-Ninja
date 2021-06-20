using UnityEngine;
public interface IInteractor
{
    bool IsInitialized { get; }
    void OnCreate();
    Coroutine InitializeAsync();
    void Start();
}
