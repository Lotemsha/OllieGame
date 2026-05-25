using System.Collections.Generic;
using UnityEngine;

public static class PlayerLocationManager
{
    // איפה לחזור כשחוזרים מקרב
    public static Vector3 nextSpawnPoint = Vector3.zero;

    // איפה לחזור כשחוזרים מדלת (בית↔עיר)
    public static Vector3 lastDoorExitPoint = Vector3.zero;

    // כיוון המבט
    public static Vector2 lookDirection = new Vector2(0, -1);

    // האם אנחנו כרגע בזרימה של "חוזרים מקרב"
    public static bool comingFromBattle = false;

    // לא משתמשים יותר ב-lastEnemyTrigger לזרימת מיקום
    public static EnemyBattleTrigger lastEnemyTrigger;
    
    public static NPCData lastEnemyNPCData;
    
    public static string lastSceneName;
    public static bool comingFromDoor = false;
}
