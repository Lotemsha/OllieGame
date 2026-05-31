using CoreClasses.Models;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBattleTrigger : MonoBehaviour
{
    [Header("NPC Settings")]
    public string npcID;
    public NPCData npcData;

    [Header("Battle Settings")]
    public string battleSceneName = "BattleScene";
    public EnemyDatabase enemyPool;
    public Animator fadeAnimator;

    private bool _playerInRange = false;
    public bool hasBattled = false;

    void Update()
    {
        // אם ה-NPC כבר הובס — לא מפעילים קרב
        if (npcData != null && npcData.isDefeated)
            return;

        // שחקן בטווח + לא נלחמנו + SPACE
        if (_playerInRange && !hasBattled && Input.GetKeyDown(KeyCode.Space))
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj == null) return;

            Debug.Log("Trigger activated by NPC: " + npcID);
            Debug.Log("npcData on trigger: " + npcData);

            hasBattled = true;

            PlayerLocationManager.lastEnemyTrigger = this;
            PlayerLocationManager.lastEnemyNPCData = npcData;

            PlayerLocationManager.comingFromBattle = true;
            PlayerLocationManager.lastSceneName = SceneManager.GetActiveScene().name;
            PlayerLocationManager.nextSpawnPoint = playerObj.transform.position;

            GameController.Instance.pendingDialogue = npcData.postBattleDialogue;
            GameController.Instance.showPostBattleDialogue = true;

            StartCoroutine(StartBattleSequence(playerObj));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _playerInRange = false;
    }

    private IEnumerator StartBattleSequence(GameObject player)
    {
        player.GetComponent<PlayerController>().canMove = false;

        // יצירת אויב
        Enemy enemy = enemyPool.GetRandomEnemy().CreateEnemy();
        GameController.Instance.EnterCombat(enemy);

        // כיבוי כל ה-Canvas בעיר
        foreach (var c in FindObjectsOfType<Canvas>())
            c.enabled = false;

        // כיבוי כל DialogueUI בעיר
        foreach (var ui in FindObjectsOfType<DialogueUI>())
        {
            ui.HideDialogue();
            ui.enabled = false;
        }

        yield return null;

        if (fadeAnimator != null)
            fadeAnimator.SetTrigger("BattleFade");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(battleSceneName);
    }
}