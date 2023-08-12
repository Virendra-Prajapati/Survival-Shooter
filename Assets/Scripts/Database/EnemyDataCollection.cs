using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDataCollection : MonoBehaviour
{
    public const string ENEMY_LIST_INFO = "EnemyListInformation";
    public static EnemyDataCollection Instance { get; private set; }


    [SerializeField] private EnemyManager zombunnySpawner;
    [SerializeField] private EnemyManager zombearSpawner;
    [SerializeField] private EnemyManager hellephantSpawner;
    

    

    [SerializeField]
    private List<EnemyInformation> enemiesInformationList;

    private void Awake()
    {
        if(Instance == null) 
            Instance = this;
        enemiesInformationList = new List<EnemyInformation>();
    }


    private void Start()
    {
        DatabaseManager.Instance.OnSaveGame += DatabaseManager_OnSaveGame;
        DatabaseManager.Instance.OnLoadGame += DatabaseManager_OnLoadGame;
    }

    private void DatabaseManager_OnLoadGame(object sender, System.EventArgs e)
    {
        if (!PlayerPrefs.HasKey(ENEMY_LIST_INFO))
            return;
        string json = PlayerPrefs.GetString(ENEMY_LIST_INFO);
        List<EnemyInfo> enemyInfoList = DatabaseManager.GetDataFromJson<List<EnemyInfo>>(json);
        StartCoroutine(LoadDataCoroutine(enemyInfoList));
    }

    private void DatabaseManager_OnSaveGame(object sender, System.EventArgs e)
    {
        List<EnemyInfo> enemyInfoList = new();
        foreach(EnemyInformation enemyInformation in enemiesInformationList)
        {
            enemyInfoList.Add(enemyInformation.GetEnemyInfo());
        }
        string json = DatabaseManager.GetJsonFromData(enemyInfoList);
        PlayerPrefs.SetString(ENEMY_LIST_INFO, json);
    }

    public void EnemySpawned(EnemyInformation enemyInformation)
    {
        enemiesInformationList.Add(enemyInformation);
    }

    public void EnemyDestroyed(EnemyInformation enemyInformation)
    {
        enemiesInformationList.Remove(enemyInformation);
    }

    private IEnumerator LoadDataCoroutine(List<EnemyInfo> enemyInfoList)
    {
        foreach(EnemyInformation enemyInformation in enemiesInformationList)
        {
            Destroy(enemyInformation.gameObject);
        }
        enemiesInformationList.Clear();
        yield return null;
        foreach(EnemyInfo enemyInfo in enemyInfoList)
        {
            switch (enemyInfo.enemyType)
            {
                case EnemyType.Zombunny:
                    zombunnySpawner.SpawnOnLoad(enemyInfo);
                    break;
                case EnemyType.Zombear:
                    zombearSpawner.SpawnOnLoad(enemyInfo);
                    break;
                case EnemyType.Hellephant:
                    hellephantSpawner.SpawnOnLoad(enemyInfo);
                    break;
            }
        }
    }

    
}
