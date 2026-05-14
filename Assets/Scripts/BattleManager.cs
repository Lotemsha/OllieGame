using CoreClasses.Models;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public EnemyDataContainer enemyData;
    public BattleUIController uiController;
    private CombatManager _combat;
    private PlayerManager _player;
    private Enemy _enemy;
    private AnxietyBar _anxietyBar;

    [Header("UI References")]
    public Slider enemyHealthSlider;
    public Slider playerHealthSlider;
    public Slider anxietySlider;

    void Start()
    {
        _combat = new CombatManager();
        _player = GameController.Instance.player;

        string startMessage = _combat.InitializeBattle(_player);

        _enemy = _combat.CurrentEnemy;

        if (_enemy != null)
        {
            if (enemyHealthSlider != null)
            {
                enemyHealthSlider.maxValue = _enemy.MaxHealth;
                enemyHealthSlider.value = _enemy.Health;
            }

            if (uiController != null)
            {
                uiController.SetupBattle(_player, _enemy);
                uiController.UpdateUI(_player, _enemy, startMessage);
            }

            if (!_combat.IsPlayerTurn)
            {
                StartCoroutine(EnemyRoutine());
            }
        }
    }
    public void ExecutePlayerTurn(int type)
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
    }
    IEnumerator VictorySequence()
    {
        yield return new WaitUntil(() => !uiController.IsProcessing);
        Debug.Log("The battle has ended");
        // SceneManager.LoadScene("City");
    }
}