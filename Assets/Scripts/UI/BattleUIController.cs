using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CoreClasses.Models;
using System.Collections;

public class BattleUIController : MonoBehaviour
{
    [Header("Player UI")]
    public Slider playerHealthSlider;
    public Slider anxietySlider;
    public TextMeshProUGUI anxietyStateText;

    [Header("Enemy UI")]
    public Slider enemyHealthSlider;

    [Header("Feedback")]
    public TextMeshProUGUI battleLogText;

    [Header("Colors")]
    public Image anxietyFillImage;
    [Header("Animation Settings")]
    public float barSmoothSpeed = 5f;

    private Coroutine typingCoroutine;
   
    private PlayerManager _currentPlayer;
    private Enemy _currentEnemy;
    public bool IsProcessing { get; private set; }


    // בתוך פונקציית ה-Update של BattleUIController (לא בתוך UpdateUI)
    void Update()
    {
        if (_currentPlayer == null) return;

        // תנועה חלקה לבר החיים של השחקן
        playerHealthSlider.value = Mathf.Lerp(playerHealthSlider.value, _currentPlayer.Health, Time.deltaTime * barSmoothSpeed);

        // תנועה חלקה לבר החיים של האויב
        if (_currentEnemy != null)
        {
            enemyHealthSlider.value = Mathf.Lerp(enemyHealthSlider.value, _currentEnemy.Health, Time.deltaTime * barSmoothSpeed);
        }

        // תנועה חלקה לבר החרדה
        anxietySlider.value = Mathf.Lerp(anxietySlider.value, _currentPlayer.Anxiety.Value, Time.deltaTime * barSmoothSpeed);
    }
    // פונקציה מרכזית שמעדכנת את כל התצוגה
    public void UpdateUI(PlayerManager player, Enemy enemy, string message)
    {
        _currentPlayer = player;
        _currentEnemy = enemy;

        UpdateLog(message);
        Debug.Log("DLL massage: " + message);
        //// עדכון חיים (של הקרב ושל האויב)
        //playerHealthSlider.value = player.Health;
        //enemyHealthSlider.value = enemy.Health;

        //// עדכון חרדה
        //anxietySlider.value = player.Anxiety.Value;

        if (anxietyFillImage != null)
        {
            var state = player.Anxiety.GetState();

            anxietyFillImage.color = state switch
            {
                AnxietyState.Balanced => new Color(0.7f, 0.5f, 0.9f), // סגלגל רגוע
                AnxietyState.High => new Color(0.5f, 0.2f, 0.7f),     // סגול מתחזק
                AnxietyState.Panic => new Color(0.2f, 0.0f, 0.3f),    // סגול כהה (פאניקה)
                AnxietyState.Freeze => new Color(0.0f, 0.1f, 0.4f),   // כחול כהה (קיפאון)
                _ => new Color(0.8f, 0.7f, 1f)                       // סגול בהיר (נמוך)
            };
        }

            // הצגת המצב הרגשי
            anxietyStateText.text = "State: " + player.Anxiety.GetState().ToString();

        // צבע הטקסט משתנה לפי מצב החרדה
            anxietyStateText.color = player.Anxiety.IsCritical ? Color.red : Color.white;
    }

    // פונקציה ראשונית להגדרת המקסימום
    public void SetupBattle(PlayerManager player, Enemy enemy)
    {
        _currentPlayer = player;
        _currentEnemy = enemy;

        playerHealthSlider.maxValue = player.MaxHealth;
        enemyHealthSlider.maxValue = enemy.MaxHealth;
        anxietySlider.maxValue = player.Anxiety.Max;

        playerHealthSlider.value = player.Health;
        enemyHealthSlider.value = enemy.Health;
        anxietySlider.value = player.Anxiety.Value;

        //enemyNameText.text = enemy.Name;
        UpdateUI(player, enemy, $"You feel the presence of {enemy.Name} starting to overwhelm you...");
    }
    public void UpdateLog(string message)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(message));
    }
    public IEnumerator TypeText(string message)
    {
        IsProcessing = true; // התחלת עיבוד
    
        battleLogText.text = "";
        foreach (char letter in message.ToCharArray())
        {
            battleLogText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        float delay = message.Length > 50 ? 2.0f : 1.2f;
        yield return new WaitForSeconds(delay);
        IsProcessing = false;
    }
}