using System.Collections; // חובה בשביל ה-Coroutine
using UnityEngine;
using UnityEngine.SceneManagement;
using CoreClasses.Models;

public class EnemyBattleTrigger : MonoBehaviour
{
    public string battleSceneName = "BattleScene";
    public Animator fadeAnimator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // שומרים את המיקום הנוכחי של אולי
            PlayerLocationManager.nextSpawnPoint = other.transform.position;

            // מפעילים את רצף המעבר
            StartCoroutine(StartBattleSequence(other.gameObject));
        }
    }

    private IEnumerator StartBattleSequence(GameObject player)
    {
        player.GetComponent<PlayerController>().canMove = false;

        // הגנה: בודקים שה-Instance וה-GameManager קיימים
        if (GameController.Instance != null && GameController.Instance.gameManager != null)
        {
            Enemy currentEnemyData = GetComponent<EnemyDataContainer>().myEnemyData;
            GameController.Instance.gameManager.EnterCombat(currentEnemyData);
        }
        else
        {
            Debug.LogError("GameController or GameManager is missing! check your CoreEngine object.");
        }

        if (fadeAnimator != null) fadeAnimator.SetTrigger("BattleFade");

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(battleSceneName);
    }

}
