using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour
{
    //stuff that shouldn't be editable from editor
    public static GameControl control;
    public bool AbleToLoadGame { get; set; } 
    public bool InvertY { get; set; }
    public bool InvertX { get; set; }
    public float MouseSensitivity { get; set; }
    public float BackgroundMusicVolume { get; set; }
    public float SoundEffectsVolume { get; set; }
    public string LastKnownFileName { get; set; }

    public bool WasLoaded { get; set; }
    public const string tempAutoSaveFileLocation = "TempSaveSpot3693";

    //not to be changed w/ options
    private string fileNameExtension;

    void Awake()
    {
        //Edit script Awake call through Edit->ProjectSettings->Script Order
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);

            fileNameExtension = ".dat";

            SetDefaultOptionSettings();
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }

    private void SetDefaultOptionSettings()
    {
        control = this;
        InvertY = false;
        InvertX = false;
        MouseSensitivity = 15F;
        BackgroundMusicVolume = 1.0f;
        SoundEffectsVolume = 0.25f;
        LastKnownFileName = "default";
    }

    public void Save(string fileName)
    {
        fileName = fileName + fileNameExtension;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + fileName);

        String content = "AllContent, can be a class";

        bf.Serialize(file, content);
        file.Close();
    }
    public void Load(string fileName)
    {
        fileName = fileName + fileNameExtension; 
        if (File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + fileName, FileMode.Open);

            //String content = (String)bf.Deserialize(file);
            file.Close();

            WasLoaded = AbleToLoadGame = true;
        }
    }

    public void SetToDefaultGameValues()
    {
        WasLoaded = false;
    }
}