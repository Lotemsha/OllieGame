using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using CoreClasses.Models;

public class EnemyBattleTrigger : MonoBehaviour
{
    public string battleSceneName = "BattleScene";
    public Animator fadeAnimator;

    public EnemyDataContainer dataContainer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerLocationManager.nextSpawnPoint = other.transform.position;
            StartCoroutine(StartBattleSequence(other.gameObject));
        }
    }

    private IEnumerator StartBattleSequence(GameObject player)
    {
        player.GetComponent<PlayerController>().canMove = false;

        if (GameController.Instance != null && GameController.Instance.gameManager != null)
        {
            if (dataContainer != null)
            {
                Enemy currentEnemyData = dataContainer.CreateInstance();
                GameController.Instance.gameManager.EnterCombat(currentEnemyData);
            }
            else
            {
                Debug.LogError("Missing Data Container on " + gameObject.name);
            }
        }
        else
        {
            Debug.LogError("GameController or GameManager is missing!");
        }

        if (fadeAnimator != null) fadeAnimator.SetTrigger("BattleFade");

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(battleSceneName);
    }
}