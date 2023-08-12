using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public EnemyType enemyType;


    void Start ()
    {
        InvokeRepeating (nameof(Spawn), spawnTime, spawnTime);
    }


    void Spawn ()
    {
        if(playerHealth.CurrentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        EnemyInformation enemyInformation = Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation).GetComponent<EnemyInformation>();
        enemyInformation.enemyType = enemyType;
        EnemyDataCollection.Instance.EnemySpawned(enemyInformation);
    }

    public void SpawnOnLoad(EnemyInfo enemyInfo)
    {
        if(playerHealth.CurrentHealth <= 0f)
        {
            return;
        }
        EnemyInformation enemyInformation = Instantiate (enemy).GetComponent<EnemyInformation>();
        enemyInformation.transform.position = enemyInfo.characterData.Position;
        enemyInformation.transform.eulerAngles = enemyInfo.characterData.Rotation;
        enemyInformation.enemyType = enemyType;
        enemyInformation.GetComponent<EnemyHealth>().CurrentHealth = enemyInfo.health;
        EnemyDataCollection.Instance.EnemySpawned(enemyInformation);
    }
}
