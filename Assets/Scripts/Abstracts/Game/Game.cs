using System;
using UnityEngine;

public enum State
{
    NotInitialized,
    Initializing,
    Initialized,
}

public class Game : MonoBehaviour
{
    public EventHandler OnGameInitializedEvent;

    protected static Game instance;
    public static State State { get; private set; }
    public static bool IsInitilized => State == State.Initialized;


    public Game()
    {
        State = State.NotInitialized;
    }

    public void Initialize()
    {
        State = State.Initializing;
    }    
}
