using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
#if UNITY_EDITOR
    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";
#else
    private static readonly string SAVE_FOLDER = Application.persistentDataPath + "/Saves/";
#endif
    public static void Initialize()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }
    public static void Save(string saveString, string fileName)
    {
        File.WriteAllText(SAVE_FOLDER + fileName +".json", saveString);
        Debug.Log($"Salvando em: {SAVE_FOLDER}/Teste.json");
    }
    public static string Load(string fileName)
    {

        if (File.Exists(SAVE_FOLDER + fileName +".json"))
        {
            string jsonString = File.ReadAllText(SAVE_FOLDER + fileName +".json");
            return jsonString;
        }
        else 
        {
            return null;
        }
    }
}
