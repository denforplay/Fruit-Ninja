using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRepoDataAdapter
{
    RepoData Adapt(RepoData oldRepoData);
}
