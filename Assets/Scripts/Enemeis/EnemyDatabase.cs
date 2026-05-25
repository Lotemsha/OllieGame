using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyDatabase", menuName = "Combat/Enemy Database")]
public class EnemyDatabase : ScriptableObject
{
    [Header("List of possible enemies in this area")]
    public List<EnemyDataContainer> enemies;

    public EnemyDataContainer GetRandomEnemy()
    {
        if (enemies == null || enemies.Count == 0)
            return null;

        int index = Random.Range(0, enemies.Count);
        return enemies[index];
    }
}
