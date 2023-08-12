using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInformation : MonoBehaviour
{
    public EnemyType enemyType;
    public EnemyInfo GetEnemyInfo()
    {
        return new EnemyInfo
        {
            characterData = new(transform.position, transform.eulerAngles),
            health = GetComponent<EnemyHealth>().CurrentHealth,
            enemyType = enemyType
        };
    }
}

[System.Serializable]
public class EnemyInfo
{
    public EnemyType enemyType;
    public CharacterData characterData;
    public int health;
}
