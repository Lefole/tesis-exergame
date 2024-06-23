using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class GameDataController : MonoBehaviour
{
    public string gameDataFile;

    void Awake()
    {
        gameDataFile = Application.dataPath + "/gameData.json";
    }

    public void SaveGameData(GameData gameData)
    {
        string jsonString=JsonUtility.ToJson(gameData);
        File.WriteAllText(gameDataFile, jsonString);

        Debug.Log("File saved");
    }



}
