using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance { get; private set; }

    [SerializeField] private Animator messageAnimator;
    [SerializeField] private Text messageText;
    public static T GetDataFromJson<T>(string json)
    {
        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
    }

    public static string GetJsonFromData<T>(T data)
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(data);
    }

    public event EventHandler OnLoadGame;
    public event EventHandler OnSaveGame;

    private void Awake()
    {
        if(Instance == null) 
            Instance = this;
    }

    public void LoadGame()
    {
        //LOAD GAME
        OnLoadGame?.Invoke(this, EventArgs.Empty);
        ShowMessage("Loaded");
    }

    public void SaveGame()
    {
        //SAVE GAME
        OnSaveGame?.Invoke(this, EventArgs.Empty);
        ShowMessage("Saved");
    }

    private void ShowMessage(string message)
    {
        messageText.text = message;
        messageAnimator.SetTrigger("ShowMessage");
    }
}
