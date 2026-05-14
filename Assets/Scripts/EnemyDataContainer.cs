using UnityEngine;
using CoreClasses.Models;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Combat/Enemy Data")]
public class EnemyDataContainer : ScriptableObject
{
    public Enemy myEnemyData;

    [Header("Enemy Settings")]
    public int id;
    public string enemyName;
    public int health;
    public float speed;
    public int damage;
    public int xpReward;
    public EnemyType type;
    public float baseAccuracy;
   
    public Enemy CreateInstance()
    {
        return new Enemy(id, enemyName, health, speed, damage, xpReward, type, baseAccuracy);
    }
}
