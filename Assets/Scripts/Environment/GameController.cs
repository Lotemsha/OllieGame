using CoreClasses.Models;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public GameManager gameManager;

    public PlayerManager player => gameManager?.Player;
    public Enemy currentEnemy;

    public string lastNpcID;
    public bool showPostBattleDialogue;
    public string[] pendingDialogue;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            DatabaseManager.SetSavePath(Application.persistentDataPath);
            gameManager = new GameManager();
            gameManager.NewGame("Ollie");

            Debug.Log("GameManager initialized and Player created!");
        }
        else
        {
            Destroy(gameObject);
        }
    }
 
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Indoors")
        {
            StartCoroutine(PlacePlayerIndoors());
            PlayerLocationManager.comingFromBattle = false;
            PlayerLocationManager.nextSpawnPoint = Vector3.zero;
            return;
        }

        if (scene.name == "City_Layout")
        {
            StartCoroutine(PlacePlayerAfterLoad());
        }
    }
    private IEnumerator PlacePlayerAfterLoad()
    {
        yield return null; // מחכה פריים אחד כדי שהשחקן ייטען

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null) yield break;

        if (PlayerLocationManager.comingFromBattle)
        {
            // חזרה מקרב: למקום הקרב
            playerObj.transform.position = PlayerLocationManager.nextSpawnPoint;
            PlayerLocationManager.comingFromBattle = false;
            PlayerLocationManager.nextSpawnPoint = Vector3.zero;
        }
        else
        {
            // חזרה מדלת: לנקודת הדלת
            if (PlayerLocationManager.lastDoorExitPoint != Vector3.zero)
            {
                playerObj.transform.position = PlayerLocationManager.lastDoorExitPoint;
            }
            else
            {
                // פעם ראשונה במשחק – נקודת ברירת מחדל
                var defaultSpawn = GameObject.Find("DefaultSpawnPoint");
                if (defaultSpawn != null)
                    playerObj.transform.position = defaultSpawn.transform.position;
            }
        }
    }

    public void EnterCombat(Enemy enemy)
    {
        Debug.Log("EnterCombat CALLED with enemy: " + enemy.Name);
        currentEnemy = enemy;
        gameManager.Combat.SetEnemy(enemy);
    }

    private IEnumerator PlacePlayerIndoors()
    {
        yield return null;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null) yield break;

        if (PlayerLocationManager.comingFromDoor)
        {
            // נכנס מהדלת — מיקום הכניסה
            playerObj.transform.position = PlayerLocationManager.lastDoorExitPoint;
            PlayerLocationManager.comingFromDoor = false;
        }
        else
        {
            // חזרה מקרב / ברירת מחדל — המיטה
            GameObject bed = GameObject.Find("BedSpawnPoint");
            if (bed != null)
                playerObj.transform.position = bed.transform.position;
        }
    }

    // לפיתוח
    private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            int cheatXP = 50;
            string msg = player.GainXP(cheatXP);
            Debug.Log($"CHEAT: +{cheatXP} XP. {msg}");
        }
    }
}
