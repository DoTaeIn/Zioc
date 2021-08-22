using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveData
{
    public List<string> savedNameList = new List<string>();
    public List<string> Savename = new List<string>();
    public List<string> Savecode = new List<string>();
}

public class SaveNLoad : MonoBehaviour
{
    private SaveData _saveData = new SaveData();
    public string SAVE_DATA_DIRECTORY;
    public string SAVE_FILENAME = "/NameCodesNTokens.txt";
    private InfoManager _infoManager;
    public List<string> SubjectNamesList;
    public List<string> SubjectCodesList;

    private void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Data_File/";
    }

    private void Awake()
    {
        _infoManager = FindObjectOfType<InfoManager>();
    }

    private void Update()
    {
        

        
    }

    public void SaveInfo()
    {
        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
        SubjectNamesList = new List<string>(_infoManager.SaveInfo.Keys);
        SubjectCodesList = new List<string>(_infoManager.SaveInfo.Values);
        _saveData.Savename = SubjectNamesList;
        _saveData.Savecode = SubjectCodesList;
        _saveData.savedNameList = _infoManager.SubjectNamesList;
        string json = JsonUtility.ToJson(_saveData);
        File.WriteAllText(SAVE_DATA_DIRECTORY+SAVE_FILENAME, json);
        Debug.Log("Saved! " + json);
    }

    public void LoadData()
    {
        string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
        _saveData = JsonUtility.FromJson<SaveData>(loadJson);
        _infoManager.SubjectNamesList = _saveData.savedNameList;
        SubjectNamesList = _saveData.Savename;
        SubjectCodesList = _saveData.Savecode;
        _infoManager.SaveInfo = SubjectNamesList.Zip(SubjectCodesList, (k, v) => new {k, v})
            .ToDictionary(a => a.k, a => a.v);
        MenuManager menus = FindObjectOfType<MenuManager>();
        menus.LoadedMakeButtons(_saveData.Savename, _saveData.Savecode);
    }
}
