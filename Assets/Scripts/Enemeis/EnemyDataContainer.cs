using UnityEngine;
using CoreClasses.Models;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Combat/Enemy Data")]
public class EnemyDataContainer : ScriptableObject
{
    [Header("Enemy Settings")]
    public int id;
    public string enemyName;
    public int maxHealth;
    public float speed;
    public int damage;
    public int xpReward;
    public EnemyType type;
    public float baseAccuracy;
   
    public Enemy CreateEnemy()
    {
        return new Enemy(id, enemyName, maxHealth, speed, damage, xpReward, type, baseAccuracy);
    }
}
