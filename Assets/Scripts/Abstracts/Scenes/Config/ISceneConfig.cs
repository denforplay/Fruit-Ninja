using System;
using System.Collections.Generic;
public interface ISceneConfig
{
    string sceneName { get; }

    Dictionary<Type, IInteractor> CreateAllInteractors();
    Dictionary<Type, IRepository> CreateAllRepositories();
}
