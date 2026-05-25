using CoreClasses.Models;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal; // חשוב עבור URP

public class SaturationController : MonoBehaviour
{
    public Volume globalVolume;
    public int maxXPForFullSaturation = 2000;

    private ColorAdjustments colorAdjustments;
    private PlayerManager player;

    void Start()
    {
        player = GameController.Instance.player;

        if (globalVolume.profile.TryGet(out colorAdjustments) == false)
        {
            Debug.LogError("No ColorAdjustments found in Volume!");
        }
    }

     void Update()
    {
        if (player == null || colorAdjustments == null) return;

        // אחוז XP מתוך הרמה הנוכחית
        float xpPercent = (float)player.TotalXP / maxXPForFullSaturation;

        // שינוי סטורציה לפי XP
        colorAdjustments.saturation.value = Mathf.Lerp(-90f, 0f, xpPercent);
    }
}