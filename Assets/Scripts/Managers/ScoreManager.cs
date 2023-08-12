using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    private const string SCORE_RECORD = "ScoreRecord";
    public static int score;

    Text text;
    
    void Awake ()
    {
        text = GetComponent <Text> ();
        score = 0;
    }

    private void Start()
    {
        DatabaseManager.Instance.OnLoadGame += DatabaseManager_OnLoadGame;
        DatabaseManager.Instance.OnSaveGame += DatabaseManager_OnSaveGame;
    }

    private void DatabaseManager_OnSaveGame(object sender, System.EventArgs e)
    {
        PlayerPrefs.SetInt(SCORE_RECORD, score);
    }

    private void DatabaseManager_OnLoadGame(object sender, System.EventArgs e)
    {
        if (PlayerPrefs.HasKey(SCORE_RECORD))
        {
            score = PlayerPrefs.GetInt(SCORE_RECORD);
        }
    }

    void Update ()
    {
        text.text = "Score: " + score;
    }

}