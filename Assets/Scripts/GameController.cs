using UnityEngine;
using CoreClasses.Models;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public GameManager gameManager;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // שומר את האובייקט הנוכחי כ-Instance הראשי
            DontDestroyOnLoad(gameObject); // דואג שה-GameController לא יימחק כשעוברים סצנה

            // אתחול ה-DLL
            DatabaseManager.SetSavePath(Application.persistentDataPath);
            gameManager = new GameManager();
        }
        else
        {
            // אם כבר קיים אחד כזה בסצנה אחרת - נמחק את החדש כדי למנוע כפילויות
            Destroy(gameObject);
        }
    }
}
