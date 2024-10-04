using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*
 * Author: [Lam, Justin]
 * Last Updated: [10/04/2024]
 * [game manager for prototype (please dont use after prototype)]
 */

public class SavePlaytestData : MonoBehaviour
{
    private bool _toggleFileExport = false;

    public void SaveData()
    {
        if (_toggleFileExport)
        {
            PlaytestData data = new PlaytestData();
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.dataPath + "/playtestData" + System.DateTime.Now + ".json", json);
        }
    }

    public void ToggleFileExport(bool value)
    {
        _toggleFileExport = value;
    }
}