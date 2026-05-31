using CoreClasses.Models;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WorldAnxietyCircleUI : MonoBehaviour
{
    public enum DisplayMode
    {
        Anxiety,
        XP
    }

    public Image fillImage;          // העיגול שמתמלא
    public TextMeshProUGUI stateText;
    public TextMeshProUGUI valueText;
    public DisplayMode mode = DisplayMode.Anxiety;

    private PlayerManager player;

    private float fill;
    private float displayedPercent = -1f;
    private float animationSpeed = 8f;
    private SaturationController saturationController;

    void Start()
    {
        player = GameController.Instance.player;
        saturationController = FindObjectOfType<SaturationController>();
        Debug.Log("Found saturation controller? " + saturationController);
        gameObject.SetActive(PlayerPrefs.GetInt("HasAnxietyClock", 0) == 1);
    }

    void Update()
    {
        if (player == null) return;

        // תצוגה משתנה לפי הצורך 
        if (Input.GetKeyDown(KeyCode.X))
        {
            mode = (mode == DisplayMode.Anxiety) ? DisplayMode.XP : DisplayMode.Anxiety;
            displayedPercent = -1f;
        }

        if (mode == DisplayMode.XP) 
        {
            // מצב XP
            fill = (float)player.TotalXP / saturationController.maxXPForFullSaturation;
            fillImage.fillAmount = fill;
            fillImage.color = Color.cyan;

            valueText.text = $"{player.TotalXP}";
            stateText.text = "XP";
            stateText.color = Color.cyan;

            return;
        }

        // מילוי העיגול
        fill = player.Anxiety.Value / player.Anxiety.Max;
        fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, fill, Time.deltaTime * 5f);

        // צבע לפי מצב
        var state = player.Anxiety.GetState();
        fillImage.color = state switch
        {
            AnxietyState.Balanced => new Color(0.7f, 0.5f, 0.9f),
            AnxietyState.High => new Color(0.5f, 0.2f, 0.7f),
            AnxietyState.Panic => new Color(0.2f, 0.0f, 0.3f),
            AnxietyState.Freeze => new Color(0.0f, 0.1f, 0.4f),
            _ => new Color(0.8f, 0.7f, 1f)
        };

        float percent = fill * 100f;

        // אם הערך השתנה — נפעיל אנימציה
        if (Mathf.Abs(percent - displayedPercent) > 0.1f)
        {
            displayedPercent = Mathf.Lerp(displayedPercent, percent, Time.deltaTime * animationSpeed);

            // טקסט דיגיטלי
            valueText.text = $"{displayedPercent:0}%";

            // אפקט דיגיטלי קטן: Pulse
            float pulse = 1f + Mathf.Sin(Time.time * 10f) * 0.05f;
            valueText.transform.localScale = new Vector3(pulse, pulse, 1f);
        }
        else
        {
            // כשהערך יציב — מחזירים לגודל רגיל
            valueText.text = $"{percent:0}%";
            valueText.transform.localScale = Vector3.one;
        }


        // טקסט מצב
        stateText.text = state.ToString();
        stateText.color = player.Anxiety.IsCritical ? Color.red : Color.white;
    }
}
