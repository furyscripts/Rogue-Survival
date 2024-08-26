using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrefabLoader
{
    GameObject[] LoadPrefabs(string path);
}
