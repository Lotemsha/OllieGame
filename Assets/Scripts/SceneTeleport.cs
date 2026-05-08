using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTeleport : MonoBehaviour
{
    public string sceneToLoad;           // שם הסצנה החדשה
    public Vector3 spawnInNewScene;      // המיקום המדויק בסצנה החדשה
    public Vector2 lookDirInNewScene = new Vector2 (0, -1); // כיוון מבט
    public bool isGoingInside;

    public Animator fadeAnimator;        // האנימציה של השחור

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TeleportSequence(other.gameObject));
        }
    }

    private IEnumerator TeleportSequence(GameObject player)
    {
        // 1. עצירת השחקן
        player.GetComponent<PlayerController>().canMove = false;

        // 2. פייד לשחור
        if (fadeAnimator != null) fadeAnimator.SetTrigger("StartFade");
        yield return new WaitForSeconds(0.5f);

        // 3. שמירת הנתונים במוח
        PlayerLocationManager.nextSpawnPoint = spawnInNewScene;
        PlayerLocationManager.lookDirection = lookDirInNewScene;

        // 4. מעבר סצנה
        SceneManager.LoadScene(sceneToLoad);
    }
}
