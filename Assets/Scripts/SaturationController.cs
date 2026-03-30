using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal; // חשוב עבור URP

public class SaturationController : MonoBehaviour
{
    public Slider xpSlider;
    public Volume globalVolume;

    private ColorAdjustments colorAdjustments;

    void Start()
    {
        // גישה להגדרות ה-Volume
        if (globalVolume.profile.TryGet(out colorAdjustments))
        {
            UpdateSaturation(xpSlider.value);
        }

        // האזנה לשינויים בסליידר
        xpSlider.onValueChanged.AddListener(UpdateSaturation);
    }

    void UpdateSaturation(float xpValue)
    {
        if (colorAdjustments != null)
        {
            // חישוב: מפה את ה-XP (0 עד 100) לסטורציה (-100 עד 0)
            // כש-XP הוא 0, הסטורציה תהיה 100- (אפור)
            // כש-XP הוא 100, הסטורציה תהיה 0 (צבעוני רגיל)
            float newSaturation = Mathf.Lerp(-100f, 0f, xpValue / xpSlider.maxValue);
            colorAdjustments.saturation.value = newSaturation;
        }
    }
}