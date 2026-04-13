using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject[] maps;

    Bench bench;

    private void OnEnable()
    {
        bench = FindObjectOfType<Bench>();
        if(bench != null)
        {
            if(bench.interacted)
            {
                UpdateMap();
            }
        }
    }

    void UpdateMap()
    {
        var savedScenes = SaveData.Instance.sceneNames;

        for(int i = 0; i < maps.Length; i++)
        {
            if(savedScenes.Contains("Outskirts_" + (i + 1)))
            {
                maps[i].SetActive(true);
            }
            else
            {
                maps[i].SetActive(false);
            }
        }
    }
}
