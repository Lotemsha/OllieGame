using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class GlobalVolume : MonoBehaviour
{
    public static GlobalVolume Instance { get; private set; }
    public Volume Volume { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Volume = GetComponent<Volume>();
        DontDestroyOnLoad(gameObject);
    }
}