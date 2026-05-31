using CoreClasses.Models;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal; // חשוב עבור URP

public class SaturationController : MonoBehaviour
{
    public Volume globalVolume;
    public int maxXPForFullSaturation = 3000;

    private ColorAdjustments colorAdjustments;
    private PlayerManager player;

    void Start()
    {
        player = GameController.Instance.player;
    }

    void Update()
    {
        if (player == null) return;

        // מנסה לאתחל אם עדיין לא הצליח
        if (colorAdjustments == null)
        {
            TryInit();
            return;
        }

        float xpPercent = Mathf.Clamp01((float)player.TotalXP / maxXPForFullSaturation);
        colorAdjustments.saturation.value = Mathf.Lerp(-90f, 0f, xpPercent);
    }

    void TryInit()
    {
        if (GlobalVolume.Instance == null) return;

        if (GlobalVolume.Instance.Volume.profile.TryGet(out colorAdjustments))
        {
            colorAdjustments.saturation.overrideState = true;
            Debug.Log("ColorAdjustments initialized successfully");
        }
    }
}