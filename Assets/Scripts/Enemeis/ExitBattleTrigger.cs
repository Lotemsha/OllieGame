using CoreClasses.Models;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitBattleTrigger : MonoBehaviour
{
    [Header("Battle Settings")]
    public EnemyDatabase enemyPool;
    public Animator fadeAnimator;

    [Header("After Victory")]
    public string citySceneName = "City_Layout";
    public Vector3 spawnAtHouseEntrance;

    [Header("Dialogue")]
    public NPCData npcData;

    private bool _battleStarted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_battleStarted)
        {
            _battleStarted = true;
            StartCoroutine(StartBattleSequence(other.gameObject));
        }
    }

    private IEnumerator StartBattleSequence(GameObject player)
    {
        player.GetComponent<PlayerController>().canMove = false;

        // שמירת מידע לחזרה אחרי הקרב
        PlayerLocationManager.comingFromBattle = true;
        PlayerLocationManager.lastSceneName = citySceneName;
        PlayerLocationManager.nextSpawnPoint = spawnAtHouseEntrance;
        PlayerLocationManager.lastEnemyNPCData = null;

        GameController.Instance.showPostBattleDialogue = false;

        Enemy enemy = enemyPool.GetRandomEnemy().CreateEnemy();
        GameController.Instance.EnterCombat(enemy);

        foreach (var c in FindObjectsOfType<Canvas>())
            c.enabled = false;

        foreach (var ui in FindObjectsOfType<DialogueUI>())
        {
            ui.HideDialogue();
            ui.enabled = false;
        }

        if (fadeAnimator != null)
            fadeAnimator.SetTrigger("BattleFade");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("Battle_Sandbox");
    }
}