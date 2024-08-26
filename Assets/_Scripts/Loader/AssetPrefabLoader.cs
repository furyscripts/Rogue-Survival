using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetPrefabLoader : IPrefabLoader
{
    private const string DefaultPath = "Assets/_Prefabs/";
    public GameObject[] LoadPrefabs(string path)
    {
        string fullPath = DefaultPath + path;
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { fullPath });
        GameObject[] prefabs = new GameObject[guids.Length];

        for (int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            prefabs[i] = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
        }

        if (prefabs == null || prefabs.Length == 0)
        {
            Debug.LogWarning($"Không có Prefab nào được nạp từ thư mục {fullPath}.");
        }

        return prefabs;
    }
}
