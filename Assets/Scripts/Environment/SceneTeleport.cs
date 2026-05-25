using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTeleport : MonoBehaviour
{
    public string sceneToLoad;
    public Vector3 spawnInNewScene;
    public Vector2 lookDirInNewScene = new Vector2(0, -1);
    public bool isGoingInside;           

    public Animator fadeAnimator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(TeleportSequence(other.gameObject));
    }

    private IEnumerator TeleportSequence(GameObject player)
    {
        player.GetComponent<PlayerController>().canMove = false;

        if (fadeAnimator != null) fadeAnimator.SetTrigger("StartFade");
        yield return new WaitForSeconds(0.5f);

        // זה *רק* מעבר דרך דלת – שומר נקודת יציאה
        PlayerLocationManager.lastDoorExitPoint = spawnInNewScene;
        PlayerLocationManager.lookDirection = lookDirInNewScene;
        PlayerLocationManager.comingFromDoor = true;
        SceneManager.LoadScene(sceneToLoad);
    }
}
