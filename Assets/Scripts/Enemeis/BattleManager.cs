using CoreClasses.Models;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public EnemyDatabase enemyData;
    public BattleUIController uiController;
    private CombatManager _combat;
    private PlayerManager _player;
    private Enemy _enemy;

    [Header("UI References")]
    public Slider enemyHealthSlider;
    public Slider playerHealthSlider;
    public Slider anxietySlider;

    public EnemyAnxietySettings enemyAnxietySettings;
    void Awake()
    {
        if (GameController.Instance.currentEnemy == null)
        {
            Debug.LogWarning("No enemy found — creating random test enemy");
            var db = Resources.Load<EnemyDatabase>("Enemies/EnemyDatabase");
            if (db == null)
            {
                Debug.LogError("EnemyDatabase not found in Resources!");
                return;
            }
            var chosen = db.GetRandomEnemy();
            _enemy = chosen.CreateEnemy();
        }
        else
        {
            _enemy = GameController.Instance.currentEnemy;
        }
    }
    void Start()
    {
        _combat = new CombatManager();
        _player = GameController.Instance.player;

        _combat.Config.FearBaseIncrease = enemyAnxietySettings.fearBaseIncrease;
        _combat.Config.StressBaseIncrease = enemyAnxietySettings.stressBaseIncrease;
        _combat.Config.AnxietyBaseIncrease = enemyAnxietySettings.anxietyBaseIncrease;
        _combat.Config.IntimidationBaseIncrease = enemyAnxietySettings.intimidationBaseIncrease;
        _combat.Config.ImpatientBaseIncrease = enemyAnxietySettings.impatientBaseIncrease;

        _combat.Config.ShameBaseDecrease = enemyAnxietySettings.shameBaseDecrease;
        _combat.Config.HopelessnessBaseDecrease = enemyAnxietySettings.hopelessnessBaseDecrease;
        _combat.Config.DissociateBaseDecrease = enemyAnxietySettings.dissociateBaseDecrease;
        _combat.Config.NumbnessBaseDecrease = enemyAnxietySettings.numbnessBaseDecrease;
        _combat.Config.TraumaBaseValue = enemyAnxietySettings.traumaBaseValue;
        
        string startMessage = _combat.InitializeBattle(_player, _enemy);

        if (_enemy != null)
        {
            enemyHealthSlider.maxValue = _enemy.MaxHealth;
            enemyHealthSlider.value = _enemy.Health;

            uiController.SetupBattle(_player, _enemy);
            uiController.UpdateUI(_player, _enemy, startMessage);

            if (!_combat.IsPlayerTurn)
                StartCoroutine(EnemyRoutine());
        }
        else
        {
            Debug.LogError("ERROR: Enemy is NULL in BattleManager!");
        }
    }

    public void ExecutePlayerTurnFromButton(int actionIndex)
    {
        ExecutePlayerTurn((AttackType)actionIndex);
    }

    public void ExecutePlayerTurn(AttackType type)
    {
        if (uiController.IsProcessing || !_combat.IsPlayerTurn)
        {
            Debug.Log("It's not the player turn or the system is busy");
            return;
        }

        if (_combat == null || _player == null || _enemy == null) return;

        // ביצוע תור השחקן
        string result = _combat.PlayerTurn(_player, type);

        // עדכון ה-UI
        uiController.UpdateUI(_player, _enemy, result);

        // בדיקה אם האויב מת
        if (_enemy.Health <= 0)
        {
            StartCoroutine(VictorySequence());
            Debug.Log("Victory!");
            return;
        }

        // מעבר לתור האויב
        StartCoroutine(EnemyRoutine());
    }

    IEnumerator EnemyRoutine()
    {
        while (uiController.IsProcessing)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.6f);
        // ביצוע תור האויב
        string result = _combat.EnemyTurn(_player);
        Debug.Log("Player Health after attack: " + _player.Health);
        uiController.UpdateUI(_player, _enemy, result);

        Debug.Log("Enemy turn complete.");

        if (_combat.LastState == BattleState.PlayerLost)
        {
            StartCoroutine(DefeatSequence());
            Debug.Log("Player Defeated!");
            yield break;
        }
    }
    IEnumerator VictorySequence()
    {
        yield return new WaitUntil(() => !uiController.IsProcessing);

        Debug.Log("The battle has ended");

        // סימון NPC כהובס — רק אם הקרב היה מול NPC
        if (PlayerLocationManager.lastEnemyNPCData != null)
        {
            PlayerLocationManager.lastEnemyNPCData.isDefeated = true;
        }

        // XP
        int xpReward = _enemy.RewardXP > 0 ? _enemy.RewardXP : 20;
        _player.GainXP(xpReward);

        // חזרה לעיר
        SceneManager.LoadScene("City_Layout");
    }
    IEnumerator DefeatSequence()
    {
        yield return new WaitUntil(() => !uiController.IsProcessing);
        Debug.Log("Player lost the battle");
        //if (PlayerLocationManager.lastEnemyTrigger != null)
        //{
        //    PlayerLocationManager.lastEnemyTrigger.hasBattled = false;
        //}
        SceneManager.LoadScene("Indoors");
    }

    // לפיתוח
    private void Update()
    {
        // ניצחון מיידי
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("CHEAT: Instant Win");
            StartCoroutine(VictorySequence());
        }

        // הפסד מיידי
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("CHEAT: Instant Lose");
            StartCoroutine(DefeatSequence());
        }
    }
}