﻿using System.IO;
using UnityEngine;

public class GenericSaveManager<T> : MonoBehaviour where T : new()
{
    private string _directoryPath;

    public T SaveFile;

    public bool UseDebugSaveFile;

    public int LoadedSaveFileNo { get; private set; }

    public string TypeString
    {
        get { return typeof(T).ToString(); }
    }

    public int SaveFileCount
    {
        get { return Directory.Exists(_directoryPath) ? Directory.GetFiles(_directoryPath).Length : 0; }
    }

    public string[] SaveFilePaths
    {
        get { return Directory.Exists(_directoryPath) ? Directory.GetFiles(_directoryPath) : new string[0]; }
    }

    private void Awake()
    {
        LoadedSaveFileNo = -1;

        _directoryPath = Application.persistentDataPath + "/" + TypeString;
    }

    public void CreateNewSaveFile()
    {
        LoadSaveFile(SaveFileCount);
    }

    public void LoadSaveFile(int fileNo = 0)
    {
        LoadedSaveFileNo = fileNo;
        var filePath = _directoryPath + "/"+ TypeString + fileNo;

        if (UseDebugSaveFile)
            return;

        if (!File.Exists(filePath))
        {
            SaveFile = new T();
            return;
        }

        SaveFile = ObjectSerialiser.LoadObjectAt<T>(filePath);
    }

    public void Save(int fileNo = 0)
    {
        if (!Directory.Exists(_directoryPath))
            Directory.CreateDirectory(_directoryPath);

        var filePath = _directoryPath + "/" + TypeString + fileNo;

        ObjectSerialiser.SaveObjectAt(SaveFile, filePath);
    }

    public void SaveCurrentFile()
    {
        Save(LoadedSaveFileNo);
    }

    public void DeleteSaveFile(int fileNo = 0)
    {
        var filePath = _directoryPath + "/" + TypeString + fileNo;

        if (!File.Exists(filePath))
            return;


        File.Delete(filePath);

        var files = Directory.GetFiles(_directoryPath);

        if (fileNo == files.Length) return;

        for(var i = fileNo; i <= files.Length; i++)
            File.Move(files[i], _directoryPath + "/" + TypeString + i);
    }

    public void DeleteAllSaveFiles()
    {
        if (!Directory.Exists(_directoryPath))
            return;

        foreach (var file in Directory.GetFiles(_directoryPath))
            File.Delete(file);
    }
}