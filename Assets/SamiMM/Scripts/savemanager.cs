using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

public static class SaveManager
{
    private static string savePath => Application.persistentDataPath + "/save.json";

    public static void SaveGame(SaveData data)
    {
        var json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log($"[SaveManager] Saved {json.Length} chars to {savePath}");
    }

    public static SaveData LoadGame()
    {
        if (!File.Exists(savePath))
            return null;

        var json = File.ReadAllText(savePath);
        var data = JsonUtility.FromJson<SaveData>(json);
        Debug.Log($"[SaveManager] Loaded save from {savePath}");
        return data;
    }

    public static void DeleteData()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("[SaveManager] Save file deleted");
        }
    }
    
    public static string GetCurrentScene()
    {
        if (!File.Exists(savePath))
            return null;

        var json = File.ReadAllText(savePath);
        var data = JsonUtility.FromJson<SaveData>(json);
        Debug.Log($"[SaveManager] Loaded level string from {savePath}");
        return data.currentScene;
    }
}

[Serializable]
public class SaveData
{
    public string currentScene;
    public List<string> collectedItemIDs = new List<string>();
    public List<string> discoveredClueIds = new List<string>();
    public Dictionary<string, bool> puzzleStates = new Dictionary<string, bool>();
    public List<ScenePickupData> pickupsByScene = new List<ScenePickupData>();
    public bool tutorialHasPlayed;
}

[Serializable]
public class ScenePickupData
{
    public string sceneName;
    public List<string> itemIDs = new List<string>();
}