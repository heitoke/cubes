using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class GameManager
{
    #region JSON SAVE AND LOAD

    public static void SaveJson(object data, string fileName)
    {
        WriteToFile(fileName, data.ToString());
    }

    public static string GetFilePath(string fileName)
    {
        return $"{Application.persistentDataPath}/{fileName}";
    }

    public static void WriteToFile(string fileName, string json)
    {
        File.WriteAllText(GetFilePath(fileName), json);
    }

    public static string ReadFromFile(string fileName)
    {
        string path = GetFilePath(fileName);

        if (File.Exists(path))
        {
            string reader = File.ReadAllText(path);
            return reader;
        }
        else Debug.LogWarning($"File {fileName} not found!");

        return "";
    }

    #endregion
}
