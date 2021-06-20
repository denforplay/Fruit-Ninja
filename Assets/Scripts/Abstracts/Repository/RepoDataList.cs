using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RepoDataList
{
    public List<RepoData> values;

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
