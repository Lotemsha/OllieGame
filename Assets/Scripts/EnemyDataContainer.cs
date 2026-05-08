using UnityEngine;
using CoreClasses.Models;

public class EnemyDataContainer : MonoBehaviour
{
    // המשתנה הזה יחזיק את האובייקט האמיתי מה-DLL
    public Enemy myEnemyData;

    [Header("Enemy Settings")]
    public int id;
    public string enemyName;
    public int health;
    public float speed;
    public int damage;
    public int xpReward;

    void Awake()
    {
        myEnemyData = new Enemy(id, enemyName, health, speed, damage, xpReward);
    }
}
