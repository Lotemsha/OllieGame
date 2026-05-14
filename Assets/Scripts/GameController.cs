using UnityEngine;
using CoreClasses.Models;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public GameManager gameManager;

    public PlayerManager player => gameManager?.Player;
    public Enemy currentEnemy;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            DatabaseManager.SetSavePath(Application.persistentDataPath);
            gameManager = new GameManager();

            // --- השורה החשובה שחסרה ---
            // אנחנו צריכים להגיד ל-DLL ליצור שחקן חדש (Ollie)
            gameManager.NewGame("Ollie");

            Debug.Log("GameManager initialized and Player created!");
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
