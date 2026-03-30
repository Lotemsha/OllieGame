using UnityEngine;
using System.Collections; // חובה בשביל ה-Coroutine

public class SceneTeleport : MonoBehaviour
{
    [Header("Teleport Settings")]
    public Transform targetDestination; //לאן השחקן עובר
    public GameObject camToDisable;    // המצלמה של החוץ
    public GameObject camToEnable;     // המצלמה של הבית

    [Header("Visual Effects")]
    public Animator fadeAnimator;      // גוררים לכאן את ה-FadeImage
    [Header("Environment Settings")]
    public bool leadsInside;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // מפעיל את רצף הפעולות: פייד -> שיגור -> חזרה מהפייד
            StartCoroutine(TeleportSequence(other.gameObject));
        }
    }

    private IEnumerator TeleportSequence(GameObject player)
    {
        // 1. השגת הסקריפטים של אולי
        PlayerController controller = player.GetComponent<PlayerController>();

        // 2. נטרול תנועה כדי שלא תזוז בזמן המעבר
        if (controller != null)
        { 
            controller.canMove = false; 
        }

        // 3. התחלת הפייד לשחור
        if (fadeAnimator != null)
        {
            fadeAnimator.SetTrigger("StartFade");
        }

        // המתנה קצרה עד שהמסך נהיה שחור לגמרי (לפי אורך האנימציה שלך)
        yield return new WaitForSeconds(0.5f);

        if (controller != null)
        {
            controller.isInside = leadsInside;
        }
        player.transform.position = targetDestination.position;

        if (camToDisable != null && camToEnable != null)
        {
            camToDisable.SetActive(false);
            camToEnable.SetActive(true);
        }

        // 5. סיבוב אולי שתסתכל קדימה
        if (controller != null)
        {
            controller.ForceLookDirection(0f, -1f);
        }

        // 6. סיום הפייד וחזרה למשחק
        if (fadeAnimator != null)
        {
            fadeAnimator.SetTrigger("EndFade");
        }

        // המתנה קצרה לסיום האנימציה לפני החזרת השליטה
        yield return new WaitForSeconds(0.5f);

        if (controller != null) controller.canMove = true;
    }
}