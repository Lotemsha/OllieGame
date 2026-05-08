using UnityEngine;
using System.Collections;
using CoreClasses.Models;

public class BattleManager : MonoBehaviour
{
    // משתנים שיחזיקו את הנתונים של הדמויות
    public UnitStats playerStats;
    public UnitStats enemyStats;
    private bool isPlayerTurn = true;

    [Header("Settings")]
    public float turnDelay = 1.0f; // כמה זמן לחכות בין התקפה להתקפה

    //Attack
    public void OnPlayerAttack()
    {
        if (!isPlayerTurn || playerStats.currentHealth <= 0) return;

        // 1. השחקן תוקף את האויב
        Debug.Log("Ollie attacks " + enemyStats.unitName);
        enemyStats.TakeDamage(playerStats.attackDamage);

        // 2. בדיקה: האם האויב עדיין חי?
        if (enemyStats.currentHealth > 0)
        {
            // 3. אם כן, מחכים רגע ואז האויב תוקף חזרה
            StartCoroutine(EnemyTurnRoutine());
        }
        else
        {
            Debug.Log("Victory! The enemy is defeated.");
        }
    }

    // Coroutine שמאפשרת לנו לעצור את הזמן בלי לתקוע את המשחק
    IEnumerator EnemyTurnRoutine()
    {
        Debug.Log("Waiting for enemy turn...");
        yield return new WaitForSeconds(turnDelay);

        // האויב תוקף את אולי
        Debug.Log(enemyStats.unitName + " attacks back!");
        playerStats.TakeDamage(enemyStats.attackDamage);

        if (playerStats.currentHealth > 0)
        {
            isPlayerTurn = true; // מחזירים את התור לשחקן רק בסיום
        }
        else
        {
            Debug.Log("Ollie has fainted...");
        }
    }
}